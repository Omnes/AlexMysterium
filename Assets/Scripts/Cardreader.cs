﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cardreader : MonoBehaviour {

	public GameObject DoorToOpen;
	public Texture2D[] Digits = new Texture2D[10];
	public Texture2D green;
	public Texture2D red;
	public Texture2D yellow;
	public Texture2D idle;
	public float YOffsetBetweenThem = 5;
	public Texture2D thingy;
	public float digitsize;
	public Vector2 digitSizes = new Vector2(64,64);
	public List<int> correctcode = new List<int>(4);
	public float delay = 1;
	public string keycard = "keycard";
	public Rect buttonField;
	public Rect displayField;
	public Vector2 numberSize;
	int count = 0; 
	bool unlocked = false;
	public List<int> clicked = new List<int>();
	float clock;
	bool clockhasstarted = false;
	public bool carddrawn = false;
	MessageWindow quest;
	bool hascard;

	private bool visited = false;
	public AudioSource m_audioSource;
	public AudioClip m_needCode_sound;
	public AudioClip m_drawcard;
	public AudioClip[] beeps = new AudioClip[4];
	ItemUseStates checkifgotcode;
	
	// Use this for initialization
	void Start () {

		checkifgotcode = GameObject.Find ("MasterMind").GetComponent<ItemUseStates>();
	//	hascard = GetComponent<ItemUseStates>().;
		renderer.material.mainTexture = idle;
		//quest = gameObject.GetComponent<MessageWindow>();
		quest = GameObject.Find ("MasterMind").GetComponent<MessageWindow>();
	}

	/*void Interact(){}

	void UseItem(Item item){
		//Debug.Log(item.name);
		if(item.name == keycard){
			carddrawn = true;
			renderer.material.mainTexture = yellow;
		}
	}
*/

	Rect makeRect(Rect r){
		return new Rect(Screen.width/r.x,Screen.height/r.y,Screen.width/r.width,Screen.height/r.height);
	}

	public void drawCard(){
		playSound(m_drawcard);
		if(!GameObject.Find("MasterMind").GetComponent<ItemUseStates>().powerout){
			carddrawn = true;
			renderer.material.mainTexture = yellow;
		}
	}

	// Update is called once per frame
	void Update () {

		if(!visited){
			//Debug.Log("visisted");
			if(!getCodeValid()){
				//Debug.Log("valid code");
				if(!checkifgotcode.foundCode){
				playSound(m_needCode_sound);
					quest.addSubQuest("1b");
				}
			}
			visited = true;
		}

		if(count == 4){
			
			unlocked = checkifcorrectcode();
			
			if(!clockhasstarted){
				
				clock = Time.time;	//gör en kort delay
				clockhasstarted = true;
			}
			
			if(Time.time - clock > delay){
				
				clicked.Clear();
				count = 0;
				clockhasstarted = false;
				carddrawn = false;
				renderer.material.mainTexture = idle;
			}
		}
	}


	bool checkifcorrectcode(){
		
		bool checkcode = true;
		
		for(int i = 0; i <count; i++){
			
			if(clicked[i] != correctcode[i]){
				
				checkcode = false;
			}
		}
		
		if(checkcode){
			
			renderer.material.mainTexture = green;
			DoorToOpen.GetComponent<Interact_Door>().locked = false;
			quest.finishedSubQuest("1d");
			//Debug.Log("UNLOCKED!");
		}
		else{
			
			renderer.material.mainTexture = red;
		}
		return checkcode;
	}


	void OnGUI(){
		
		//if(carddrawn){
		//GUI.DrawTexture(new Rect(50,50,thingy.width,thingy.height),thingy);

		float temppos = 0;
		float newrow = 1;
		float rowdistance = 0;
		GUIStyle style = GUIStyle.none;

		GUILayout.BeginArea(makeRect(buttonField));
		GUILayout.BeginVertical();


		for(int i = 0; i < 3; i++){
		//GUILayout.FlexibleSpace();//Knapparna
			GUILayout.BeginHorizontal();
			for(int j = 3*i; j < i*3 +3; j++){
				if(GUILayout.Button("",style,GUILayout.ExpandHeight(true))){
					if(carddrawn){
						if(count < 4){
							int rand = Random.Range(0,3);
							playSound(beeps[rand]);
							clicked.Add(j+1);
							count++;
						}
					}else{
						//quest.addQuest("1"); //ändra siffran till rätt quest sen
					}
				}
			}
			GUILayout.EndHorizontal();
		//GUILayout.FlexibleSpace();
		}
	//GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.Label("");
		if(GUILayout.Button("",style,GUILayout.ExpandHeight(true))){
			if(carddrawn){
				if(count < 4){
					int rand = Random.Range(0,3);
					playSound(beeps[rand]);
					clicked.Add(0);
					count++;
				}
			}else{
			//	quest.addQuest("1"); //ändra siffran till rätt quest sen
			}
		}
		GUILayout.Label("");
		//GUILayout.FlexibleSpace();
		GUILayout.EndHorizontal();

		//GUILayout.FlexibleSpace();
		GUILayout.EndVertical();
		GUILayout.EndArea();

		GUILayout.BeginArea(makeRect(displayField));
		GUILayout.BeginHorizontal();
		for(int i = 0; i < clicked.Count; i++){
			GUILayout.Box(Digits[clicked[i]],style,GUILayout.Width(Screen.width/numberSize.x),GUILayout.Height(Screen.height/numberSize.y));
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	//}
	}

	public bool getCodeValid(){
		bool valid = GameObject.FindGameObjectWithTag("Mastermind").GetComponent<ItemUseStates>().passcode;
		return valid;
	}

	public void playSound(AudioClip ac){
		m_audioSource.clip = ac;
		m_audioSource.Play();
	}
}
