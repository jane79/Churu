using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class IntroGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

      public void Restart()
    {
        SceneManager.LoadScene(0);
    }
    public void gamestart()
    {
        SceneManager.LoadScene(1);
    }
    public void End()
    {
        Application.Quit();
    }
}
