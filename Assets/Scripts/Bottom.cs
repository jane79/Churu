using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottom : MonoBehaviour
{
    public GameObject player;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            PlayerResposition();
        }
    }
    void PlayerResposition()
    {
        player.transform.position = new Vector3(0, 0, 0);
        Rigidbody2D rigid = player.GetComponent<Rigidbody2D>();
        rigid.velocity = Vector2.zero;
    }
}
