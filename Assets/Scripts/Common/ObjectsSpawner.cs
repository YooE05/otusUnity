using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectsSpawner : MonoBehaviour
{
    public GameObject InstantianeObject(GameObject prefab, Transform container)
    {
        return Instantiate(prefab, container);
    }
}
