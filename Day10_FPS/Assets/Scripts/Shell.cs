using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour
{
    public float forceMin = 50;
    public float forceMax = 200;

    float lifeTime = 4f;
    float fadeTime = 2f;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float force = UnityEngine.Random.Range(forceMin, forceMax);
        rb.AddForce(transform.right * force);
        rb.AddTorque(UnityEngine.Random.insideUnitSphere * force);
        //Destroy(gameObject, 100f);

        StartCoroutine(Fade());
    }

    IEnumerator Fade()
    {
        yield return new WaitForSeconds(lifeTime);

        float percent = 0;
        //float fadeSpeed = 1 / fadeTime;
        Material mat = GetComponent<Renderer>().material;
        Color initialColor = mat.color;
        while (percent < 1)
        {
            //percent += Time.deltaTime * fadeSpeed;
            percent += (1 / fadeTime) * Time.deltaTime;
            mat.color = Color.Lerp(initialColor, Color.clear, percent);
            yield return null;
        }
        Destroy(gameObject);
    }
}
