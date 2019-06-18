using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppSystem : MonoBehaviour
{
    public RectTransform postPrefab;
    public RectTransform content;

    public void UpdatePostData(List<PostData> posts)
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach(var post in posts)
        {
            var p = Instantiate(postPrefab, content);
        }
    }
}
