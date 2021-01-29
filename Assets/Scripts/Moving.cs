using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moving : MonoBehaviour
{
    public Transform startPos;
    public Transform endPos;
    public Transform desPos;
    public float speed;
    bool flip;
    SpriteRenderer spriteRenderer;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Start()
    {
        transform.position = startPos.position;
        desPos = endPos;
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            collision.transform.SetParent(transform);
        }
    }

    public void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, desPos.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, desPos.position) <= 0.05f)
        {
            flip = !flip;
            if (desPos == endPos) { desPos = startPos; }
            else desPos = endPos;
        }

        spriteRenderer.flipX = flip;
    }
}
