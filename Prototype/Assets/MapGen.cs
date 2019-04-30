using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGen : MonoBehaviour
{
    int deadZone;
    MeshRenderer mr;
    public Material mt;
    public Material dz;
    public ParticleSystem smoke;

    // Start is called before the first frame update
    void Start()
    {
        for (float i = 0.5f; i <= 9.5f; i++)
        {
            for (float j = 0.5f; j <= 9.5f; j++)
            {
                GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.parent = transform;
                mr = cube.GetComponent<MeshRenderer>();
                cube.transform.position = new Vector3(i * 10f, -1.5f, j * 10f);
                cube.transform.localScale = new Vector3(10f, 3f, 10f);
                mr.material = mt;
            }
        }
        StartCoroutine(destroyDeadZone());
    }

    private void Update()
    {
        
    }

    IEnumerator destroyDeadZone()
    {
        while (transform.childCount != 0)
        {
            if (transform.childCount > 0)
            {
                deadZone = UnityEngine.Random.Range(0, transform.childCount - 1);
                mr = transform.GetChild(deadZone).GetComponent<MeshRenderer>();
                mr.material = dz;
                var smoke_instance = Instantiate(smoke, transform.GetChild(deadZone).position, Quaternion.Euler(-90f, 0f, 0f));
                yield return new WaitForSeconds(3f);
                transform.GetChild(deadZone).Translate(-Vector3.up * 2f);
                yield return new WaitForSeconds(1f);
                smoke_instance.transform.parent = transform.GetChild(deadZone);
                Destroy(transform.GetChild(deadZone).gameObject); // ?
                yield return new WaitForSeconds(1f); // ?
            }
        }
    }
}
