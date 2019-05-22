using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ColliderState
{
    Closed, 
    Open, 
    Colliding
}

public interface IHitBoxResponder
{
    void CollisionWith(Collider collider);
}

public class HitBox : MonoBehaviour
{
    public LayerMask mask;
    public Collider[] hitBoxes;
    public Color inactiveColor;         // Closed
    public Color collisionOpenColor;    // Open
    public Color collidingColor;        // Colliding
    public bool drawGizmo = true;
    public bool updateInEditor = false;
    public ColliderState state = ColliderState.Closed;

    List<Collider> colliderList;
    IHitBoxResponder responder = null;

    private void Awake()
    {
        colliderList = new List<Collider>();
    }

    private void OnDrawGizmos() // 필요할 경우 계속 호출 됨
    {
        if (!drawGizmo)
        {
            return;
        }

        if (updateInEditor && !Application.isPlaying) // Scene 뷰에서 체크하기 위해 사용
        {
            UpdateHitBox();
            print("UpdateHitBox");
        }

        CheckGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix; // 월드 기준으로 gizmos를 렌더링 collider의 transform 정보를 local에서 world로 바꿔줘야 함
        foreach (var c in hitBoxes)
        {
            if (c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Gizmos.DrawCube(bc.center, bc.size);
            }

            if (c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Gizmos.DrawSphere(sc.center, sc.radius);
            }
        }
    }

    private void CheckGizmoColor()
    {
        switch(state)
        {
            case ColliderState.Closed:
                Gizmos.color = inactiveColor;
                break;
            case ColliderState.Open:
                Gizmos.color = collisionOpenColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = collidingColor;
                break;
        }
    }

    public void UpdateHitBox()
    {
        if (colliderList == null)
        {
            colliderList = new List<Collider>();
        }
        else
        {
            colliderList.Clear();
        }

        if (state == ColliderState.Closed)
        {
            return;
        }

        foreach (var c in hitBoxes)
        {
            if (c.GetType() == typeof(BoxCollider))
            {
                BoxCollider bc = (BoxCollider)c;
                Collider[] colliders = Physics.OverlapBox(transform.TransformPoint(bc.center), 
                                                          bc.size * 0.5f, 
                                                          transform.rotation, 
                                                          mask, 
                                                          QueryTriggerInteraction.Collide);
                colliderList.AddRange(colliders);
            }

            if (c.GetType() == typeof(SphereCollider))
            {
                SphereCollider sc = (SphereCollider)c;
                Collider[] colliders = Physics.OverlapSphere(transform.TransformPoint(sc.center),
                                                             sc.radius,
                                                             mask,
                                                             QueryTriggerInteraction.Collide);
                colliderList.AddRange(colliders);
            }
        }

        foreach (var c in colliderList)
        {
            // C# 6.0부터 가능
            responder?.CollisionWith(c);
            //if (responder != null)
            //{
            //    responder.CollisionWith(c);
            //}
            //print("Colliding: " + c.name);
        }
        state = colliderList.Count > 0 ? ColliderState.Colliding : ColliderState.Open;
    }

    public void StartCheckingCollision()
    {
        state = ColliderState.Open;
    }

    public void StopCheckingCollision()
    {
        state = ColliderState.Closed;
    }

    public void SetResponder(IHitBoxResponder responder)
    {
        this.responder = responder;
    }
}
