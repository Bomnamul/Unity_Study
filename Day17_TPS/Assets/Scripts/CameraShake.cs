using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Vector3 originPos;
    private void OnEnable()
    {
        originPos = transform.localPosition;
    }

    public IEnumerator Shake(float amount, float duration)
    {
        float time = 0;
        while (time <= duration)
        {
            transform.localPosition = originPos + Random.insideUnitSphere * amount;
            time += Time.deltaTime;
            yield return null;
        }
        transform.localPosition = originPos;
        enabled = false;
    }
}
