using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music_script : MonoBehaviour {

    public AudioClip[] tracks;
    public AudioSource source;
    public float musicVolume;
    public List<int> playedTracks = new List<int>();

    public static music_script instance = null;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        musicVolume = 1;
	}

    private void Update()
    {
        if (playerMovement.instance.gameStart)
        {
            if (!source.isPlaying)
            {
                NextSong();
            }

        }
    }


    public void PlaySong(int t)
    {
        source.Stop();
        source.PlayOneShot(tracks[t], musicVolume * 0.85f);
        playedTracks.Add(t);
      
        
    }

    void NextSong()
    {
        int r = Random.Range(0, tracks.Length);
        Debug.Log("Playing: " + tracks[r].name);
        if (playedTracks.Contains(r))
            NextSong();
        else
            PlaySong(r);
    }

 
}
