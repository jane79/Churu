using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MomDuck : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public float movePower = 1f;
    bool isTracing;
    GameObject traceTarget;
    public float rightMax;
    public float leftMax;
    Vector3 startPos;
    Vector3 pos;
    Rigidbody2D otherRigid;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        isTracing = false;
        startPos = transform.position;
        pos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        Move();
    }
    void LateUpdate()
    {
        //방향 전환
        if(isTracing){
            //int cnt = 0;
            if(otherRigid.position.x <= transform.position.x){
                //cnt+=1;
                Debug.Log("LEFT!");
                spriteRenderer.flipX = true;
            }else{
                //cnt+=1;
                Debug.Log("RIGHT!");
                spriteRenderer.flipX = false;
            }
            //Debug.Log(cnt.ToString());
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            traceTarget = other.gameObject;
            otherRigid = traceTarget.GetComponent<Rigidbody2D>();
            isTracing = true;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            isTracing = true;
            //Debug.Log(spriteRenderer.flipX.ToString());
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            isTracing = false;
        }
    }

    void Move(){
        Vector3 moveVelocity = Vector3.zero;
        string dist = "";
        if(isTracing){
            Vector3 playerPos = traceTarget.transform.position;
            if(playerPos.x<transform.position.x){
                dist = "Left";
            }else if(playerPos.x > transform.position.x){
                dist = "Right";
            }
        }
        if(dist == "Left"){
            moveVelocity = Vector3.left;
            transform.localScale = new Vector3(1, 1, 1);
        }else if(dist == "Right"){
            moveVelocity = Vector3.right;
            transform.localScale = new Vector3(-1, 1, 1);
        }

        pos += moveVelocity*movePower*Time.deltaTime;
        if(pos.x - startPos.x >=rightMax){
            pos.x = rightMax + startPos.x;
        }else if(pos.x - startPos.x <=leftMax){
            pos.x=leftMax + startPos.x;
        }
        transform.position = pos;
    }
}
