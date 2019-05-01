using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healing : MonoBehaviour
{
    public ParticleSystem heal;
    
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(heal, collision.transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        //Destroy(collision.transform.GetChild(1).gameObject);
        Destroy(collision.transform.Find("HealingEffect(Clone)").gameObject);
    }
}
