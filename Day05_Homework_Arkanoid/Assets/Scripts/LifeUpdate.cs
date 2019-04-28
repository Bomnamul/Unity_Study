using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeUpdate : MonoBehaviour
{
    Text lifeLabel;
    // Start is called before the first frame update
    void Start()
    {
        lifeLabel = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        lifeLabel.text = ScoreManager.life.ToString();
    }
}
