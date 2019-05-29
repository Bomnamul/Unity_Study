using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtBox : MonoBehaviour
{
    public BoxCollider hurtBox;
    public Color collisionOpenColor;
    public Color collidingColor;
    public bool drawGizmo = true;

    ColliderState state = ColliderState.Open; // HitBox에서 바깥에 정의 해놓아서 그냥 사용가능

    private void OnDrawGizmos()
    {
        if (!drawGizmo)
        {
            return;
        }
        CheckGizmoColor();
        Gizmos.matrix = transform.localToWorldMatrix;
        Gizmos.DrawCube(hurtBox.center, hurtBox.size);
    }

    private void CheckGizmoColor()
    {
        switch (state)
        {
            case ColliderState.Open:
                Gizmos.color = collisionOpenColor;
                break;
            case ColliderState.Colliding:
                Gizmos.color = collidingColor;
                break;
        }
    }

    public void GetHitby(int damage)
    {
        state = ColliderState.Colliding;
        Invoke("ResetState", 0.2f);
    }

    void ResetState()
    {
        state = ColliderState.Open;
    }
}
