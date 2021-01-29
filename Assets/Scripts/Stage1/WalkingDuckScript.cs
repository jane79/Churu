using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingDuckScript : MonoBehaviour
{
    Rigidbody2D rigid;
    public float movePower = 1f;
    Animator animator;
    Vector3 movement;
    SpriteRenderer spriteRenderer;
    int movementFlag = 0;  // 0:Idle, 1:Left, 2:Right
    // Start is called before the first frame update

    /////////////////// Move Range
    Vector3 pos; //현재위치
    Vector3 startPos;
    public float rightMax = 5.0f;
    public float leftMax = -5.0f;
    public float direction = 3.0f;  // 이동속도 + 방향
    ///////////////////
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        pos = transform.position;
        startPos = transform.position;
        //StartCoroutine("ChangeMovement");
    }

    /* For Random Movement
    //Coroutine
    IEnumerator ChangeMovement()
    {
        //Random Change Movement
        movementFlag = Random.Range(0, 3);
        //Mapping Animation
        if(movementFlag == 0){
            animator.SetBool("isMoving", false);
        }
        else{
            animator.SetBool("isMoving", true);
        }
        yield return new WaitForSeconds(3f);
        //Restart Logic
        StartCoroutine("ChangeMovement");
    }

    void FixedUpdate()
    {
        Move();
    }

    */
    
    private void Update()
    {
        rigid.angularVelocity = 0f;
        pos.x += Time.deltaTime*direction;
        if(pos.x - startPos.x >=rightMax){
            direction*=-1;
            pos.x = rightMax + startPos.x;
        }else if(pos.x - startPos.x <=leftMax){
            direction*=-1;
            pos.x=leftMax + startPos.x;
        }
        transform.position = pos;

        //Direction Sprite
        if(direction > 0){
            spriteRenderer.flipX = true;
        }else{
            spriteRenderer.flipX = false;
        }
    }
}
