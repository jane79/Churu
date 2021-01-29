using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    Vector3 FinishPos;
    bool isStarted = false;
    static bool isFinished = false;
    static int sceneNum = 3;
    // Start is called before the first frame update
    void Awake()
    {
        Time.timeScale = 1f;
    }
    void Start()
    {
        FinishPos = GameObject.FindGameObjectWithTag("Finish").transform.position;
        if(sceneNum>0){
            StartGame();
        }
    }
    void OnGUI()
    {
        //GUI Stage
        GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        GUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        GUILayout.BeginVertical();
        GUILayout.FlexibleSpace();
        // if(sceneNum<7){
        //     GUILayout.Label("Chapter "+(sceneNum+1));
        // }else{
        //     GUILayout.Label("Stage End");
        // }
        GUILayout.Space(5);
        GUILayout.EndVertical();
        GUILayout.FlexibleSpace();
        GUILayout.EndHorizontal();
        GUILayout.EndArea();

        //Start & End GUI
        if(!isStarted && sceneNum == 3){
            GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.BeginVertical();
            GUILayout.FlexibleSpace();
            GUILayout.Label("R U Ready? :)");
            
            isStarted = true;
            StartGame();
            
            GUILayout.Space(100);
            GUILayout.FlexibleSpace();
            GUILayout.EndVertical();
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.EndArea();
        }

        // else if(isFinished && sceneNum == 5){
        //     GUILayout.BeginArea(new Rect(0, 0, Screen.width, Screen.height));
        //     GUILayout.BeginHorizontal();
        //     GUILayout.FlexibleSpace();
        //     GUILayout.BeginVertical();
        //     GUILayout.FlexibleSpace();
        //     GUILayout.Label("Thanks for Playing!! :D");
        //     if (GUILayout.Button("Restart?")){
        //         // 플레이어 hp 회복하기
        //         isFinished = false;
        //         sceneNum = 0;
        //         SceneManager.LoadScene(0, LoadSceneMode.Single);
        //     }
        //     GUILayout.Space(100);
        //     GUILayout.FlexibleSpace();
        //     GUILayout.EndVertical();
        //     GUILayout.FlexibleSpace();
        //     GUILayout.EndHorizontal();
        //     GUILayout.EndArea();
        // }
    }
    void StartGame()
    {
        //Play Game
        Time.timeScale = 1f;
        //Turn Off Standing Camera
        //GameObject standingCamera = GameObject.FindGameObjectWithTag("MainCamera");
        //standingCamera.SetActive(false);
        //Player Generate
    }
    public static void FinishGame()
    {
        //Stop Game
        Time.timeScale = 1f;
        //GUI Open
        sceneNum ++;
        if(sceneNum == 9){
            isFinished = true;
        }
        else{
            SceneManager.LoadScene(sceneNum, LoadSceneMode.Single);
        }
    }
    public static void RestartStage()
    {
        Debug.Log(sceneNum.ToString());
        //Stop Game
        Time.timeScale = 1f;
        //Reload Stage
        SceneManager.LoadScene(sceneNum, LoadSceneMode.Single);
    }
}
