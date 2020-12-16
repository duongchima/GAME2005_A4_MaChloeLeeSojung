using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
 
    public static ObjectPooler Instance;
    public List<GameObject> objectPool;
    public GameObject Object;
        public int max;
    private void Awake()
    {
        Instance = this;
    }

 
    // Start is called before the first frame update
    void Start()
    {
        objectPool = new List<GameObject>();
        for (int i = 0; i < max; i++)
        {
            GameObject obj = (GameObject)Instantiate(Object, Object.transform.position, Object.transform.rotation);
            obj.SetActive(false);
            objectPool.Add(obj);
        }
    }
    public GameObject GetObject()
    {
        for(int i = 0; i < objectPool.Count; i++)
        {
            if(!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }
        return null;
    }

 
}
