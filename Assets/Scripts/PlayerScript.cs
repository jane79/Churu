using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour
{
    public float maxSpeed;
    public float jumpPower;
    public int maxHealth = 3;
    public bool isUnBeatTime;
    public AudioClip audioJump;
    public AudioClip audioItem;
    public AudioClip audioDie;
    public AudioClip audioFinish;
    public AudioClip audioDamaged;
    public Rigidbody2D Anchovy;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    Animator anim;
    AudioSource audioSource;
    

    int jumpCount = 0;
    int health = 3;
    bool isDie = false;


    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        jumpCount = 0;
        health = maxHealth;
    }

    private void Update(){ // 단발적인 입력에 좋음
        //Check Health
        // if(health == 0){
        //     if(!isDie){
        //         //Die();
        //     }
        //     return;
        // }
        //Jump
        if(Input.GetButtonDown("Jump")){
            audioSource.clip = audioJump;
            audioSource.Play();

            if(jumpCount == 1){
                //&& !anim.GetBool("isJumping")
                rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isDoubleJumping", true);

                jumpCount+=1;
            }
            if(jumpCount == 0){
                rigid.AddForce(Vector2.up*jumpPower, ForceMode2D.Impulse);
                anim.SetBool("isJumping", true);
    
                jumpCount+=1;
            }        
        }
        //Stop Speed
        if(Input.GetButtonUp("Horizontal")){
            rigid.velocity = new Vector2(rigid.velocity.normalized.x*0.5f, rigid.velocity.y);
        }

        //Direction Sprite
        if(Input.GetButtonDown("Horizontal")){
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        //Animation
        if(rigid.velocity.normalized.x == 0){
            anim.SetBool("isWalking", false);
        }else{
            anim.SetBool("isWalking", true);
        }

        //Throwing Anchovy
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        if(sceneID == 5 && Input.GetButtonDown("Anchoby")){
            StartCoroutine("ThrowingAnchovy");
        }else if(sceneID == 5 && Input.GetButtonUp("Anchoby")){
            StopCoroutine("ThrowingAnchovy");
        }

    }

    IEnumerator ThrowingAnchovy()
    {
        while(true){
            Vector3 anchovyPos = transform.position;
            anchovyPos.z = anchovyPos.z-1;
            Rigidbody2D anchovy = (Rigidbody2D) Instantiate(Anchovy, anchovyPos, Quaternion.identity);
            if(spriteRenderer.flipX == false){   // 오른쪽으로 보고 있음
                anchovy.AddForce (new Vector2(5, 5), ForceMode2D.Impulse);
            }else{
                anchovy.AddForce (new Vector2(-5, 5), ForceMode2D.Impulse);
            }
            yield return new WaitForSeconds(0.3f);
        }
    }

    void FixedUpdate(){
        //Check Health
        // if(health == 0){
        //     return;
        // }
        //Move By Controller
        //Max Speed
        float h = Input.GetAxisRaw("Horizontal");
        rigid.AddForce(Vector2.right*h, ForceMode2D.Impulse);
        if(rigid.velocity.x > maxSpeed){   // Right
            rigid.velocity = new Vector2(maxSpeed, rigid.velocity.y);
        }
        else if(rigid.velocity.x < maxSpeed*(-1)){   // Left
            rigid.velocity = new Vector2(maxSpeed*(-1), rigid.velocity.y);
        }

        //RayCast : 오브젝트 검색을 위해 ray를 쏘는 방식
        //Landing Platform
        if(rigid.velocity.y < 0){
            Debug.DrawRay(rigid.position, Vector3.down, new Color(0, 1, 0)); // 에디터 창에서만 보여짐
            int layerMask = ~LayerMask.GetMask("Player");
            RaycastHit2D rayHitPlat = Physics2D.Raycast(rigid.position, Vector3.down, 1, layerMask);
            //RaycastHit2D rayHitWater = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Water"));
            //Debug.Log(rayHitPlat.collider.gameObject.name);
            // RayCastHit : Ray에 닿은 오브젝트
            // GetMask() : 레이어 이름에 해당하는 정수값을 리턴하는 함수
            // if(rayHitWater != null){
            //     Debug.Log(rayHitWater.ToString());
            //     jumpCount = 0;
            // }
            if(rayHitPlat.collider != null){   // collider가 있는 경우
                if(rayHitPlat.distance < 0.8f){
                    Debug.Log("F :"+rayHitPlat.collider.gameObject.name);
                    anim.SetBool("isJumping", false);
                    anim.SetBool("isDoubleJumping", false);
                    jumpCount = 0;
                }
            }
            
            //RaycastHit2D rayHitEne = Physics2D.Raycast(rigid.position, Vector3.down, 1, LayerMask.GetMask("Default"));
            // 딴거 발 밑에 있을 때 점프 모션 그만두는거...
        }
    }

    IEnumerator UnBeatTime()
    {
        int countTime = 0;
        while(countTime<10){
            //Alpha Effect
            if(countTime % 2 == 0){
                spriteRenderer.color = new Color32(255, 255, 255, 90);
            }else{
                spriteRenderer.color = new Color32(255, 255, 255, 180);
            }
            //Wiat Update Frame
            yield return new WaitForSeconds(0.2f);
            countTime++;
        }
        //Alpha Effect End
        spriteRenderer.color = new Color32(255, 255, 255, 255);
        //UnBeatTime Off
        isUnBeatTime = false;
        yield return null;
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if((other.gameObject.tag == "Enemy" && !isUnBeatTime) ||
            (other.gameObject.tag == "AnchovyCat" && !isUnBeatTime)){
            //Damaged
            audioSource.clip = audioDamaged;
            audioSource.Play();

            anim.SetTrigger("New Trigger");
            Vector2 attackedVelocity = Vector2.zero;
            if(other.gameObject.transform.position.x > transform.position.x){
                attackedVelocity = new Vector2(-7f, 11f);
            }else{
                attackedVelocity = new Vector2(7f, 11f);
            }
            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);
            health --;
            // if(health>0){
            //     isUnBeatTime = true;
            //     StartCoroutine("UnBeatTime");
            // }

            //For Debuging
            isUnBeatTime = true;
            StartCoroutine("UnBeatTime");
        }
    }

    
    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Attach : " + other.gameObject.tag);
        //Debug.Log("Health : "+health.toString())
        if(other.gameObject.tag == "Finish"){
            audioSource.clip = audioFinish;
            audioSource.Play();

            other.enabled = false;
            GameManager.FinishGame();
        }else if(other.gameObject.tag == "EnemySkill"){
            //Damaged
            audioSource.clip = audioDamaged;
            audioSource.Play();
            anim.SetTrigger("New Trigger");
            Vector2 attackedVelocity = Vector2.zero;
            if(other.gameObject.transform.position.x > transform.position.x){
                attackedVelocity = new Vector2(-2f, 7f);
            }else{
                attackedVelocity = new Vector2(2f, 7f);
            }
            rigid.AddForce(attackedVelocity, ForceMode2D.Impulse);
            health --;
            // if(health>0){
            //     isUnBeatTime = true;
            //     StartCoroutine("UnBeatTime");
            // }
            //For Debuging
            isUnBeatTime = true;
            StartCoroutine("UnBeatTime");
        }else if(other.gameObject.tag == "Item")
        {
            audioSource.clip = audioItem;
            audioSource.Play();

            bool isCan = other.gameObject.name.Contains("Can");
            bool isItem = other.gameObject.name.Contains("Item");
            bool isSuper = other.gameObject.name.Contains("superItem");
            //점수 TODO:
            if (isCan)
            {
                //gameManager.stagePoint += 50;
                // 목숨 +1
            }
            else if (isItem){
                //gameManager.stagePoint += 100;
            }
            else if (isSuper){
                //gameManager.stagePoint += 300;
            }
            other.gameObject.SetActive(false);
        }else if(other.gameObject.tag == "Water"){
            jumpCount = 0;
        }
    }
    

    void OnGUI()
    {
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginVertical();
        GUILayout.Space(10);
        GUILayout.BeginHorizontal();
        GUILayout.Space(15);
        string heart = "";
        // for(int i=0;i<health;i++){
        //     heart+="♥  ";
        // }
        GUILayout.Label(heart);
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.EndVertical();
        GUILayout.EndArea();
    }

    void Die()
    {
        audioSource.clip = audioDie;
        audioSource.Play();

        isDie = true;
        rigid.velocity = Vector2.zero;
        //Die Motion
        //anim.Play("Die");
        BoxCollider2D[] colls = gameObject.GetComponents<BoxCollider2D>();
        colls[0].enabled = false;
        //Die Bounching
        Vector2 dieVelocity = new Vector2(0, 10f);
        rigid.AddForce(dieVelocity, ForceMode2D.Impulse);
        //Restart Stage
        Invoke("RestartStage", 2f);
    }

    void RestartStage()
    {
        GameManager.RestartStage();
    }

    
}
