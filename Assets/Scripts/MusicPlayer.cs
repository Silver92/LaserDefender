using UnityEngine;
using System.Collections;

using UnityEngine.SceneManagement;

public class MusicPlayer : MonoBehaviour
{
    static MusicPlayer instance = null;

    public AudioClip startClip;
    public AudioClip gameClip;
    public AudioClip endClip;

    private AudioSource music;

	private void Awake()
	{
		if (instance != null && instance != this) {
            Destroy (this);
            print ("Duplicate music player self-destructing!");
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
            music = GetComponent<AudioSource>();
            music.clip = startClip;
            music.loop = true;
            music.Play();
        }
	}

	void OnEnable( )
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("MusicPlayer: loaded level "+scene.buildIndex);
        
            music.Stop();               
        if(scene.buildIndex == 0)
            music.clip = startClip;
        if(scene.buildIndex == 1)
            music.clip = gameClip;
        if(scene.buildIndex == 2)
            music.clip = endClip;

        music.loop = true;
        music.Play();
    }
    
	void Start () {

		
	}
}
