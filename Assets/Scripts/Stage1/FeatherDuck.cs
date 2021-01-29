using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherDuck : MonoBehaviour
{
    Animator animator;
    Vector3 pos; //현재위치
    float distance;
    public GameObject Feather;   // 깃털
    public GameObject FeatherReversed;
    CircleCollider2D circleCollider;
    SpriteRenderer spriteRenderer;
    bool isTracking;

    List<Collider2D> PlayerTriggerList = new List<Collider2D>();
    float shootTimer = 2.5f;
    const float shootDelay = 2.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator = gameObject.GetComponentInChildren<Animator> ();
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        circleCollider = GetComponent<CircleCollider2D>();
        pos = transform.position;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(!PlayerTriggerList.Contains(other)){
            PlayerTriggerList.Add(other);
        }
        if(other.gameObject.tag == "Player"){
                isTracking = true;
                Debug.Log("Shoot Feather!");
            }
    }
    
    void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            //Debug.Log("FEATHER!!");
            //방향 전환
            Rigidbody2D otherRigid = other.gameObject.GetComponent<Rigidbody2D>();
            if(otherRigid.position.x <= transform.position.x){
                spriteRenderer.flipX = true;
            }else{
                spriteRenderer.flipX = false;
            }

            /////Shoot Feather
            Vector3 featherPos = transform.position;
            featherPos.z = featherPos.z-1;
            if(shootTimer >= shootDelay){
                // player is at left (reversed)
                if(otherRigid.position.x <= transform.position.x){
                    for(int i=0;i<4;i++){
                        float angle = 45-130/3*i;
                        Instantiate(FeatherReversed, featherPos, Quaternion.Euler(0, 0, angle));
                        shootTimer = 0;
                    }
                }
                else{
                    // player is at right (reversed)
                    for(int i=0;i<4;i++){
                        float angle = -45+130/3*i;
                        Instantiate(Feather, featherPos, Quaternion.Euler(0, 0, angle));
                        shootTimer = 0;
                    }
                }
            }
            shootTimer += Time.deltaTime;
            /////
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if(PlayerTriggerList.Contains(other)){
            PlayerTriggerList.Remove(other);
        }
        if(other.gameObject.tag == "Player"){
            isTracking = false;
            shootTimer = 2.5f;  // 깃털 shoot time 초기화
            Debug.Log("Stop Shooting!");
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}