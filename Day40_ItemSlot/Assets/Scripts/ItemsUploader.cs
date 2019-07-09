using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsUploader : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GameDataManager.instance.UploadItems();
        }
    }
}
