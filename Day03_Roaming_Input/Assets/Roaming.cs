// https://tiborstanko.sk/lerp-vs-slerp.html

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
        //transform.Rotate(Vector3.up * Vector3.SignedAngle(transform.forward, dir, Vector3.up) * 10f * Time.deltaTime); // SignedAngle: Vector3.up 을 기준으로 양의 각도와 음의 각도로 나타냄 (-180 ~ 180)
        //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(dir, Vector3.up), 0.15f); // Vector3.up 생략 가능
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 0.15f); // Slerp: 등각 속도 운동

        //transform.position = transform.position + dir.normalized * moveSpeed * Time.deltaTime;
        //transform.Translate(dir.normalized * moveSpeed * Time.deltaTime, Space.World);
        //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime); // wikipedia lerp 방식이지만 현재의 위치 관점에서 lerp (등속 X)
        //transform.position = Vector3.Lerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / dir.magnitude); // 분모가 짧아짐 == t값이 점점 커짐 (이해 하고 넘어 갈 것)
        //transform.position = Vector3.Slerp(transform.position, nextPoint, moveSpeed * Time.deltaTime / dir.magnitude); // 사실 Vector에 Lerp, Slerp 잘 안씀
        transform.position = Vector3.MoveTowards(transform.position, nextPoint, moveSpeed * Time.deltaTime); // 마지막 param == 최대 거리 (maxDistanceDelta)
    }
}
