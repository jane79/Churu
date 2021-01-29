using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    private const float moveSpeed = 0.05f;
    float shootTimer = 0;
    float rotation;
    float radRot;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        rotation = transform.rotation.eulerAngles.z;
        radRot = rotation*Mathf.Deg2Rad;
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = moveSpeed*Mathf.Cos(radRot);
        float moveY = moveSpeed*Mathf.Sin(radRot);
        pos.x = pos.x+moveX;
        pos.y = pos.y+moveY;
        transform.position = pos;
    }
    
    void FixedUpdate()
    {
        shootTimer+=Time.deltaTime;
        if(shootTimer >= 3){
           Destroy(gameObject);
        }
    }
}
