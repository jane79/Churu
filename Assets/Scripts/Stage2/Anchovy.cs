using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchovy : MonoBehaviour
{
    private const float moveSpeed = 0.05f;
    public Vector2 movePower;
    float shootTimer = 0;
    float bounceTimer = 0;
    Rigidbody2D rigid;
    Vector3 pos;
    bool isOnGround;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        pos = transform.position;
        isOnGround = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(isOnGround){
            rigid.angularVelocity = 0f;
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if(other.gameObject.tag == "AnchovyCat"){
            //Bad Cat Get Anchoby!
            Destroy(gameObject);
        }
        // else if(other.gameObject.tag == "Platform"){
        //     isOnGround = true;
        //     rigid.angularVelocity = 0f;
        //     //Destroy(gameObject);
        // }
    }
    void BouncingAnim(){
        
    }
    void FixedUpdate()
    {
        shootTimer+=Time.deltaTime;
        if(shootTimer >= 2){
           Destroy(gameObject);
        }
    }
}
