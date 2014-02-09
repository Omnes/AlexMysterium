using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cardreader : MonoBehaviour {
	
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
	bool carddrawn = false;
	MessageWindow quest;

	
	// Use this for initialization
	void Start () {
		
		renderer.material.mainTexture = idle;
		quest = gameObject.GetComponent<MessageWindow>();
	}

	/*void Interact(){}

	void UseItem(Item item){
		Debug.Log(item.name);
		if(item.name == keycard){
			carddrawn = true;
			renderer.material.mainTexture = yellow;
		}
	}
*/

	Rect makeRect(Rect r){
		return new Rect(Screen.width/r.x,Screen.height/r.y,Screen.width/r.width,Screen.height/r.height);
	}

	// Update is called once per frame
	void Update () {

		if(Input.GetKeyUp(KeyCode.C)){

			Debug.Log ("Key was pressed");
			if(!carddrawn){
			carddrawn = true;
			renderer.material.mainTexture = yellow;
			}

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
			Debug.Log("UNLOCKED!");
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

		GUILayout.BeginArea(makeRect(buttonField));
		GUILayout.BeginVertical();


		for(int i = 0; i < 3; i++){
		//GUILayout.FlexibleSpace();//Knapparna
			GUILayout.BeginHorizontal();
			for(int j = 3*i; j < i*3 +3; j++){
				if(GUILayout.Button("" + (j+1),GUILayout.ExpandHeight(true))){
					if(carddrawn){
						if(count < 4){
							clicked.Add(j+1);
							count++;
						}
					}else{
						quest.addQuest("1"); //ändra siffran till rätt quest sen
					}
				}
			}
			GUILayout.EndHorizontal();
		//GUILayout.FlexibleSpace();
		}
	//GUILayout.FlexibleSpace();
		GUILayout.BeginHorizontal();
		GUILayout.Label("");
		if(GUILayout.Button("" + 0,GUILayout.ExpandHeight(true))){
			if(carddrawn){
				if(count < 4){
					clicked.Add(0);
					count++;
				}
			}else{
				quest.addQuest("1"); //ändra siffran till rätt quest sen
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
			GUILayout.Box(Digits[clicked[i]],GUILayout.Width(Screen.width/numberSize.x),GUILayout.Height(Screen.height/numberSize.y));
		}
		GUILayout.EndHorizontal();
		GUILayout.EndArea();
	//}
	}
}
