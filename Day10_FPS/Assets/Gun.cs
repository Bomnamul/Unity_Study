using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public float fireRate = 10; // 초당 격발 횟수
    public Light muzzleFlash;
    public GameObject shellPrefab;
    public Transform shellEjection;
    public GameObject impactFX;
    public GameObject bulletHolePrefab;

    Camera fpsCamera;
    float nextTimeToFire = 0f;
    Vector3 originPos, smoothVel;
    float recoilAngle;
    float recoilVel;
    //Transform fx;
    //Transform shell;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInParent<Camera>();
        originPos = transform.localPosition;
        //shell = transform.GetChild(1).transform;
    }

    // Update is called once per frame
    void Update()
    {
        fpsCamera.transform.localRotation *= Quaternion.Euler(Vector3.left * recoilAngle);

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

        // kick damping
        // SmoothDamp: 스프링처럼 돌아옴(Lerp이랑 비슷)
        transform.localPosition = Vector3.SmoothDamp(transform.localPosition,
                                                     originPos,
                                                     ref smoothVel,
                                                     0.1f);

        // recoil damping
        recoilAngle = Mathf.SmoothDamp(recoilAngle, 0, ref recoilVel, 0.2f);
    }

    private void Shoot()
    {
        muzzleFlash.enabled = true;
        Invoke("OffFlashLight", 0.05f);

        MakeShell();

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

        GameObject fx = Instantiate(impactFX, hit.point, Quaternion.identity);
        Destroy(fx, 0.3f);

        MakeBulletHole(hit.point, hit.normal, hit.transform);

        // kick
        transform.localPosition -= Vector3.forward * UnityEngine.Random.Range(0.07f, 0.3f);

        // recoil
        recoilAngle += UnityEngine.Random.Range(2f, 5f);
        recoilAngle = Mathf.Clamp(recoilAngle, 0, 25);

        //StartCoroutine(ShellRoutine());
    }

    private void MakeBulletHole(Vector3 point, Vector3 normal, Transform parent)
    {
        var clone = Instantiate(bulletHolePrefab, point + normal * 0.01f, Quaternion.FromToRotation(-Vector3.forward, normal));
        clone.transform.parent = parent;
        Destroy(clone, 3f);
    }

    private void MakeShell()
    {
        GameObject clone = Instantiate(shellPrefab, shellEjection);
        clone.transform.parent = null;
    }

    //IEnumerator ShellRoutine()
    //{
    //    fx = Instantiate(shell, transform);
    //    fx.GetComponent<Rigidbody>().isKinematic = false;
    //    fx.GetComponent<Rigidbody>().AddForce(transform.right * 150f);
    //    fx.parent = null;
    //    yield return new WaitForSeconds(5f);
    //    Destroy(GameObject.Find("M4_Shell(Clone)"));
    //}

    void OffFlashLight()
    {
        muzzleFlash.enabled = false;
    }
}
