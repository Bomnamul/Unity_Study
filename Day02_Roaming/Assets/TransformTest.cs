using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour
{
    public Transform wayRoot;
    // Start is called before the first frame update
    int a = 1;
    int b = 1;
    void Start()
    {
        transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>().material.color = Color.yellow;
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).Translate(wayRoot.GetChild(0).position);
        }

        for (int i = 0; i < wayRoot.childCount; i++)
        {
            wayRoot.GetChild(i).GetComponent<MeshRenderer>().material.color = Color.red;
        }
    }

    // Update is called once per frame
    void Update()
    {
        var list = new List<Transform>();
        for (int i = 0; i < wayRoot.childCount; i++)
        {
            list.Add(wayRoot.GetChild(i));
        }
        Vector3 v = list[b].position - transform.GetChild(1).position;

        list[a].GetComponent<MeshRenderer>().material.color = Color.green;
        list[b].GetComponent<MeshRenderer>().material.color = Color.green;

        transform.GetChild(0).LookAt(list[a]);
        transform.GetChild(0).Translate(transform.GetChild(0).forward.normalized * 8 * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.GetChild(0).position, list[a].position) < 0.3)
        {
            list[a].GetComponent<MeshRenderer>().material.color = Color.red;
            if (a == list.Count - 1)
            {
                a = 0;
            }
            else
            {
                a++;
            }
        }

        transform.GetChild(1).Translate(v.normalized * 10 * Time.deltaTime, Space.World);
        if (Vector3.Distance(transform.GetChild(1).position, list[b].position) < 0.3)
        {
            list[b].GetComponent<MeshRenderer>().material.color = Color.red;
            if (b == list.Count - 1)
            {
                b = 0;
            }
            else
            {
                b++;
            }
        }
    }
}
