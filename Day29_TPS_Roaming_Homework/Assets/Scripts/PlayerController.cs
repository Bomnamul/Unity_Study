using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float jumpHeight = 1.5f;
    public float rotationSpeed = 180f;
    public LayerMask groundMask;
    public Transform groundChecker;
    public Transform weaponHolder;
    public Transform bowHolder;
    public Transform weaponDisarmHolder;
    public bool onGround = false;

    Rigidbody rb;
    RaycastHit hit;
    public Vector3 moveDirection = Vector3.zero;

    float angle = 0f;

    MouseLook mouseLook;
    Animator anim;

    public bool isEquipped {
        get
        {
            return weaponHolder != null && weaponHolder.childCount > 0;
        }
    }

    public bool isDisarmed {
        get
        {
            return weaponDisarmHolder != null && weaponDisarmHolder.childCount > 0;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        mouseLook = GetComponentInChildren<MouseLook>();
        anim = GetComponent<Animator>();
    }

    public void CheckOnGround()
    {
        onGround = Physics.SphereCast(groundChecker.position,
                                      0.2f,
                                      -transform.up,
                                      out hit,
                                      0.2f,
                                      groundMask,
                                      QueryTriggerInteraction.Ignore);

        anim.SetBool("OnGround", onGround);
    }

    // Update is called once per frame
    public void FrameMove()
    {
        CheckOnGround();

        //if (Input.GetButtonDown("Jump") && isGrounded)
        //{
        //    Jump(jumpHeight);
        //}
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        float mouseMoveX = Input.GetAxis("Mouse X");

        if (Input.GetMouseButtonDown(1))
        {
            transform.Rotate(Vector3.up * Quaternion.FromToRotation(transform.forward, mouseLook.target.forward).eulerAngles.y);
            mouseLook.ResetCamera();
        }

        if (Input.GetMouseButton(1))
        {
            moveDirection = (new Vector3(h, 0, v)).normalized;
            float rotationY = mouseMoveX * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationY);
        }
        else
        {
            moveDirection = (new Vector3(0, 0, v)).normalized;
            float rotationY = h * rotationSpeed * Time.deltaTime;
            transform.Rotate(Vector3.up, rotationY);
        }

        moveDirection = transform.TransformDirection(moveDirection);
        moveDirection *= moveSpeed;

        anim.SetFloat("h", h);
        anim.SetFloat("v", v);
    }

    private void FixedUpdate() // 초기화가 안되면 움직이는 중에 다른 동작시 움직임
    {
        Vector3 move = moveDirection * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
        moveDirection = Vector3.zero;
    }

    public void Jump(float jumpHeight)
    {
        rb.drag = 0;
        rb.velocity = Vector3.zero;
        rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(groundChecker.position, 0.2f);
    }

    private void OnCollisionStay(Collision collision)
    {
        if (rb.velocity.y > 0)
            return;
        angle = Vector3.Angle(Vector3.up, collision.contacts[0].normal);
        if (30 <= angle && angle <= 45)
            rb.drag = 100;
        else
            rb.drag = 0;
    }

    private void OnCollisionExit(Collision collision)
    {
        rb.drag = 0;
        angle = 0;
    }

    public GameObject GetNearestWeaponIn(float radius, float angle, string weaponTag)
    {
        GameObject[] weapons = GameObject.FindGameObjectsWithTag(weaponTag);
        var list = new List<GameObject>(weapons);
        int index = list.FindIndex(o =>
        {
            Vector3 dir = o.transform.position - transform.position;
            return dir.magnitude < radius && Vector3.Angle(dir, transform.forward) < angle;
        });
        if (index == -1)
            return null;
        return list[index];
    }
    void Disarm()
    {
        if (isEquipped && !anim.IsInTransition(0))
        {
            Transform weapon = weaponHolder.GetChild(0);
            weapon.SetParent(weaponDisarmHolder);
            anim.SetInteger("HoldingWeaponID", 0);
        }
    }

    void Equip() // callback function
    {
        if (isDisarmed && !anim.IsInTransition(0))
        {
            Transform weapon = weaponDisarmHolder.GetChild(0);
            weapon.SetParent(weaponHolder);
            anim.SetInteger("HoldingWeaponID", weapon.GetComponent<WeaponType>().weaponId);
        }
    }

    public void TrailEnable()
    {
        GameObject trail = weaponHolder.GetChild(0).GetChild(2).GetChild(0).gameObject;
        trail.SetActive(true);
    }

    public void TrailDisable()
    {
        GameObject trail = weaponHolder.GetChild(0).GetChild(2).GetChild(0).gameObject;
        trail.SetActive(false);
    }
}
