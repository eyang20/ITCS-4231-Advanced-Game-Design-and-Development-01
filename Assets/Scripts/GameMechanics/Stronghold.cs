using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stronghold : MonoBehaviour
{
    CharacterStats strongholdStats;

    // Start is called before the first frame update
    void Start()
    {
        strongholdStats = GetComponent<CharacterStats>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            CharacterStats enemyStats = other.GetComponentInParent<CharacterStats>();

            if (strongholdStats != null)
            {
                strongholdStats.TakeDamage(1);
            }

            if (enemyStats != null)
            {
                enemyStats.TakeDamage(999); // or just enemyStats.Die();
            }
        }
    }
}
