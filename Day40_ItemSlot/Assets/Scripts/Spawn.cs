using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public GameObject itemPrefab;

    public void SpawnItem()
    {
        Instantiate(itemPrefab, GameObject.FindGameObjectWithTag("Player").transform.position + Vector3.right, Quaternion.identity);
    }
}
