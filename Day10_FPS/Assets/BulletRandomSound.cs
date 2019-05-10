using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletRandomSound : MonoBehaviour
{
    public AudioClip[] clips;

    AudioSource sound;

    private void Awake() // Script가 들어간 Object가 생긴 시점에 실행
    {
        sound = gameObject.AddComponent<AudioSource>();
    }

    private void Start() // Update가 호출되기 직전에 실행
    {
        
    }

    public void Play()
    {
        int rnd = Random.Range(0, clips.Length);
        sound.PlayOneShot(clips[rnd], 1f);
    }
}
