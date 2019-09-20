using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Player : MonoBehaviourPun, IPunObservable
{
    public float moveSpeed = 4;

    Animator anim;
    float lastX, lastY;
    Vector3 heading;
    Vector3 networkPosition;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (!photonView.IsMine)
            return;

        heading = new Vector3(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"), 0).normalized;
        if (!anim.GetBool("Attack"))
            Move();

        if (Input.GetKeyDown(KeyCode.C) && !anim.GetBool("Attack"))
        {
            //Attack();
            photonView.RPC("AttackOnServer", RpcTarget.MasterClient);
        }

        var rdr = GetComponent<SpriteRenderer>();
        rdr.color = Color.red;
    }

    [PunRPC]
    void AttackOnServer(PhotonMessageInfo info) // Server에서만 실행됨 (Cheating을 방지하기 위해 param을 안 받아오고 server상의 정보로만 판단)
    {
        //int clientId = info.photonView.ViewID; // 객체마다 가지는 고유 ID
        //GameObject client = PhotonView.Find(clientId).gameObject;

        //Vector3 pos = client.transform.position + Vector3.up * 0.2f;
        //Vector2 dir = client.GetComponent<Player>().GetHeadingDirection();

        Vector3 pos = transform.position + Vector3.up * 0.2f;
        Vector2 dir = GetHeadingDirection();
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(pos, 0.3f, dir, 1f, 1 << LayerMask.NameToLayer("HurtBox"));
        Debug.DrawRay(pos, dir, Color.white, 0.5f);
        if (hit.collider != null)
        {
            //hit.transform.GetComponentInParent<IDamageable>().TakeHit(10, hit.point, dir);
            int id = hit.transform.GetComponentInParent<PhotonView>().ViewID;
            hit.transform.GetComponentInParent<IDamageable>().TakeDamage(10);
            photonView.RPC("AttackEffectOnClient", RpcTarget.All, id, 10, hit.point, dir); // RpcTarget.All: a, b, c 클라이언트가 있을 경우 a가 RpcTarget.All 호출 할 경우 각 클라이언트의 a 오브젝트에게 적용
        }
        else
        {
            photonView.RPC("AttackEffectOnClient", RpcTarget.All);
        }
    }

    [PunRPC]
    void AttackEffectOnClient(int id, int damage, Vector2 point, Vector2 direction, PhotonMessageInfo info)
    {
        PhotonView.Find(id).GetComponent<IDamageable>().TakeHit(10, point, direction);
        anim.SetBool("Attack", true);
    }

    [PunRPC]
    void AttackEffectOnClient()
    {
        anim.SetBool("Attack", true);
    }

    private void Attack()
    {
        Vector3 pos = transform.position + Vector3.up * 0.2f;
        Vector2 dir = GetHeadingDirection();
        RaycastHit2D hit;
        hit = Physics2D.CircleCast(pos, 0.3f, dir, 1f, 1 << LayerMask.NameToLayer("HurtBox"));
        Debug.DrawRay(pos, dir, Color.white, 0.5f);
        if (hit.collider != null)
        {
            hit.transform.GetComponentInParent<IDamageable>().TakeHit(10, hit.point, dir);
            hit.transform.GetComponentInParent<IDamageable>().TakeDamage(10);
        }

        anim.SetBool("Attack", true);
    }

    public Vector2 GetHeadingDirection()
    {
        if (heading.magnitude == 0)
            return new Vector2(lastX, lastY);
        else
            return heading;
    }

    private void Idle()
    {
        UpdateAnimation(heading);
    }

    private void Move()
    {
        Vector3 movement = heading * moveSpeed * Time.deltaTime;
        transform.position += movement;
        UpdateAnimation(heading);
    }

    private void UpdateAnimation(Vector3 heading)
    {
        if (heading.x == 0 && heading.y == 0)
        {
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("Move", false);
        }
        else
        {
            lastX = heading.x;
            lastY = heading.y;
            anim.SetFloat("DirX", heading.x);
            anim.SetFloat("DirY", heading.y);
            anim.SetFloat("LastDirX", lastX);
            anim.SetFloat("LastDirY", lastY);
            anim.SetBool("Move", true);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!photonView.IsMine)
            return;

        if (collision.CompareTag("Mob"))
        {
            photonView.RPC("AttackOnServer", RpcTarget.MasterClient);
            print("Attack");
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(heading);
            stream.SendNext(transform.position);
            stream.SendNext(lastX);
            stream.SendNext(lastY);
        }
        else
        {
            heading = (Vector3)stream.ReceiveNext();
            networkPosition = (Vector3)stream.ReceiveNext();
            lastX = (float)stream.ReceiveNext();
            lastY = (float)stream.ReceiveNext();

            float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
            networkPosition += (heading * moveSpeed * lag);
        }
    }

    private void FixedUpdate()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, 0.15f);
        }
    }
}
