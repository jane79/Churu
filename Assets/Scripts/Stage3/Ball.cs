using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    Vector2 movePower;
    Rigidbody2D rigid;
    bool isPlayerHere = false;
    float shootTimer = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
    }
    void FixedUpdate(){
        shootTimer+=Time.deltaTime;
        if(shootTimer >= 3){
           Destroy(gameObject);
        }
    }
}
