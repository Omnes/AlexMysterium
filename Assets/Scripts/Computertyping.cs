using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;

public class Computertyping : MonoBehaviour {

	public Texture2D currentLayout;
	public Texture2D inboxLayout;
	public Texture2D mailLayout;
	public Texture2D returntomail;
	public Texture2D Return;
	public TextAsset mailsource;
	public Font PasswordFont;
	public Font MailFont;
	public int passwordSize = 20;
	public int mailheadlineSize = 20;
	public int mailtextSize = 20;
	public int maxPasswordLength = 25;
	public float passwordPosX = 200;
	public float passwordPosY = 200;
	public float headlinePosX = 100;
	public float headlinePosY = 100;
	public float mailtextPosX = 100;
	public float mailtextPosY = 100;
	public float backgroundPosX = 50;
	public float backgroundPosY = 50;
	public float returntomailPosX = 100f;
	public float returntomailPosY = 100f;
	public string correctPassword = "Alex2001";
	float mailquantity = 3;
	bool computerscreen = false; 
	bool loggedin = false;
	bool readingmail = false;
	string passwordinput = "";
	TextAsset passwordGUI;
	List<Questpair> maillist = new List<Questpair>();
	int currentmail;
	

	// Use this for initialization
	
	void Start () {
		
	}
	
	void Interact(){
		
		computerscreen = true; 
	}
	
	// Update is called once per frame
	 void Update() {
		
		if(!loggedin){  									// Hanterar tangentbordets input
        	foreach (char c in Input.inputString) {
            	if (c == "\b"[0]){							//Suddar ut skit
            	
					if (passwordinput.Length != 0){
        	        	
						passwordinput = passwordinput.Substring(0, passwordinput.Length - 1);
					}
				}
				else if (c == "\n"[0] || c == "\r"[0]){  //enter, KANSKE BEHÖVER LÄGGA TILL ETT KLICK OCKSÅ
			
					if(passwordinput == correctPassword){
						
						loggedin = true;				//Loggat in!!!!
						currentLayout = inboxLayout;
						addemails();
						//SPELA UPP LJUD
					}
				}
           	 	else{
            		passwordinput += c;	
        		}
			}
		}
    }
	
	
	void addemails(){
	
		XmlDocument doc = new XmlDocument(); 
		doc.LoadXml(mailsource.text);				//Laddar vår xml-fil
		
		for(int i = 1; i <= mailquantity; i++){
			
			XmlNode headline = doc.SelectSingleNode("mails/mail/id[contains(text(),'"+ i +"')]").NextSibling;		//Noden för rubriken
			XmlNode mail = headline.NextSibling; 																	//noden för brevtexten
			maillist.Add(new Questpair(headline.InnerText, mail.InnerText));
			maillist[i-1].setID2(i);
			Debug.Log("Added mail"+i);
		}
	}

	
	void OnGUI(){
		
		GUIStyle style = GUIStyle.none;
		
		if(computerscreen == true){   //Gå in på datorn

			GUI.DrawTexture(new Rect(backgroundPosX,backgroundPosY, currentLayout.width,currentLayout.height),currentLayout);

			if (!loggedin){ 					//Lösenord-gui

				style.font = PasswordFont;
				style.fontSize = passwordSize;
				passwordinput = GUI.TextField(new Rect(passwordPosX,passwordPosY,200,20), passwordinput, maxPasswordLength, style);
			}
			else if(loggedin && !readingmail){ 			//Inbox-gui
				
				float tempposY = headlinePosY;
				GUILayout.BeginArea(new Rect(headlinePosX,headlinePosY,currentLayout.width,currentLayout.height));
				GUILayout.BeginVertical();
			
			    foreach(Questpair node in maillist){	//radar upp alla mail
				
				style.font = MailFont;
				style.fontSize = mailheadlineSize;
					
				if(GUI.Button(new Rect(headlinePosX,tempposY,mailheadlineSize*node.mID.Length,mailheadlineSize),node.mID,style)){ //klicka på rubriken för att läsa

						currentLayout = mailLayout;
						readingmail = true;
						currentmail = node.getID2();
				}
					
				tempposY += 30;
				GUILayout.BeginHorizontal();
				GUILayout.BeginVertical();
				
				}
				
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				GUILayout.EndArea();
			}
			else{												//Mail-gui

				Debug.Log("HÄRÄRETTFETTMAIL");

				GUILayout.BeginArea(new Rect(mailtextPosX,mailtextPosY,currentLayout.width,currentLayout.height));
				GUILayout.BeginVertical();

				style.font = MailFont;
				style.fontSize = mailtextSize;

				GUILayout.Label(maillist[currentmail-1].mContent, style);

<<<<<<< HEAD
				if(GUI.Button(new Rect(returntomailPosX,returntomailPosY,returntomail.width,returntomail.height), returntomail,style)){ 	//Return-button
=======
				if(GUI.Button(new Rect(returntomailPosX,returntomailPosY,returntomail.height,returntomail.width), returntomail,style)){ 	//Return-button
>>>>>>> d17862795a2c939a2d185ea457c011e33cdb0646
					
					readingmail = false;
					currentLayout = inboxLayout;
					
				}

				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				GUILayout.EndArea();

			}
			
			if(GUI.Button(new Rect(20,20,Return.width,Return.height), Return,style)){ 	//Return-button
			
				computerscreen = false;	
			}
		}
	}
}
