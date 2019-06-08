using System.Collections.Generic;
using UnityEngine;

namespace odt.util
{
    public class PrefabLocalPool
    {
        private static PrefabLocalPool pool;

        public static PrefabLocalPool GetOrCreateLocalPool(GameObject prefab, Transform parent, int size = 1)
        {
            if (pool == null)
            {
                pool = new PrefabLocalPool(prefab, parent, size);
            }

            return pool;
        }

        private Dictionary<int, List<GameObject>> poolDictionary;

        private PrefabLocalPool(GameObject prefab, Transform parent, int size)
        {
            if (poolDictionary == null)
            {
                poolDictionary = new Dictionary<int, List<GameObject>>();
            }

            Initialize(prefab, parent, size);

        }

        private void Initialize(GameObject prefab, Transform parent, int size)
        {
            if (!poolDictionary.ContainsKey(prefab.GetHashCode()))
            {
                List<GameObject> prefabs = new List<GameObject>();
                for (int i = 0; i < size; i++)
                {
                    GameObject newObj = Object.Instantiate(prefab, Vector3.zero, Quaternion.identity, parent);
                    newObj.SetActive(false);
                    prefabs.Add(newObj);
                }
                poolDictionary.Add(prefab.GetHashCode(), prefabs);
            }
        }

        public GameObject Spawn(int hashCode, Vector3 position, Quaternion rotation)
        {
            if (!poolDictionary.ContainsKey(hashCode))
            {
                Debug.LogWarning($"Pool not found : [{hashCode.ToString()}]");
                return null;
            }

            foreach (GameObject obj in poolDictionary[hashCode])
            {
                if (!obj.activeInHierarchy)
                {
                    obj.transform.position = position;
                    obj.transform.rotation = rotation;
                    obj.SetActive(true);
                    return obj;
                }
            }

            Debug.LogWarning($"Object not available on Pool : [{hashCode.ToString()}]");
            return null;
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation)
        {
            return Spawn(prefab.GetHashCode(), position, rotation);
        }

        public void Despawn(int hashCode, GameObject gameObject)
        {
            if (!poolDictionary.ContainsKey(hashCode))
            {
                Debug.LogWarning($"Pool not found : [{hashCode.ToString()}]");
                return;
            }

            foreach (GameObject obj in poolDictionary[hashCode])
            {
                if (obj.name.GetHashCode() == gameObject.name.GetHashCode())
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }
}




