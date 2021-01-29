using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepingCat : MonoBehaviour
{
    public float movePower = 1f;
    Animator animator;
    Vector3 movement;
    SpriteRenderer spriteRenderer;
    public AudioClip audioSleep;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            audioSource.clip = audioSleep;
            audioSource.Play();
            animator.SetBool("isPlayerHere", true);
        }
    }
    //카메라 영역에서 벗어날 때, 다시 잠들기
    void OnBecameInvisible(){
        animator.SetBool("isPlayerHere", false);
    }
}
