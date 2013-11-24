using UnityEngine;
using System.Collections;

public class Music_Master : MonoBehaviour {
	
	
	private static Music_Master singleton_instance = null;
	public SceneSound_Packet current;	// current sound_pack (for the scene) in use.
	public int index = 0;
	
	public AudioClip currentlyPlaying;
	public AudioSource speaker;
	//private AudioSettings settings;
	
	void Awake() {
		if (singleton_instance != null && singleton_instance != this) {
			Destroy(this.gameObject);
			return;
		} else {
			singleton_instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
		
		speaker = gameObject.GetComponent<AudioSource>();
	}
	// Use this for initialization
	void Start () {
		newScene();
		switchSong();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			switchSong();
		}
	}
	
	void newScene(){
		current = GameObject.FindWithTag("Music_pack").GetComponent<SceneSound_Packet>();
	}
	
	public static Music_Master Instance {
		get { return singleton_instance; }
	}
	
	public void fadeIn(){
		if(currentlyPlaying){
			
		}
	}
	
	public void fadeOut(){
			
	}
	
	public void switchSong(){
		Debug.Log ("clipCount:" + current.clipCount);
		index = index%current.clipCount;
			//index = index - (current.clipCount * int(index/current.clipCount));
		currentlyPlaying = current.getElement(index);
		speaker.clip = currentlyPlaying;
		speaker.Play();
		
		++index;
	}
}
