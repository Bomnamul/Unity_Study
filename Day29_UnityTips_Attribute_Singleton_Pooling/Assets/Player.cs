using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        print(MobManager.Instance.myGlobalVar == "whatever"); // MobManager의 static한 변수 Instance (하나만 존재)
        print(MobManager.Instance.MobCount() == 100);
    }
}
