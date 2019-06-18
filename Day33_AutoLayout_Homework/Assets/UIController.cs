using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public InputField inputField;
    public ChatSystem chatSystem;

    int uiTimeStamp = 0;

    // Start is called before the first frame update
    void Start()
    {
        inputField.onEndEdit.AddListener((message) =>   // 편집이 끝나면 Callback으로 Message를 넘겨줌 (Delegate)
        {
            GameDataManager.Instance?.AddMessage(message, UnityEngine.Random.Range(0, 2) == 0 ? true : false);  // Instance? : 종료 시점에 null이 될 수 있으니 null이면 실행 x
            inputField.Select();
            inputField.ActivateInputField();
            inputField.text = string.Empty;
        });
    }

    // Update is called once per frame
    void Update()
    {
        int timeStamp = GameDataManager.Instance.GetTimeStamp();
        if (timeStamp != uiTimeStamp)
        {
            uiTimeStamp = timeStamp;
            List<ChatData> messages = GameDataManager.Instance.GetChatData();
            chatSystem.UpdateChatData(messages);
        }
    }
}
