using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    private GameObject music;

    void Awake() {
        music = GameObject.FindWithTag("Music");
        if(tag == "Music") {
            DontDestroyOnLoad(music);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
