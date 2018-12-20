using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GM : MonoBehaviour {


    public GameObject Menu;
    public Slider volume;
    public Text volumeText;
    

	// Use this for initialization
	void Start () {
        Menu.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ChangeVolume()
    {
        music_script.instance.musicVolume = volume.value;
        music_script.instance.source.volume = volume.value;
        volumeText.text = "Volume " + volume.value;
    }

    public void ToggleMenu()
    {
        if (!Menu.active)
        {
            Menu.SetActive(true);
            volumeText.text = "Volume " + music_script.instance.musicVolume;
            volume.value = music_script.instance.musicVolume;
        }else if (Menu.active)
        {
            Menu.SetActive(false);
        }
        
    }


    public void ResetGame() 
    {
        Application.LoadLevel(Application.loadedLevel);
        //p.transform.position = playerMovement.instance.respawn.position;
    }

}
