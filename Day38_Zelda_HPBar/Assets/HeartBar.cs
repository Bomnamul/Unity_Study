using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Heart
{
    public Image foregroundImage;
    public Image[] glowImages;
    public RectTransform glow;

    public Heart(int maxHealth)
    {
        glowImages = new Image[maxHealth];
    }

    public void TurnOnGlowAt(int i)
    {

    }
}

public class HeartBar : MonoBehaviour
{
    public GameObject heartPrefab;
    public int totalHealth;
    public int CurrentHealth { get; private set; }
    const int healthPerHeart = 4;

}
