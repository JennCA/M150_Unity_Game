using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteSound : MonoBehaviour
{
    public Button musicToggleButton;
    public Sprite spriteMusicOn;
    public Sprite spriteMusicOff;

    // Start is called before the first frame update
    void Start()
    {
        changeVolume();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void pauseMusic() {
        toggleSound(); //updated player prefs.
        changeVolume();
    }

    void changeVolume() {
        if(PlayerPrefs.GetInt("Muted", 0) == 0) {
            AudioListener.volume = 1;
            musicToggleButton.GetComponent<Image>().sprite = spriteMusicOn;
        } else {
            AudioListener.volume = 0;
            musicToggleButton.GetComponent<Image>().sprite = spriteMusicOff;
        }
    }

    public void toggleSound() {
        if(PlayerPrefs.GetInt("Muted", 0) == 0) {
            PlayerPrefs.SetInt("Muted", 1); //AudioListener.volume 1
        } else {
            PlayerPrefs.SetInt("Muted", 0); //AudioListener.volume 0
        }
    }
}
