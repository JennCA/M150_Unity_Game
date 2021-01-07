using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuBehavior : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void triggerMenuBehavior(int i) {
		switch (i) {
		default:
		case(0):
			SceneManager.LoadScene("SampleScene");	
			break;
		case(1):
			SceneManager.LoadScene("Settings");
			break;
        case(2):
			Application.Quit();
			break;
		}

	}
}
