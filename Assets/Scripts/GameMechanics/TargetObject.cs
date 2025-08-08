using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetObject : MonoBehaviour
{
    public enum TargetingType
    {
        ally,
        enemy
    }

    public TargetingType targetingType = TargetingType.ally;
    public Transform targetor;
    public float detectionRadius = 15f;
    public float rotationSpeed = 5f;

    public LayerMask detectionLayers;
    public Transform currentTarget;
    public bool autoScan = true;

    public GameObject bullet;
    public float shootRange = 30f;
    public float fireCooldown = 1f; // Delay between shots
    private Coroutine firingCoroutine;

    void Update()
    {
        if (autoScan)
        {
            ScanForTargets();
        }

        if (currentTarget != null)
        {
            RotateToward(currentTarget);
        }

        if (currentTarget != null && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(AutoFireRoutine());
        }

        else if (currentTarget == null && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    public void StartFiringAt(Transform target)
    {
        currentTarget = target;

        if (firingCoroutine == null)
        {
            Debug.Log($"Starting fire routine at target: {currentTarget}");
            firingCoroutine = StartCoroutine(AutoFireRoutine());
        }
    }

    public IEnumerator AutoFireRoutine()
    {
        while (currentTarget != null)
        {
            Debug.Log("Firing coroutine running...");

            // Rotate toward target
            RotateToward(currentTarget);

            // Fire raycast
            Vector3 direction = (currentTarget.position - targetor.position).normalized;
            Ray ray = new Ray(targetor.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, shootRange))
            {
                Debug.Log($"Raycast hit: {hit.transform.name}");

                if (hit.transform == currentTarget)
                {
                    if(bullet != null)
                    {
                        //GameObject impact = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
                        GameObject impact = FireAtTarget();
                        Destroy(impact, 2f);
                    }

                    else
                    {
                        Debug.Log($"Shooting {currentTarget}.");
                    }

                    //Target targetScript = hit.transform.GetComponentInParent<Target>();
                    //if (targetScript != null)
                    //{
                    //    targetScript.TakeDamage(1f);
                    //}
                }
            }

            yield return new WaitForSeconds(fireCooldown);
        }
    }

    public void ScanForTargets()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, detectionRadius, detectionLayers);

        foreach (var hit in hits)
        {
            GameObject obj = hit.gameObject;

            if (obj == gameObject)
            {
                continue;
            }

            //Ignore other enemies
            if (targetingType == TargetingType.enemy)
            {                
                if (obj.CompareTag("Enemy"))
                {
                    continue;
                }                   
            }

            //Ignore allies or players
            else if (targetingType == TargetingType.ally)
            {            
                if (obj.CompareTag("Player") || obj.CompareTag("Ally"))
                    continue;
            }

            if (IsValidTarget(hit.gameObject))
            {
                currentTarget = hit.transform.root;
                Debug.Log($"{gameObject.name} targeting {currentTarget.name}");
                return; 
            }
        }

        currentTarget = null;
    }

    public bool IsValidTarget(GameObject obj)
    {
        string[] validTags = GetValidTargetTags();

        foreach (var tag in validTags)
        {
            if (obj.CompareTag(tag)) return true;
        }

        return false;
    }

    public string[] GetValidTargetTags()
    {
        if (targetingType == TargetingType.ally)
        {
            return new string[] { "Enemy" };
        }

        else //Enemy turret
        {
            return new string[] { "Player", "Ally" };
        }
    }

    public void RotateToward(Transform target)
    {
        Vector3 direction = (target.position - targetor.position);
        direction.y = 0f;
        direction.Normalize();

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        targetor.rotation = Quaternion.Slerp(targetor.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    public GameObject FireAtTarget()
    {
        GameObject projectile = Instantiate(bullet, targetor.position, targetor.rotation);

        Bullet bulletScript = projectile.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            if (targetingType == TargetingType.enemy)
            {
                bulletScript.bulletType = Bullet.BulletType.enemy;
            }

            else if (targetingType == TargetingType.ally)
            {
                bulletScript.bulletType = Bullet.BulletType.ally;
            }
        }

        return projectile;
    }

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, detectionRadius);
    //    Gizmos.color = Color.yellow;
    //    Gizmos.DrawWireSphere(transform.position, shootRange);

    //    Debug.DrawLine(targetor.position, currentTarget.position, Color.green);
    //}
}
