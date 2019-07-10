using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public int slotId;

    public void Drop()
    {
        foreach (Transform child in transform)
        {
            child.GetComponent<Spawn>().SpawnItem();
            Destroy(child.gameObject);
            GameDataManager.instance.RemoveItemAt(slotId);
        }
    }

    //public Button xButton;
    //public GameObject healthPotion;

    //private void Start()
    //{
    //    xButton.onClick.AddListener(ButtonClicked);
    //}

    //private void ButtonClicked()
    //{
    //    print("Drop Potion: " + slotId);
    //    var player = GameFlow.instance.player;
    //    if (transform.childCount != 0)
    //    {
    //        print("Entered");
    //        Instantiate(healthPotion, player.transform.localPosition + Vector3.right, Quaternion.identity);
    //        Destroy(transform.GetChild(0).gameObject);
    //    }
    //}
}
