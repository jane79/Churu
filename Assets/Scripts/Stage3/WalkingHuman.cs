using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingHuman : MonoBehaviour
{
    public float moveSpeed = 0.1f;
    Animator animator;
    Vector3 movement;
    SpriteRenderer spriteRenderer;

    Vector3 pos; //현재위치
    public float direction = 3.0f;  // 이동속도 + 방향
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pos = transform.position;
    }
    // void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(other.gameObject.tag == "Player"){
    //         Debug.Log("CAT!");
    //     }
    // }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            pos = transform.position;
            if(other.gameObject.transform.position.x > transform.position.x){
                // 플레이어가 우측에 있을 때, 따라가기
                spriteRenderer.flipX = false;
                pos.x = pos.x+moveSpeed;
                transform.position = pos;
            }else{
                // 플레이어가 좌측에 있을 때
                spriteRenderer.flipX = true;
                pos.x = pos.x-moveSpeed;
                transform.position = pos;
            }
        }
    }
}
