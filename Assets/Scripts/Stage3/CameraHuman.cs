using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHuman : MonoBehaviour
{
    public GameObject Sprite;
    public AudioClip audioCamera;
    AudioSource audioSource;
    SpriteRenderer spriteRenderer;
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            StartCoroutine("TakePhoto", other);
        }
    }
    void OnTriggerStay2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            Rigidbody2D otherRigid = other.gameObject.GetComponent<Rigidbody2D>();
            if(otherRigid.position.x <= transform.position.x){
                    spriteRenderer.flipX = false;
            }else{
                spriteRenderer.flipX = true;
            }
        }
    }
    void OnTriggerExit2D(Collider2D other){
        if(other.gameObject.tag == "Player"){
            StopCoroutine("TakePhoto");
            //StopCoroutine(TakePhoto(other));
            Debug.Log("End Shoot");
        }
    }
    IEnumerator TakePhoto(Collider2D player){
        while(true){
            // 3번 연속 sprite 플레이어 쪽으로 날리기 & 타임
            Debug.Log("What's Wrong");
            ShootSprite(player);
            yield return new WaitForSeconds(1f);
            ShootSprite(player);
            yield return new WaitForSeconds(1f);
            ShootSprite(player);
            yield return new WaitForSeconds(3f);
        }
    }
    void ShootSprite(Collider2D player){
        audioSource.clip = audioCamera;
        audioSource.Play();
        Rigidbody2D playerRigid = player.gameObject.GetComponent<Rigidbody2D>();
        Vector3 playerPos = playerRigid.position;
        Vector3 spritePos = transform.position;
        spritePos.z = spritePos.z-1;
        GameObject obj = Instantiate(Sprite, spritePos, Quaternion.identity) as GameObject;
        float angle = Mathf.Atan2(playerPos.y - transform.position.y, playerPos.x - transform.position.x);
        // Debug.Log("x : "+(playerPos.x - transform.position.x).ToString()+"   y : "+(playerPos.y - transform.position.y).ToString());
        // Debug.Log("angle : " +angle.ToString());
        obj.SendMessage("TheStart", angle);
    }
}
