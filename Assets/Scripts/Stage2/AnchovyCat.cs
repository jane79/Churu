using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchovyCat : MonoBehaviour
{
    Animator animator;
    Vector3 movement;
    Rigidbody2D rigid;
    public float jumpPower;
    int jumpCount = 0;
    bool isAngry = true;

    void Start()
    {
        jumpCount = 0;
        rigid = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator> ();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.angularVelocity = 0f;
        // 플레이어 점프하면 똑같이 점프해서 그냥 못 지나가게
        if(Input.GetButtonDown("Jump") && isAngry){
            if(jumpCount == 1){
                //&& !anim.GetBool("isJumping")
                rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
                jumpCount+=1;
            }
            if(jumpCount == 0){
                rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
                jumpCount+=1;
            }
        }
    }
    void FixedUpdate(){
        if(rigid.velocity.y < 0){
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); // 에디터 창에서만 보여짐
            RaycastHit2D rayHitPlat = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Platform"));
            // RayCastHit : Ray에 닿은 오브젝트
            // GetMask() : 레이어 이름에 해당하는 정수값을 리턴하는 함수
            if(rayHitPlat.collider != null){   // collider가 있는 경우
                if(rayHitPlat.distance < 0.8f){
                    jumpCount = 0;
                }
            }
            
            //RaycastHit2D rayHitEne = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Default"));
            // 딴거 발 밑에 있을 때 점프 모션 그만두는거...
        }
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Anchovy" && isAngry){
            animator.SetBool("isAnchovyHere", true);
            gameObject.tag = "Untagged";
            isAngry = false;
        }
    }
}
