using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateBlock : MonoBehaviour
{
    public GameObject blockPrefab;

    Camera fpsCamera;
    Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        fpsCamera = GetComponentInChildren<Camera>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("onClick");
            RaycastHit hit;
            if (Physics.Raycast(fpsCamera.transform.position, 
                                fpsCamera.transform.forward, 
                                out hit, 
                                10f))
            {
                Instantiate(blockPrefab, hit.transform.position + hit.normal, Quaternion.identity);
            }
        }
    }
}
