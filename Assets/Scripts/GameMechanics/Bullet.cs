using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public int damage;

    public enum BulletType
    {
        ally,
        enemy
    }

    public BulletType bulletType = BulletType.ally;

    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log($"{gameObject.name} was hit.");

        if (other.CompareTag("Player") && bulletType == BulletType.enemy)
        {
            DealDamage(other);
        }

        else if (other.CompareTag("Enemy") && bulletType == BulletType.ally)
        {
            DealDamage(other);
        }   
    }

    void DealDamage(Collider other)
    {
        other.GetComponentInParent<CharacterStats>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
