using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GwakDuck : MonoBehaviour
{
    Animator animator;
    Vector3 pos;
    public GameObject Gwak;   // 음파
    public GameObject GwakReversed;
    public AudioClip audioGwak;
    SpriteRenderer spriteRenderer;
    AudioSource audioSource;
    bool isTracking;
    bool isJumping = false;

    float shootTimer = 2.5f;
    const float shootDelay = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pos = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            isTracking = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            //Debug.Log("GWAK!!");
            //방향 전환
            Rigidbody2D otherRigid = other.gameObject.GetComponent<Rigidbody2D>();
            if(otherRigid.position.x <= transform.position.x){
                spriteRenderer.flipX = false;
            }else{
                spriteRenderer.flipX = true;
            }
            
            /////Shoot Gwak
            if(shootTimer >= shootDelay){
                audioSource.clip = audioGwak;
                audioSource.Play();
                // player is at left (reversed)
                if(otherRigid.position.x <= transform.position.x){
                    Instantiate(Gwak, transform.position, Quaternion.identity);
                    shootTimer = 0;
                }
                else{
                    // player is at right (reversed)
                    Instantiate(GwakReversed, transform.position, Quaternion.identity);
                    shootTimer = 0;
                }
            }
            shootTimer += Time.deltaTime;
            /////    
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            isTracking = false;
            shootTimer = 2.5f;  // 깃털 shoot time 초기화
            //Debug.Log("Stop Shooting!");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        
    }
}
