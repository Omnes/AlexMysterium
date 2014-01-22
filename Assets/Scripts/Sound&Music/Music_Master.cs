using UnityEngine;
using System.Collections;

public class Music_Master : MonoBehaviour {
	
	
	private static Music_Master singleton_instance = null;
	public SceneSound_Packet current;	// current sound_pack (for the scene) in use.
	public int index = 0;
	
	public AudioClip currentlyPlaying;
	public AudioSource speaker;
	
	public bool enabled = true;
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
		if(enabled){
			switchSong();
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			switchSong();
		}
		if(Input.GetKeyDown(KeyCode.S) && speaker.isPlaying){
			speaker.Pause();
		}
		if(Input.GetKeyDown(KeyCode.D) && !speaker.isPlaying){
			speaker.Play();
		}
		if(!speaker.isPlaying && enabled){// simple song switcher
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
			//##############DO THIS
		}
	}
	
	public void fadeOut(){
			//##############DO THIS
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
	
	public void turnOnOff(){
		enabled = !enabled;
	}
}
