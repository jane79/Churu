using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabyDuck : MonoBehaviour
{
    Animator animator;
    Vector3 pos;
    public GameObject Mom;  // 엄마오리
    Rigidbody2D rigid;
    bool isTouched;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponentInChildren<Animator> ();
        pos = transform.position;
        isTouched = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player" && !isTouched){  //Die
            //Damaged
            isTouched = true;
            Vector3 momPos = pos;
            momPos.y+=0.3f;
            momPos.z-=1;
            Instantiate(Mom, momPos, Quaternion.identity);
        }
    }

}
