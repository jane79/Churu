using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallShooter : MonoBehaviour
{
    public Rigidbody2D Ball;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Rigidbody2D ball = (Rigidbody2D) Instantiate(Ball, transform.position, Quaternion.identity);
            ball.AddForce (new Vector2(-5, 3), ForceMode2D.Impulse);
        }
    }
}
