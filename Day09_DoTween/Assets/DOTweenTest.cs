using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DOTweenTest : MonoBehaviour
{
    bool isRotating = false;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(1f);
        //transform.DOJump(new Vector3(3, 0, 0), 2f, 1, 1); // DontDestroyOnLoad에서 동작
        //transform.DOMoveX(5f, 2f);
        //transform.DOMoveX(5f, 2f).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        //transform.DOMoveX(5f, 2f).SetLoops(-1, LoopType.Restart).SetEase(Ease.OutElastic);
        //GetComponent<MeshRenderer>().material.DOColor(Color.magenta, 1f);
        //GetComponent<MeshRenderer>().material.DOColor(Color.magenta, 1f).SetLoops(-1, LoopType.Yoyo);
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !isRotating)
        {
            isRotating = true;
            transform.DORotate(transform.up * 90f, 5f).SetEase(Ease.OutElastic).SetRelative().OnComplete(() =>
            {
                isRotating = false;
            }); // OnComplete Callback으로 Lambda를 넣음
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            DOTween.Kill(transform);
        }
    }
}
