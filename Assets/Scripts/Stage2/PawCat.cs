using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PawCat : MonoBehaviour
{
    Vector3 startPos;
    public float upSpeed;
    public float downSpeed;
    public float targetY;
    public float targetX;
    public AudioClip audioPaw;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position;
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            // 위로 올라오기
            StopCoroutine("PawDown");
            StartCoroutine("PawUp");
        }
    }
    void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player"){
            // 다시 내려가기
            StopCoroutine("PawUp");
            StartCoroutine("PawDown");
        }
    }

    IEnumerator PawUp()
    {
        audioSource.clip = audioPaw;
        audioSource.Play();
        Vector3 pos = transform.position;
        if(transform.rotation.z != 0){
            while(pos.x>=targetX){
                pos.x = pos.x-upSpeed;
                transform.position = pos;
                yield return new WaitForSeconds(0.01f);
            }
        }else{
            while(pos.y<=targetY){
                pos.y = pos.y+upSpeed;
                transform.position = pos;
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return null;
        //1.63
    }

    IEnumerator PawDown(){
        Vector3 pos = transform.position;
        if(transform.rotation.z != 0){
            while(pos.x<=startPos.x){
                pos.x = pos.x+upSpeed;
                transform.position = pos;
                yield return new WaitForSeconds(0.01f);
            }
        }else{
            while(pos.y>=startPos.y){
                pos.y = pos.y-downSpeed;
                transform.position = pos;
                yield return new WaitForSeconds(0.01f);
            }
        }
        yield return null;
        //0
    }
}
