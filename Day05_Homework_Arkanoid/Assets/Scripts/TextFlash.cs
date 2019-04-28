using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextFlash : MonoBehaviour
{
    public GameObject flash;
    public static TextFlash instance;

    private void Awake()
    {
        if (TextFlash.instance == null)
        {
            TextFlash.instance = this;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        flash.SetActive(false);
        StartCoroutine (Blink());
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Submit"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Blink()
    {
        while (true)
        {
            flash.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            flash.SetActive(false);
            yield return new WaitForSeconds(0.3f);
        }
    }
}
