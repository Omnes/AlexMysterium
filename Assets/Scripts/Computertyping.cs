using UnityEngine;
using System.Collections;
using System.Xml;
using System.Collections.Generic;
using System.Text.RegularExpressions;

public class Computertyping : MonoBehaviour {

	public Texture2D currentLayout;
	public Texture2D inboxLayout;
	public Texture2D Return;
	public TextAsset mailsource;
	public Font PasswordFont;
	public Font MailFont;
	public int passwordSize = 20;
	public int mailheadlineSize = 20;
	public int mailtextSize = 20;
	private int maxPasswordLength = 10;
	public Rect passwordField;
	public Rect headlineField;
	public Rect mailtextField;

	public string correctPassword = "Alex2001";
	float mailquantity = 3;
	public bool computerscreen = false; 
	bool loggedin = false;
	bool readingmail = false;
	string passwordinput = "";
	TextAsset passwordGUI;
	List<Questpair> maillist = new List<Questpair>();
	int currentmail = 1;
	private GameObject screen;

	//seans
	public bool compPower = true;
	public Transform desktopPic;
	public int correctMail = 2;
	private bool played = false;
	public AudioSource aSource;
	public AudioClip mailClip;
	public AudioClip powerOutSound;
	public AudioClip flashAudio;
	//
	private float currentTime;
	public float powerOutTime = 1f;
	private bool initiatePOut = false;
	private float timeBeforeGasp = 0.3f;
	private bool playFlashSound = false;
	private float currentFlashTime;
	private bool haveFlashlight = false; // ändra denna så den är falsk
	

	// Use this for initialization
	
	void Start () {
		screen = GameObject.Find ("computerscreen");
		screen.renderer.enabled = false;
	}

	public void setComputerScreen(bool toggle){
		computerscreen = toggle;
	}

	void ActivateStuff(){
		if(compPower){
			setComputerScreen(true);
			screen.renderer.enabled = true;
			gameObject.GetComponent<ComputerSound_script>().playSound();
		}
	}
	
	void DeactivateStuff(){
		Debug.Log ("OUTOFTHIS");
		setComputerScreen(false);
	}
	
	// Update is called once per frame
	 void Update() {

		if(initiatePOut){
			initiatePowerOut();
		}

		//haveFlashlight ska sättas i initiatePowerOut
		if(playFlashSound && !haveFlashlight){
			if(currentFlashTime + powerOutSound.length + 1f < Time.time){
				doPlayFlashSound();
			}
		}

		if(compPower){
			if(!loggedin){  									// Hanterar tangentbordets input
	        	foreach (char c in Input.inputString) {
					if (c == "\n"[0] || c == "\r"[0]){  //enter, KANSKE BEHÖVER LÄGGA TILL ETT KLICK OCKSÅ
						if(passwordinput == correctPassword){
							loggedin = true;				//Loggat in!!!!f
							currentLayout = inboxLayout;

							screen.renderer.material.SetTexture("_MainTex",inboxLayout);
							addemails();
							//SPELA UPP LJUD
						}else{
							passwordinput = "";
						}
					}
				}
				if(passwordinput.Length > maxPasswordLength){
					passwordinput = passwordinput.Substring(0, passwordinput.Length - 1);
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

	Rect makeRect(float x,float y,float w,float h){
		return new Rect(Screen.width/x,Screen.height/y,Screen.width/w,Screen.height/h);
	}
	Rect makeRect(Rect r){
		return new Rect(Screen.width/r.x,Screen.height/r.y,Screen.width/r.width,Screen.height/r.height);
	}

	
	void OnGUI(){

		//GUI.Box(makeRect(passwordField),"passfield");
		//GUI.Box(makeRect(mailtextField),"mailtextField");
		//GUI.Box(makeRect(headlineField),"headlineField");

		
		GUIStyle style = GUIStyle.none;
		
		if(computerscreen == true){   //Gå in på datorn


			//GUI.DrawTexture(new Rect(backgroundPosX,backgroundPosY, currentLayout.width,currentLayout.height),currentLayout);

			if (!loggedin){ 					//Lösenord-gui

				style.font = PasswordFont;
				style.fontSize = passwordSize;
				GUI.SetNextControlName ("PasswordField");
				passwordinput = GUI.TextField(makeRect(passwordField), passwordinput,maxPasswordLength, style);
				passwordinput = Regex.Replace(passwordinput, @"[^a-zA-Z0-9]", "");  //remove unallowed Chars
				GUI.FocusControl("PasswordField");

			}
			else if(loggedin && !readingmail){ 			//Inbox-gui
				
				//float tempposY = headlinePosY;
				GUILayout.BeginArea(makeRect(headlineField));
				GUILayout.BeginVertical();
			
			    foreach(Questpair node in maillist){	//radar upp alla mail
					
					style.font = MailFont;
					style.fontSize = mailheadlineSize;

					GUILayout.BeginHorizontal();
						if(GUILayout.Button(node.mID,style)){ //klicka på rubriken för att läsa
								currentmail = node.getID2();
							if(currentmail == correctMail && !played){
								aSource.clip = mailClip;
								//Activate sound
								aSource.Play();
								currentTime = Time.time;
								initiatePOut = true;
								played = true;
								
							}
						}
						//GUILayout.FlexibleSpace();
					GUILayout.EndHorizontal();

						
					//tempposY += 30;
				
				}

				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				GUILayout.EndArea();


				GUILayout.BeginArea(makeRect(mailtextField));
				GUILayout.BeginVertical();
				
				style.font = MailFont;
				style.fontSize = mailtextSize;
				if(currentmail > 0){
					GUILayout.Label(maillist[currentmail-1].mContent, style);
				}
				
				
				GUILayout.FlexibleSpace();
				GUILayout.EndVertical();
				GUILayout.EndArea();
			}
		}
	}

	void setPowerOut(bool power){
		computerscreen = power;
		compPower = power;
		desktopPic.gameObject.SetActive(false);
	}

	void initiatePowerOut(){
		if(currentTime + powerOutTime < Time.time && initiatePOut){
			GameObject mastermind = GameObject.FindGameObjectWithTag("Mastermind");
			mastermind.SendMessage("powerOut");
			aSource.clip = powerOutSound;
			aSource.PlayDelayed(timeBeforeGasp);
			initiatePOut = false;
			//for flashlightsoudn
			haveFlashlight = mastermind.GetComponent<Inventory>().checkItemSupply("item_flashlight",1);
			currentFlashTime = Time.time;
			playFlashSound = true;
		}
	}
	void doPlayFlashSound(){
		aSource.clip = flashAudio;
		aSource.Play();
		playFlashSound = false;
	}

}
