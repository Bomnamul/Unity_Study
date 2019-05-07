using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstPersonController : MonoBehaviour
{
    public float moveSpeed = 8f;
    public float mouseSensitivityX = 2f;
    public float mouseSensitivityY = 2f;
    public float jumpHeight = 2f;
    public Transform groundChecker;
    public LayerMask groundMask;

    Vector3 moveDirection = Vector3.zero;

    [SerializeField]
    bool isGrounded = false;
    RaycastHit hit;

    Transform cameraTransform;
    Rigidbody rb;
    float verticalLookRotation;

    // Start is called before the first frame update
    void Start()
    {
        cameraTransform = GetComponentInChildren<Camera>().transform;
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.SphereCast(groundChecker.position,
                                        0.5f,
                                        -transform.up,
                                        out hit,
                                        0.2f,
                                        groundMask,
                                        QueryTriggerInteraction.Ignore);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.velocity = Vector3.zero;
            rb.AddForce(Vector3.up * Mathf.Sqrt(jumpHeight * -2f * Physics.gravity.y), ForceMode.VelocityChange);
        }

        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        moveDirection = (new Vector3(h, 0, v)).normalized;
        moveDirection *= moveSpeed;

        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * mouseSensitivityX); // frame의 변화와 상관없는 수치이기에 deltaTime이 필요없다

        verticalLookRotation += Input.GetAxis("Mouse Y") * mouseSensitivityY;
        verticalLookRotation = Mathf.Clamp(verticalLookRotation, -30, 30); // 값의 범위를 지정
        cameraTransform.localEulerAngles = Vector3.left * verticalLookRotation; // local : 부모의 입장에서, cameraTransform.Rotate(global) 사용하면 안됨

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Cursor.lockState == CursorLockMode.Locked)
            {
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
            }
            else
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
    }

    private void FixedUpdate()
    {
        // TransformPoint: position, rotation, scale (좌표로 해석)
        // TransformDirection: rotation only (방향 벡터만 고려)
        // TransformVector: rotation and scale only (벡터의 방향과 길이)
        Vector3 move = transform.TransformDirection(moveDirection) * Time.fixedDeltaTime;
        rb.MovePosition(rb.position + move);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawSphere(groundChecker.position, 0.5f);
    }
}
