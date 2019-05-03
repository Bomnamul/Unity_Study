using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Paths : MonoBehaviour
{
    public Transform target;
    public PathType pathType = PathType.CatmullRom;
    public Vector3[] wayPoints = new[]
    {
        new Vector3(4, 2, 6),
        new Vector3(8, 6, 14),
        new Vector3(4, 6, 14),
        new Vector3(0, 6, 6),
        new Vector3(-3, 0, 0),
    };

    // Start is called before the first frame update
    void Start()
    {
        Tween t = target.DOPath(wayPoints, 4f, pathType)
            .SetOptions(true) // SetOptions(true) 폐곡선? 으로
            .SetLookAt(0.001f);
        t.SetEase(Ease.Linear).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
