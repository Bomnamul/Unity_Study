using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakUp : MonoBehaviour
{
    public Texture[] cracks;
    public ParticleSystem fx;

    Renderer render;
    int numHits = 0;
    float lastHitTime;
    float hitTimeThreadhold = 0.05f; // 이 간격으로 다음 키 입력 받음

    // Start is called before the first frame update
    void Start()
    {
        render = GetComponent<Renderer>();
        lastHitTime = Time.time;
    }

    public void Hit() // Coroutine...
    {
        StopAllCoroutines();
        //CancelInvoke();
        if (Time.time > lastHitTime + hitTimeThreadhold)
        {
            numHits++;
            if (numHits < cracks.Length)
            {
                render.material.SetTexture("_DetailMask", cracks[numHits]);
            }
            else
            {
                var clone = Instantiate(fx, transform.position, Camera.main.transform.rotation);
                Destroy(clone, 2f); // AddComponent의 반대가 Destroy이기 때문에 GameObject를 지울 것인지 Component를 지울 것인지 확실히 구분해야 함
                Destroy(gameObject);
            }
            lastHitTime = Time.time;
        }
        //Invoke("Heal", 2f); // Coroutine으로 해보자
        StartCoroutine(Healing());
    }

    IEnumerator Healing()
    {
        yield return new WaitForSeconds(2f);
        numHits = 0;
        render.material.SetTexture("_DetailMask", cracks[0]);
    }

    //    void Heal()
    //    {
    //        numHits = 0;
    //        render.material.SetTexture("_DetailMask", cracks[0]);
    //    }
}
