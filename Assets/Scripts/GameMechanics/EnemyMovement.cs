using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System.Linq;

public class EnemyMovement : MonoBehaviour
{
    private TargetObject enemyAttack;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public Transform[] walkPoints;
    private int currentWalkIndex = 0;

    //Attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    private void Awake()
    {
        walkPoints = GetNearestPath(transform);
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();

        enemyAttack = GetComponent<TargetObject>();
        enemyAttack.targetingType = TargetObject.TargetingType.enemy;
        sightRange = enemyAttack.detectionRadius;
        attackRange = enemyAttack.shootRange;
    }

    private void Update()
    {
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatIsPlayer);

        if (!playerInSightRange && !playerInAttackRange)
        {
            MoveToWalkPoint();
        }

        if (playerInSightRange)
        {
            enemyAttack.RotateToward(player);
        }
        
        if (playerInSightRange && !playerInAttackRange)
        {
            //...calls the method ChasePlayer().
            ChasePlayer();
        }

        if (playerInSightRange && playerInAttackRange)
        {
            //...calls the method AttackPlayer().
            agent.SetDestination(transform.position);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            CharacterStats playerStats = other.GetComponentInParent<CharacterStats>();

            if (playerStats != null)
            {
                playerStats.TakeDamage(1);
                Destroy(gameObject);
            }
        }
    }

    private Transform[] GetNearestPath(Transform enemyTransform)
    {
        GameObject[] pathParents = GameObject.FindGameObjectsWithTag("AIWalkPoint");
        GameObject closestPath = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject path in pathParents)
        {
            float distance = Vector3.Distance(enemyTransform.position, path.transform.position);
            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                closestPath = path;
            }
        }

        if (closestPath != null)
        {
            List<Transform> pathPoints = new List<Transform>();
            foreach (Transform child in closestPath.transform)
            {
                pathPoints.Add(child);
            }
            return pathPoints.ToArray();
        }

        return new Transform[0];
    }

    private void MoveToWalkPoint()
    {
        if (walkPoints.Length == 0 || currentWalkIndex >= walkPoints.Length)
        {
            return;
        }

        agent.SetDestination(walkPoints[currentWalkIndex].position);

        float distance = Vector3.Distance(transform.position, walkPoints[currentWalkIndex].position);

        if (distance < 1f)
        {
            currentWalkIndex++;

            if (currentWalkIndex >= walkPoints.Length)
            {
                //Stop the AI once it reaches the last node.
                agent.isStopped = true;
            }
        }
    }

    private void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }
    
    //private void AttackPlayer()
    //{
    //    //Preventing the enemy from continuing to move
    //    //if(transform.position <= player.transform)
    //    //{
    //    //    agent.SetDestination(transform.position);
    //    //}
    //    enemyAttack.autoScan = false;

    //    agent.SetDestination(transform.position);
    //}

    //private void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, sightRange);
    //}
}
