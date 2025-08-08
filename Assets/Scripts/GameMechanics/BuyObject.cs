using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyObject : MonoBehaviour
{
    public int itemCost;
    public GameObject ally;
    public GameObject ammoBox;

    public enum ItemType
    {
        npc,
        ammo
    }

    public ItemType item = ItemType.npc;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Interact"))
        {
            //Skip the rest of the code if the player does not have enough money.
            if (GameManager.instance.money < itemCost)
            {
                return;
            }

            GameManager.instance.money -= itemCost;

            if (item == ItemType.ammo)
            {
                SpawnItem(ammoBox);
                Debug.Log("Bought item.");
            }

            else if (item == ItemType.npc)
            {
                SpawnItem(ally);
                Debug.Log("Summoned npc.");
            }
        }
    }

    void SpawnItem(GameObject item)
    {
        Instantiate(item, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
