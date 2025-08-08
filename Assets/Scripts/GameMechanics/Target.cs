using UnityEngine;

public class Target : MonoBehaviour
{
    public float health = 50f;
    public GameObject textTransform;

    public void TakeDamage(float amount)
    {
        //Health is subtracted by a float called amount.
        health -= amount;

        //If health less than or equal to zero...
        if(health <= 0f)
        {
            //...call DestorySelf().
            DestorySelf();
        }
    }

    void DestorySelf()
    {
        //If textTransform is NOT null...
        if (textTransform != null)
        {
            //...sets textTransform to be disabled
            textTransform.SetActive(false);
        }

        //Destroys itself.
        Destroy(gameObject, 3);
    }
}
