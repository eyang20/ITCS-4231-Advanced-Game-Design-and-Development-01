using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPooling : MonoBehaviour
{
    //The Dictionary is a generic collection. It needs a key and value and in this case a Tag and a Queue<>. Requires the use of using System.Collections.Generic;.
    //The Queue need to needs to know what it has to store. In this case it is GameObject(s).
    //Dictionary<string, Queue<GameObject>> is called poolDictionary
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    public int enemyCount = 0;

    [System.Serializable]
    public class Pool
    {
        public string tag;
        public GameObject enemy;
        public int size;
    }

    public static EnemySpawnPooling Instance;

    private void Awake()
    {
        Instance = this;
    }

    //Adds a list called pools from the class Pool each with thier own tag, enemy, and size from the class Pool.
    public List<Pool> pools;

    // Start is called before the first frame update
    void Start()
    { 
        //Sets poolDictionary to be a new Dictionary<string, Queue<GameObject>>();
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        //Foreach Pool class called pool in the list of pools...
        foreach (Pool pool in pools)
        {
            //...queue GameObjects called objectPool is a new queue of GameObjects.
            Queue<GameObject> objectPool = new Queue<GameObject>();

            if (enemyCount <= pool.size)
            {
                //For an int called i is zero, if i is less than the size value in pool, increase the value of i until it is no longer less than the size value in pool.
                for (int i = 0; i < pool.size; i++)
                {
                    //A GameObject called obj is an Instantiated enemy from pool.
                    GameObject obj = Instantiate(pool.enemy);

                    //Increase the value of enemyCount.
                    enemyCount++;

                    //Set obj to an inactive state.
                    obj.SetActive(false);

                    //objectPool puts obj into it's Queue.
                    objectPool.Enqueue(obj);
                }
            }

            else
            {
                return;
            }

            //For an int called i is zero, if i is less than the size value in pool, increase the value of i until it is no longer less than the size value in pool.
            /*for (int i = 0; i < pool.size; i++)
            {
                //A GameObject called obj is an Instantiated enemy from pool.
                GameObject obj = Instantiate(pool.enemy);

                //XXX
                //enemyCount++;

                //Set obj to an inactive state.
                obj.SetActive(false);

                //objectPool puts obj into it's Queue.
                objectPool.Enqueue(obj);
            }*/

            //Adds the pool objectPool to the poolDictionary.
            poolDictionary.Add(pool.tag, objectPool);
        }
    }

    //Get the string tag, the Vector3's position, and gets the rotation of the GameObject
    public GameObject SpawnFromPool(string tag, Vector3 position, Quaternion rotation)
    {
        //If poolDictionary does NOT contain a key with tag...
        if (!poolDictionary.ContainsKey(tag))
        {
            //...print text in the Debug menu and return nothing.
            Debug.LogWarning(tag + "does not exist");
            return null;
        }

        //A GameObject called objectToSpawn pulls the gameobject with tag from poolDictionary and then pulls out of the Queue
        GameObject objectToSpawn = poolDictionary[tag].Dequeue();

        //Sets objectToSpawn in an active state.
        objectToSpawn.SetActive(true);

        //Sets objectToSpawn's position and rotation to the position and rotation of the GameObject that this script is attached to.
        objectToSpawn.transform.position = position;
        objectToSpawn.transform.rotation = rotation;

        //Adds the objectToSpawn to the poolDictionary.
        poolDictionary[tag].Enqueue(objectToSpawn);

        //Returns the GameObject objectToSpawn to this method.
        return objectToSpawn;
    }
}
