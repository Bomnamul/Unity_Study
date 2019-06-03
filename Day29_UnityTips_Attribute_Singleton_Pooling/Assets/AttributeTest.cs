using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttributeTest : MonoBehaviour
{
    [Range(0, 10)]
    public int damageRange = 10;

    [Header("Health Settings")]
    public int health;
    public int maxHealth;
    [Space(10)]
    [Header("Damage Settings")]
    public int damage;
    [HideInInspector]
    public float hiddenValue = 100f;

    // Start is called before the first frame update
    void Start()
    {
        string s = "";
        for (int i = 0; i < 10; i++)
        {
            s += i;
        }
        print(s);

        Func1();
    }

    private void Func1()
    {
        Func2();
    }

    private void Func2()
    {
        int i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
