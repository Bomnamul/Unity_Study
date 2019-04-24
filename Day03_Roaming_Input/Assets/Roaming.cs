using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roaming : MonoBehaviour
{
    public Transform wayPointsRoot;

    List<Transform> wayPoints;
    public float moveSpeed = 1;
    private Vector3 nextPoint;
    public int n = 0;

    // Start is called before the first frame update
    void Start()
    {
        wayPoints = new List<Transform>();
        foreach (Transform t in wayPointsRoot) // 자식에 대하여 foreachable
        {
            wayPoints.Add(t);
        }
        nextPoint = wayPoints[n].transform.position;

        for (int i = 0; i < wayPoints.Count; i++)
        {
            wayPoints[i].GetComponent<MeshRenderer>().material.color = Color.magenta;
        }
        wayPoints[n].GetComponent<MeshRenderer>().material.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, nextPoint) < 0.2f)
        {
            n++;
            n %= wayPoints.Count; // 간편하니 외워둡시다
            //n = n % wayPoints.Count;
            nextPoint = wayPoints[n].transform.position;

            wayPoints[n].GetComponent<MeshRenderer>().material.color = Color.yellow;
            if (n == 0)
            {
                wayPoints[wayPoints.Count - 1].GetComponent<MeshRenderer>().material.color = Color.magenta;
            }
            else
            {
                wayPoints[n - 1].GetComponent<MeshRenderer>().material.color = Color.magenta;
            }
        }
        Vector3 dir = nextPoint - transform.position;
        dir.y = 0;
        //transform.Rotate(Vector3.up * Vector3.Angle(transform.forward, dir) * 10f * Time.deltaTime); // Angle함수는 항상 작은 값을 선택 But 현재는 오른 방향으로만 돌기 때문에 굳이 넓은 각도로 회전
        transform.Rotate(Vector3.up * Vector3.SignedAngle(transform.forward, dir, Vector3.up) * 10f * Time.deltaTime); // SignedAngle: Vector3.up 을 기준으로 양의 각도와 음의 각도로 나타냄 (-180 ~ 180)
        transform.position = transform.position + dir.normalized * moveSpeed * Time.deltaTime;
    }
}
