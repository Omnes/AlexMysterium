using UnityEngine;
using System.Collections;

public class Music_Master : MonoBehaviour {
	
	
	private static Music_Master singleton_instance = null;
	public SceneSound_Packet current;	// current sound_pack (for the scene) in use.
	public int index = 0;
	public float increment = 0.1f;
	private float speakerVolume;
	
	public float FadeInTo = 1.0f;
	public float FadeOutTo = 0.0f;
	private bool m_fadeIn = false;
	private bool m_fadeOut = false;
	private bool m_enabeld;
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
		speakerVolume = speaker.volume;

		newScene();
		switchSong();
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.A)){
			switchSong();
		}
		if(m_fadeIn && !m_fadeOut){
			speakerVolume = speaker.volume;
			fadeIn();
		}else if(!m_fadeIn && m_fadeOut){
			fadeOut();
		}
	}

	public void Enable(bool x){
		m_enabeld = x;
		if(!m_enabeld){
			speakerVolume = speaker.volume;
			m_fadeIn 	= true;
			m_fadeOut = false;
		}else{
			m_fadeIn 	= false;
			m_fadeOut = true;
		}
	}

	void newScene(){
		current = GameObject.FindWithTag("Music_pack").GetComponent<SceneSound_Packet>();
	}
	
	public static Music_Master Instance {
		get { return singleton_instance; }
	}
	
	public void fadeIn(){
		speaker.volume += increment*Time.deltaTime;
		if(speaker.volume > FadeInTo){
			m_fadeIn = false;
		}
	}
	
	public void fadeOut(){
		speaker.volume -= increment*Time.deltaTime;
		
		if(speaker.volume < FadeOutTo){
			m_fadeOut = false;
		}	
	}
	
	public void switchSong(){
		//Debug.Log ("clipCount:" + current.clipCount);
		index = index%current.clipCount;
			//index = index - (current.clipCount * int(index/current.clipCount));
		currentlyPlaying = current.getElement(index);
		speaker.clip = currentlyPlaying;
		speaker.Play();
		
		++index;
	}
}
