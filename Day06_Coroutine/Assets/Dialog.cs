using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialog : MonoBehaviour
{
    bool showDialog = false;
    string answer = "";

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return StartCoroutine("ShowDialog");
        yield return StartCoroutine(answer); // 함수를 string으로 호출
    }

    IEnumerator ShowDialog()
    {
        showDialog = true;
        do
        {
            yield return null;
        }
        while (answer == "");
        showDialog = false;
    }

    IEnumerator ActionA()
    {
        print("ActionA");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator ActionB()
    {
        print("ActionB");
        yield return new WaitForSeconds(1f);
    }

    private void OnGUI() // Old Style
    {
        if (showDialog)
        {
            if (GUI.Button(new Rect(10f, 10f, 100f, 20f), "A"))
            {
                answer = "ActionA";
            }
            else if (GUI.Button(new Rect(10f, 50f, 100f, 20f), "B"))
            {
                answer = "ActionB";
            }
        }
    }
}
