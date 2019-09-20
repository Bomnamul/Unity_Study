using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;

public class Slime : MonoBehaviourPun, IDamageable, IPunObservable
{
    public int health;

    Animator anim;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        print(health);
    }

    public void TakeHit(int damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        transform.DOMove(transform.position + hitDirection, 0.2f);
        anim.SetTrigger("Damaged");
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (int)stream.ReceiveNext();
        }
    }
}
