using UnityEngine; 
using System.Collections;

public class MessageWindow : MonoBehaviour { 
	
	public Texture2D Messagelayout;
	public TextAsset asset;
	public float posX = 100;
	public float posY = 100;
	public Font font;
	public Color color = Color.black;
	public int size = 12;
	bool writeMessage;

// Use this for initialization 
	void Start () { 
		
		//print(asset.text);
		bool writeMessage = false; 
	} 
	

//Triggar Meddelandet 
	void Interact(){
		
		Debug.Log("Displaying Message");
		writeMessage = true; 
		Time.timeScale = 0; 
	} 
	
//Ritar meddelandet 
	void OnGUI(){ 
		
		
		GUIStyle style = GUIStyle.none;
		style.font = font;
		style.fontSize = size;
		//style.font.material.color = color;
		
		
		if(writeMessage){ 
			
			GUI.DrawTexture(new Rect(posX,posY, Messagelayout.width,Messagelayout.height),Messagelayout); 
			
			GUI.Box(new Rect(posX + 10,posY + 10, Messagelayout.width-10,Messagelayout.height-10), asset.text,style);
			
			if(GUI.Button(new Rect(posX,posY, Messagelayout.width,Messagelayout.height),"",style)){ 
				
				writeMessage = false; 
				Time.timeScale = 1; 
			} 
		} 
	} 
}