using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class audio : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(transform.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        int sceneID = SceneManager.GetActiveScene().buildIndex;
        if (sceneID>2){
            GameObject.Destroy(this.gameObject);
        }
    }
}
