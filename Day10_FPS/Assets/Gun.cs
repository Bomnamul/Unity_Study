using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 10; // 초당 격발 횟수
    public Light muzzleFlash;

    Camera fpsCamera;
    float nextTimeToFire = 0f;
    Vector3 originPos, smoothVel;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInParent<Camera>();
        originPos = transform.localPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition,
                                                     originPos,
                                                     ref smoothVel,
                                                     0.1f);
        // SmoothDamp: 스프링처럼 돌아옴(Lerp이랑 비슷)
    }

    private void Shoot()
    {
        muzzleFlash.enabled = true;
        Invoke("OffFlashLight", 0.05f);

        RaycastHit hit;
        if (Physics.Raycast(fpsCamera.transform.position,
                            fpsCamera.transform.forward,
                            out hit,
                            200f))
        {
            print(hit.transform.name);
            Debug.DrawLine(fpsCamera.transform.position, hit.point, Color.magenta, 2f);
            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(fpsCamera.transform.forward * 500f);
            }
        }
        transform.localPosition -= Vector3.forward * UnityEngine.Random.Range(0.07f, 0.3f);
    }

    void OffFlashLight()
    {
        muzzleFlash.enabled = false;
    }
}
