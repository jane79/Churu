using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingCat : MonoBehaviour
{
    public float movePower = 1f;
    Animator animator;
    Vector3 movement;
    SpriteRenderer spriteRenderer;
    Vector3 pos; //현재위치
    Vector3 startPos;
    public float rightMax = 5.0f;
    public float leftMax = -5.0f;
    public float direction = 3.0f;  // 이동속도 + 방향
    bool isAttacking = false;
    Rigidbody2D rigid;
    
    
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pos = transform.position;
        startPos = transform.position;
        rigid = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rigid.angularVelocity = 0f;
        if(!isAttacking){
            pos.x += Time.deltaTime*direction;
        }
        if(pos.x-startPos.x >=rightMax){
            direction*=-1;
            pos.x = rightMax+startPos.x;
        }else if(pos.x - startPos.x <=leftMax){
            direction*=-1;
            pos.x=leftMax + startPos.x;
        }
        transform.position = pos;

        //Direction Sprite
        if(direction > 0 && !isAttacking){
            spriteRenderer.flipX = false;
        }else if (direction < 0 && !isAttacking){
            spriteRenderer.flipX = true;
        }
    }
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.gameObject.tag == "Player"){
    //         animator.SetBool("isPlayerTouched", true);
    //         isAttacking = true;
    //         if(other.gameObject.transform.position.x > transform.position.x){
    //             // 플레이어가 우측에 있을 때
    //             spriteRenderer.flipX = false;
    //         }else{
    //             spriteRenderer.flipX = true;
    //         }
    //     }
    // }
    // void OnTriggerExit2D(Collider2D other){
    //     if(other.gameObject.tag == "Player"){
    //         animator.SetBool("isPlayerTouched", false);
    //         isAttacking = false;
    //     }
    // }


    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player"){
            //Attack!
            animator.SetBool("isPlayerTouched", true);
            isAttacking = true;
            
            if(other.gameObject.transform.position.x > transform.position.x){
                // 플레이어가 우측에 있을 때
                spriteRenderer.flipX = false;
            }else{
                spriteRenderer.flipX = true;
            }
        }
    }
    void OnCollisionExit2D(Collision2D other){
        if(other.gameObject.tag == "Player"){
            animator.SetBool("isPlayerTouched", false);
            isAttacking = false;
        }
    }

}
