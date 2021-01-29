using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public GameObject A;
    Transform AT;
    // Start is called before the first frame update
    void Start()
    {
        AT = A.transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,AT.position,2f * Time.deltaTime);
        transform.Translate (0, 0, -10); //카메라를 원래 z축으로 이동
    }
}
