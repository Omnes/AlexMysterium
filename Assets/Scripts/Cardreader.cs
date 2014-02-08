using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Cardreader : MonoBehaviour {
	
	public Texture2D[] Digits = new Texture2D[10];
	public Texture2D green;
	public Texture2D red;
	public Texture2D yellow;
	public float ButtonStartX = 100;
	public float ButtonStartY = 100;
	public float DisplayposX = 100;
	public float DisplayposY = 50;
	public float YOffsetBetweenThem = 5;
	public float buttondistanceX = 50;
	public float buttondistanceY = 50;
	public Texture2D thingy;
	public float digitsize;
	public Vector2 digitSizes = new Vector2(64,64);
	public List<int> correctcode = new List<int>(4);
	public float delay = 1;
	int count = 0; 
	bool unlocked = false;
	public List<int> clicked = new List<int>();
	float clock;
	bool clockhasstarted = false;
	bool carddrawn = false;
	MessageWindow quest;
	
	// Use this for initialization
	void Start () {
		
		renderer.material.mainTexture = yellow;
		quest = gameObject.GetComponent<MessageWindow>();
	}
	
	// Update is called once per frame
	void Update () {
		
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
				renderer.material.mainTexture = yellow;
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

			renderer.material.mainTexture = green;

			float temppos = 0;
			float newrow = 1;
			float rowdistance = 0;

			for(int i = 1; i < Digits.Length; i++){									//Knapparna
				
				if(GUI.Button(new Rect(ButtonStartX + temppos,ButtonStartY + rowdistance,50,30),"" + i)){
					if(carddrawn){
						if(count < 4){
							clicked.Add(i);
							count += 1;
						}
					}
				else{
					//spela upp ljud???
					quest.addQuest("1"); //ändra siffran till rätt quest sen
				}
				}

				temppos += buttondistanceX;

				if(newrow >= 3){
					rowdistance +=buttondistanceY;
					newrow = 0;
					temppos = 0;
				}
				newrow += 1;
			}

			if(GUI.Button(new Rect(ButtonStartX + buttondistanceX,ButtonStartY + buttondistanceY*3,50,30),"" + 0)){
				
				if(carddrawn){
					if(count < 4){	
						clicked.Add(0);
						count += 1;
					}
				}
				else{
					quest.addQuest("1"); //ändra siffran till rätt quest sen
				}
			}
			for(int i = 0; i < clicked.Count; i++){
				
				GUI.DrawTexture(new Rect(DisplayposX + i*digitSizes.x, DisplayposY + i*YOffsetBetweenThem , digitSizes.x ,  digitSizes.y - i),Digits[clicked[i]]);
			}
		//}
	}
}
