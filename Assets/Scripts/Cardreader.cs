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
	
	// Use this for initialization
	void Start () {
		
		renderer.material.mainTexture = yellow;
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
		
		//GUI.DrawTexture(new Rect(50,50,thingy.width,thingy.height),thingy);
		
		float temppos = 0;
		
		for(int i = 0; i < Digits.Length; i++){
			
			if(GUI.Button(new Rect(ButtonStartX + temppos,ButtonStartY,30,30),"" + i)){
				
				if(count < 4){
					
					clicked.Add(i);
					
					count += 1;
				}
			}
			temppos += 50;
		}
		
		for(int i = 0; i < clicked.Count; i++){
			
			GUI.DrawTexture(new Rect(DisplayposX + i*digitSizes.x, DisplayposY + i*YOffsetBetweenThem , digitSizes.x ,  digitSizes.y - i),Digits[clicked[i]]);
		}
	}
}
