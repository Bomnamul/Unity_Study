using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Prefab의 script와 협력해 전체 Chatting Message Redraw

public class ChatSystem : MonoBehaviour
{
    public RectTransform chatPrefab;
    public RectTransform content;

    public void UpdateChatData(List<ChatData> messages)
    {
        foreach(Transform child in content)
        {
            Destroy(child.gameObject);
        }

        foreach(var chat in messages)
        {
            var c = Instantiate(chatPrefab, content);
            c.GetComponent<ChatMessage>().SetMessage(chat.Message, chat.IsAlignLeft);
        }
    }
}
