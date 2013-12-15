using UnityEngine; 
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class MessageWindow : MonoBehaviour { 
	
	public Texture2D Messagelayout;
	public Texture2D FinishedQuest;
	public TextAsset asset; //tillfällig
	public float posX = 150;
	public float posY = 50;
	public Font font;
	public Color color = Color.black;
	public int MainquestSize = 20;
	public int SubquestSize = 15;
	public float subQuestTab= 30;
	bool writeMessage;	
	List<Questpair> questlog = new List<Questpair>(); //Array där alla quests ska sparas

// Use this for initialization 
	void Start () { 
		
		//print(asset.text);
		bool writeMessage = false; 
		addQuest ("2");
		addQuest ("1");
		addSubQuest("1a");
		addSubQuest("1b");
		addSubQuest("1c");
		addSubQuest("1d");
		addSubQuest("2a");
		addSubQuest("2b");
		finishedSubQuest("2b");
		finishedSubQuest("2a");
	} 
	

//Triggar Meddelandet 
	void Interact(){
		
		Debug.Log("Displaying Message");
		writeMessage = true; 
		Time.timeScale = 0; 
	} 
	
// Lägg till en main-quest
	void addQuest(string id){
		
		XmlDocument doc = new XmlDocument(); 
		doc.LoadXml(asset.text);				//Laddar vår xml-fil
		
		XmlNode quest = doc.SelectSingleNode("quests/quest/id[contains(text(),'"+id +"')]").NextSibling; //noden för meddelandet
	
		questlog.Add(new Questpair(id, quest.InnerText));
		
		foreach(Questpair node in questlog){  //Tillfällig testsak woo
			
			Debug.Log (node.mContent);
		}
	}
	
//Lägg till en subquest
	void addSubQuest(string subquest){
		
		XmlDocument doc = new XmlDocument(); 
		doc.LoadXml(asset.text);				//Laddar vår xml-fil
		
		XmlNode quest = doc.SelectSingleNode("quests/quest/id[contains(text(),'" + subquest +"')]").NextSibling; //noden för meddelandet
	
		foreach(Questpair node in questlog){
		
			if(subquest.StartsWith(node.mID)){
				
				node.addSubQuest(new Questpair(subquest, quest.InnerText));
				//foreach(Questpair subNode in node.mQuestlog){
				//	questlog.Add(new Questpair(subquest, ));
			}
		}
	}
	
//Klarat en mainquest
	void finishedQuest(string id){
		
		foreach(Questpair node in questlog){
		
			if(node.mID == id){
				
				node.mContent = "hej" + node.mContent + "hej";
			}
		}
	}

//Klarat en subquest
	void finishedSubQuest(string subquest){
	
		foreach(Questpair node in questlog){
		
			if(subquest.StartsWith(node.mID)){
				
				foreach(Questpair subNode in node.mQuestlog){
				
					if(subNode.mID == subquest){ 
						
						subNode.mFinished = true;
					}
				}
			}
		}
	}
	
//Ritar meddelandet 
	void OnGUI(){ 
		
		//XmlNodeList quest = doc.GetElementsByTagName("quest"); 	//array med alla quests??
	
		//XmlNode quest = ("quests/quest/id[contains(text(),'1')]").NextSibling;
		
	/*	foreach(XmlNode node in quest){
			
			if(node.FirstChild.InnerText == "2"){  //Vettefan
				
				//questlog.Add(node.FirstChild.NextSibling.InnerText); //Återigen vettefan
				//Debug.Log(node.FirstChild.NextSibling.InnerText);
			}
		}
		*/
		
		//doc.GetElementsByTagName("quest1").;
		
		GUIStyle style = GUIStyle.none; //Gui-layout som tar bort ful bakgrund
		style.font = font;				//fin comic sans
		//style.font.material.color = color;
		
		/*if(Time.time > 10){
		
			finishedQuest("2");
			
			foreach(Questpair node in questlog){  //Tillfällig testsak woo
		
				Debug.Log (node.mContent);
			}
		}
		*/
		if(writeMessage){ 
			float tempposY = posY;
			
			GUI.DrawTexture(new Rect(posX,tempposY, Messagelayout.width,Messagelayout.height),Messagelayout);
			
			//GUI.Box(new Rect(posX + 10,tempposY + 10, Messagelayout.width-10,Messagelayout.height-10), "",style);
			
			GUILayout.BeginArea(new Rect(posX + 10,tempposY + 10,Messagelayout.width-10,Messagelayout.height-10));
			GUILayout.BeginVertical();
			
			foreach(Questpair node in questlog){ //loopar igenom alla mainquests
				
				style.fontSize = MainquestSize;
				GUILayout.Label(node.mContent,style);
				GUILayout.BeginHorizontal();
				GUILayout.Space(subQuestTab);
				GUILayout.BeginVertical();
				
				foreach(Questpair subnode in node.mQuestlog){ //loopar igenom alla subquests
					GUILayout.BeginHorizontal();
					style.fontSize = SubquestSize;
					if(subnode.mFinished == true){
						GUILayout.Space(-FinishedQuest.width);
						GUILayout.Box(FinishedQuest,style);
					}
					GUILayout.Label(subnode.mContent,style);
					GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();
				}
				
				GUILayout.EndVertical();
				GUILayout.EndHorizontal();
				
				/*if(GUI.Button(new Rect(posX,posY, Messagelayout.width,Messagelayout.height),"",style)){ 
					
					writeMessage = false; 
					Time.timeScale = 1; 
				}*/
			} 
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.EndArea();
			if(GUI.Button(new Rect(posX,posY, Messagelayout.width,Messagelayout.height),"",style)){ 
					
					writeMessage = false; 
					Time.timeScale = 1; 
				}
		}/*
		if(writeMessage){ 
			float tempposY = posY;
			
			GUI.DrawTexture(new Rect(posX,tempposY, Messagelayout.width,Messagelayout.height),Messagelayout);
			
			GUI.Box(new Rect(posX + 10,tempposY + 10, Messagelayout.width-10,Messagelayout.height-10), "",style);
			
			
			foreach(Questpair node in questlog){
				
				GUI.Label(new Rect(posX + 10,tempposY + 10, Messagelayout.width-10,Messagelayout.height-10), node.mContent,style);
				
				foreach(Questpair subnode in node.mQuestlog){
					
					GUI.Label(new Rect(posX + 10,tempposY + 30, Messagelayout.width-10,Messagelayout.height-10), "\t" + subnode.mContent,style);
					
					tempposY += 30;
				}
				
				tempposY += 30;
				/*if(GUI.Button(new Rect(posX,posY, Messagelayout.width,Messagelayout.height),"",style)){ 
					
					writeMessage = false; 
					Time.timeScale = 1; 
				}
			} 
		}*/
	} 
}