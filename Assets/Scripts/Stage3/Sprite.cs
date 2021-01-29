using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sprite : MonoBehaviour
{
    float shootAngle;
    public const float shootSpeed = 0.05f;
    float shootTimer = 0;
    Vector3 pos;
    // Start is called before the first frame update
    void TheStart(float angle){
        shootAngle = angle;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = shootSpeed*Mathf.Cos(shootAngle);
        float moveY = shootSpeed*Mathf.Sin(shootAngle);
        pos.x = pos.x+moveX;
        pos.y = pos.y+moveY;
        transform.position = pos;
    }
    void FixedUpdate(){
        shootTimer+=Time.deltaTime;
        if(shootTimer >= 3){
           Destroy(gameObject);
        }
    }
}
