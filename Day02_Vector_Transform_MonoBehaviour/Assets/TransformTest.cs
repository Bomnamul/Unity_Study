using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // transform: 1. transform(position, rotate, scale) 자체 2. 계층관계를 갖고 있음(tree 구조의 Node에 해당)
        print(transform.position); // world position
        print(transform.rotation);
        print(transform.lossyScale); // 함부로 못바꿈

        print(transform.forward); // 현재 GameObject가 바라보는 방향
        print(transform.right);
        print(transform.up);

        print(transform == GetComponent<Transform>());
        print(transform.childCount == 3);
        print(transform.GetChild(0).name == "B");
        print(transform.GetChild(1).name == "C");
        print(transform.GetChild(0).parent.name == "A");
        print(transform.Find("D").name == "D");
        print(transform.Find("D/F").name == "E"); // '/'로 디렉토리 찾듯이 찾을 수 있음
        print(transform.Find("D/F").root.name == "A");
        print(transform.Find("D/F").root == transform);
        print(transform.Find("D/F").root.name == transform.name);
        print(transform.Find("D/F").root.name == transform.gameObject.name);

        GetComponent<MeshRenderer>().material.color = Color.yellow;
        gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;
        transform.gameObject.GetComponent<MeshRenderer>().material.color = Color.yellow;

        print("DFS TEST");
        print(DFS(transform) == "A B E G C D F ");
        print("BFS TEST");
        print(BFS(transform) == "A B C D E F G ");

    }

    public string DFS(Transform t)
    {
        var list = new List<Transform>();
        for (int i = 0; i < t.childCount; i++)
        {
            list.Add(t.GetChild(i));
        }
        string s = t.name + " ";
        foreach (var n in list)
        {
            s += DFS(n);
        }
        return s;
    }

    public string BFS(Transform t)
    {
        string s = "";
        var q = new Queue<Transform>();
        q.Enqueue(t);
        while (q.Count > 0)
        {
            Transform n = q.Dequeue();
            s += n.name + " ";

            for (int i = 0; i < n.childCount; i++)
            {
                q.Enqueue(n.GetChild(i));
            }
        }
        return s;
    }
}
