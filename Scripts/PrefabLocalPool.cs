using System.Collections.Generic;
using UnityEngine;

namespace odt.util
{
    public static class Extension
    {
        public static Vector3 PoolPosition(this Vector3 param)
        {
            return new Vector3(-200, -200, -200);
        }
    }

    public class PrefabLocalPool
    {
        private List<GameObject> pool;

        public PrefabLocalPool(GameObject prefab, int prewarm = 0)
        {
            pool = new List<GameObject>();
            Initialize(prefab, prewarm);
        }

        private void Initialize(GameObject prefab, int prewarm)
        {
            for (int i = 0; i < prewarm; i++)
            {
                GameObject newObj = Object.Instantiate(prefab, new Vector3().PoolPosition(), Quaternion.identity);
                newObj.SetActive(false);
                pool.Add(newObj);
            }
        }

        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            if(pool.Count <= 0)
            {
                Debug.LogWarning($"Pool empty");
                return null;
            }

            foreach (GameObject obj in pool)
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }

            Debug.LogWarning($"Object not available on Pool : [{pool[0].ToString()}]");
            return null;
        }

        public void Despawn(GameObject gameObject)
        {
            foreach (GameObject obj in pool)
            {
                if (obj == gameObject)
                {
                    gameObject.SetActive(false);
                    return;
                }
            }
        }
    }
}




