using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gwak : MonoBehaviour
{
    private const float moveSpeed = 0.05f;
    float shootTimer = 0;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        pos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float moveX = moveSpeed*(-1);
        pos.x = pos.x+moveX;
        transform.position = pos;
    }

    void FixedUpdate()
    {
        shootTimer+=Time.deltaTime;
        if(shootTimer >= 5){
           Destroy(gameObject);
        }
    }
}
