using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class intro : MonoBehaviour
{
    static int sceneNum = 1;
    private float time_current;
    // Start is called before the first frame update
    void Start()
    {
        time_current = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
         if (Time.time - time_current > 8f)
        {
            FinishGame();
        }
    }

    public static void FinishGame()
    {
        sceneNum++;
        SceneManager.LoadScene(sceneNum, LoadSceneMode.Single);
    }
}
