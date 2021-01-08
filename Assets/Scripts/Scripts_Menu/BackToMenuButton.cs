using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenuButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerButtonBehavior(int i) {
		switch (i) {
		default:
		case(0):
			SceneManager.LoadScene("MainMenu");	
			break;
		}
	}
}
