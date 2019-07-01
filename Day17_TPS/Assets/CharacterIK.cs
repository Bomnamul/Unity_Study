using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterIK : MonoBehaviour
{
    public Transform lookAtThis;
    public bool IKEnabled = true;

    Animator anim;
    Transform leftFoot;
    Transform rightFoot;
    Vector3 leftFootPos;
    Vector3 rightFootPos;
    Quaternion leftFootRot;
    Quaternion rightFootRot;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        leftFoot = anim.GetBoneTransform(HumanBodyBones.LeftFoot);
        rightFoot = anim.GetBoneTransform(HumanBodyBones.RightFoot);
    }

    private void OnAnimatorIK(int layerIndex)
    {
        if (!IKEnabled)
        {
            return;
        }

        FindFloorPositions(leftFoot, ref leftFootPos, ref leftFootRot, Vector3.up);
        FindFloorPositions(rightFoot, ref rightFootPos, ref rightFootRot, Vector3.up);

        anim.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 0.5f);
        anim.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 0.3f);

        anim.SetIKPositionWeight(AvatarIKGoal.RightFoot, 0.5f);
        anim.SetIKRotationWeight(AvatarIKGoal.RightFoot, 0.3f);

        anim.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootPos);
        anim.SetIKPosition(AvatarIKGoal.RightFoot, rightFootPos);

        anim.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootRot);
        anim.SetIKRotation(AvatarIKGoal.RightFoot, rightFootRot);


        if (lookAtThis != null)
        {
            // Only Head
            //anim.SetLookAtWeight(1f);
            //anim.SetLookAtPosition(lookAtThis.position);

            // Head and body
            float distanceFaceObject = Vector3.Distance(
                anim.GetBoneTransform(HumanBodyBones.Head).position, 
                lookAtThis.position);

            anim.SetLookAtWeight(Mathf.Clamp01(2 - distanceFaceObject), Mathf.Clamp01(1 - distanceFaceObject));
            anim.SetLookAtPosition(lookAtThis.position);
        }
    }

    private void FindFloorPositions(Transform t, 
                                    ref Vector3 targetPosition, 
                                    ref Quaternion targetRotation, 
                                    Vector3 direction)
    {
        RaycastHit hit;
        Vector3 rayOrigin = t.position;
        rayOrigin += direction * 0.3f;

        Debug.DrawRay(rayOrigin, -direction, Color.green);
        if (Physics.Raycast(rayOrigin, -direction, out hit, 3f))
        {
            targetPosition = hit.point;
            Quaternion rot = Quaternion.LookRotation(transform.forward);
            targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal) * rot;
        }
    }
}
