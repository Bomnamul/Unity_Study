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
    public GameObject holdPos; // 집은 물건의 위치

    Camera fpsCamera;
    float nextTimeToFire = 0f;
    Vector3 originPos, smoothVel;
    float recoilAngle;
    float recoilVel;
    bool isHolding = false; // 물건을 들고 있는지 판단
    Transform originParent; // hold될 transform의 기존 부모
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

        if (Input.GetKeyDown(KeyCode.F))
        {
            Holding();
        }

        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            nextTimeToFire = Time.time + 1f / fireRate;

            if (isHolding)
            {
                Throwing();
            }
            else
            {
                Shoot();
            }
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

    private void Holding()
    {
        if (!isHolding)
        {
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, 2f)) // 2m안에 hit가 발생하면 hit의 부모를 기억해두고 holdPos의 자식으로 넣음 
            {
                if (hit.transform.GetComponent<Rigidbody>()) // 잡을 수 있는 물체인지 Rigidbody의 유무로 판단
                {
                    isHolding = true;
                    Rigidbody rb = hit.transform.GetComponent<Rigidbody>();
                    rb.isKinematic = true;
                    hit.transform.localPosition = holdPos.transform.position;
                    hit.transform.localRotation = holdPos.transform.rotation;
                    originParent = hit.transform.parent;
                    hit.transform.parent = holdPos.transform;
                }
            }
        }
        else
        {
            Throwing();
        }
    }

    private void Throwing() // rb에 AddForce로 힘을 가하고 부모를 원래대로 돌림
    {
        Rigidbody rb = holdPos.transform.GetChild(0).GetComponent<Rigidbody>();
        rb.isKinematic = false;
        rb.AddForce(fpsCamera.transform.forward * 500f);
        holdPos.transform.GetChild(0).parent = originParent;
        isHolding = false;
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
