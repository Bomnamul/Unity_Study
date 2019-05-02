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

    IEnumerator OnCollisionExit(Collision collision)
    {
        //Destroy(collision.transform.GetChild(1).gameObject);
        yield return new WaitForSeconds(1f);
        Destroy(collision.transform.Find("HealingEffect(Clone)").gameObject);
    }
}
