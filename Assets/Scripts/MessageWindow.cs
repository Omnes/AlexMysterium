using UnityEngine; 
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class MessageWindow : MonoBehaviour { 
	
	public Texture2D Messagelayout;
	public Texture2D FinishedQuest;
	public TextAsset asset; //tillfällig
	public AudioSource newObjective;
	//public AudioClip addQuestSound;
	public float posX = 150;
	public float posY = 50;
	public Font font;
	public Color color = Color.black;
	public int MainquestSize = 20;
	public int SubquestSize = 15;
	public float subQuestTab= 30;
	public Rect textfield;
	public Rect texturefield;



	bool writeMessage;	
	List<Questpair> questlog = new List<Questpair>(); //Array där alla quests ska sparas
	

// Use this for initialization 
	void Start () { 
		//newObjective = gameObject.GetComponent<AudioSource>();	
		//print(asset.text);
		bool writeMessage = false; 
	} 

	void showMessageWindow(){
		Debug.Log("Displaying Message");
		writeMessage = true; 
		Time.timeScale = 0; 
	}

	Rect makeRect(Rect r){
		return new Rect(Screen.width/r.x,Screen.height/r.y,Screen.width/r.width,Screen.height/r.height);
	}

	public void Update(){

		if(Input.GetKeyDown(KeyCode.Q)){

			addQuest("1");
			addSubQuest("1a");
			addSubQuest("1b");
			addSubQuest("1c");
			addSubQuest("1d");
			addSubQuest("1e");
			addSubQuest("1f");
		}

		if(Input.GetKeyDown(KeyCode.F)){
			
			finishedQuest("1");
			finishedSubQuest("1a");
			finishedSubQuest("1b");
			finishedSubQuest("1c");
			finishedSubQuest("1d");
			finishedSubQuest("1e");
			finishedSubQuest("1f");
		}
	}

// Lägg till en main-quest
	public void addQuest(string id){
		
		Debug.Log("quest added");
		
		XmlDocument doc = new XmlDocument(); 
		doc.LoadXml(asset.text);				//Laddar vår xml-fil
		
		XmlNode quest = doc.SelectSingleNode("quests/quest/id[contains(text(),'"+id +"')]").NextSibling; //noden för meddelandet
	
		bool dontadd = false;

		foreach(Questpair node in questlog){  //Ser till att inte samma quest läggs till flera gånger
			if(id == node.getID()){
				dontadd = true;
			}
		}	

		if(!dontadd){
			newObjective.Play();
			questlog.Add(new Questpair(id, quest.InnerText));
		}

		Debug.Log("about to play audio");
	//	audio.clip = addQuestSound;
	//	audio.timeSamples = 5000;
	//	audio.Play();

		Debug.Log ("audio played");
	}
	
//Lägg till en subquest
	public void addSubQuest(string subquest){

		Debug.Log ("hej");
		XmlDocument doc = new XmlDocument(); 
		doc.LoadXml(asset.text);				//Laddar vår xml-fil
		
		XmlNode quest = doc.SelectSingleNode("quests/quest/id[contains(text(),'" + subquest +"')]").NextSibling; //noden för meddelandet
	
		bool dontadd = false;

		foreach(Questpair node in questlog){
			Debug.Log("1");
			if(subquest.StartsWith(node.mID)){
				Debug.Log ("2");
				foreach(Questpair sub in node.mQuestlog){
					Debug.Log("3");
					if(sub.getID() == subquest){
						Debug.Log("added nothing, already exists");
						dontadd = true;
					}
				}
				if(!dontadd){
					newObjective.Play();
					node.addSubQuest(new Questpair(subquest, quest.InnerText));
					Debug.Log ("added" + quest.InnerText + "quest");
					//foreach(Questpair subNode in node.mQuestlog){
					//	questlog.Add(new Questpair(subquest, ));
				}
			}
		}
	}
	
//Klarat en mainquest
	void finishedQuest(string id){
		
		foreach(Questpair node in questlog){
		
			if(node.mID == id){
				
				node.mContent =  node.mContent;
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
			
			GUI.DrawTexture(makeRect(texturefield),Messagelayout); //FUNKAR INTE?!
			//GUILayout.BeginArea(new Rect(posX + 10,tempposY + 10,Messagelayout.width-10,Messagelayout.height-10));
			GUILayout.BeginArea(makeRect(textfield));
			GUILayout.BeginVertical();
			//GUILayout.Space(distancetext);
			
			foreach(Questpair node in questlog){ //loopar igenom alla mainquests
				
				style.fontSize = MainquestSize;
				GUILayout.Label(node.mContent,style);
				GUILayout.BeginHorizontal();
				GUILayout.BeginVertical();
				
				foreach(Questpair subnode in node.mQuestlog){ //loopar igenom alla subquests
					GUILayout.BeginHorizontal();
					GUILayout.Space(subQuestTab);
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
		
			} 
			GUILayout.FlexibleSpace();
			GUILayout.EndVertical();
			GUILayout.EndArea();
			if(GUI.Button(makeRect(texturefield),"",style)){ //Stänger ner questlogen
					
					writeMessage = false; 
					Time.timeScale = 1; 
				}
		}
	} 
}