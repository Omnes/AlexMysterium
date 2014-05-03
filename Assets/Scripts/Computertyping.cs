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

	public AudioSource audioS_sound;
	public AudioSource audioS_voice;

	public AudioClip mail_sound;
	public AudioClip key_space_sound;
	public AudioClip key_enter_sound;
	public AudioClip key_any_sound;

	public AudioClip[] clip_array;

	private float currentTime;
	public float powerOutDelay = 8f;
	private bool initiatePOut = false;
	private bool haveFlashlight = false; // ändra denna så den är falsk
	private string tempPass = null;
	private bool enterKey = false;
	MessageWindow quest;

	bool added1 = false;
	bool added2 = false;

	//realk mastermind uknow
	GameObject m_mastermind;


	// Use this for initialization
	
	void Start () {
	
		m_mastermind = GameObject.Find("MasterMind");
		loggedin = m_mastermind.GetComponent<ItemUseStates>().isLoggedIn;
		screen = GameObject.Find ("computerscreen");
		screen.renderer.enabled = false;
		if(loggedin){
			screen.renderer.material.SetTexture("_MainTex",inboxLayout);
			addemails();
		}
		played = m_mastermind.GetComponent<ItemUseStates>().poweroutOcurred;

		quest = GameObject.Find ("MasterMind").GetComponent<MessageWindow>();

	}

	public void setComputerScreen(bool toggle){
		computerscreen = toggle;
	}

	void ActivateStuff(){
		bool valid = GameObject.FindGameObjectWithTag("Mastermind").GetComponent<ItemUseStates>().powerout;
		if(!valid){
			if(compPower){
				setComputerScreen(true);
				screen.renderer.enabled = true;
				gameObject.GetComponent<ComputerSound_script>().playSound();
			}
		}
	}
	
	void DeactivateStuff(){
		//Debug.Log ("OUTOFTHIS");
		setComputerScreen(false);
	}
	
	// Update is called once per frame
	 void Update() {

		if(initiatePOut){
			initiatePowerOut();
		}
			
		if(compPower){
			if(!loggedin){  									// Hanterar tangentbordets input
	        	foreach (char c in Input.inputString) {
					if (c == "\n"[0] || c == "\r"[0]){  //enter, KANSKE BEHÖVER LÄGGA TILL ETT KLICK OCKSÅ
						if(passwordinput == correctPassword){
							m_mastermind.GetComponent<ItemUseStates>().password = true;
							loggedin = true;				//Loggat in!!!!
							m_mastermind.GetComponent<ItemUseStates>().isLoggedIn = loggedin;
							quest.finishedSubQuest("1c");
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
			//Debug.Log("Added mail"+i);
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
				if(!added1){
					quest.addSubQuest("1c");
					added1 = true;
				}
				style.font = PasswordFont;
				style.fontSize = passwordSize;
				GUI.SetNextControlName ("PasswordField");
				passwordinput = GUI.TextField(makeRect(passwordField), passwordinput,maxPasswordLength, style);
				checkKeyPress(passwordinput);
				passwordinput = Regex.Replace(passwordinput, @"[^a-zA-Z0-9 ]", "");  //remove unallowed Chars
				////Debug.Log("REG :"+passwordinput+"#");

				GUI.FocusControl("PasswordField");

			}
			else if(loggedin){ 			//Inbox-gui
				
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
								audioS_voice.clip = mail_sound;
								audioS_voice.Play();
								quest.finishedSubQuest("1b");
								quest.addSubQuest("1d");
							GameObject.Find("MasterMind").GetComponent<ItemUseStates>().foundCode = true;
								//Activate sound
								currentTime = Time.time;
							if(GameObject.Find("MasterMind").GetComponent<ItemUseStates>().poweroutOcurred == false){
									initiatePOut = true;
								}
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
		if(!m_mastermind.GetComponent<ItemUseStates>().poweroutOcurred){
			if(currentTime + powerOutDelay < Time.time){
				GameObject.Find("MasterMind").GetComponent<ItemUseStates>().poweroutOcurred =true;
				GameObject mastermind = GameObject.FindGameObjectWithTag("Mastermind");
				mastermind.SendMessage("powerOut");
				haveFlashlight = mastermind.GetComponent<Inventory>().checkItemSupply("item_flashlight",1);
				playSounds();
				initiatePOut = false;
			}
		}
	}

	void playSounds(){
		StartCoroutine("playSoundInOrder");
	}

	IEnumerator playSoundInOrder(){
		for(int i = 0; i < clip_array.Length; i++){
			if(i != clip_array.Length-1){
				audioS_voice.clip = clip_array[i];
				audioS_voice.Play();
			}else{
				yield return new WaitForSeconds(1f);
				if(!haveFlashlight){
					audioS_voice.clip = clip_array[i];
					audioS_voice.Play();
					quest.addSubQuest("1f");
				}
			}

			yield return new WaitForSeconds(clip_array[i].length);
		}
		StopCoroutine("playSoundInOrder");
	}

	public void checkKeyPress(string pass){
		if(tempPass != pass && pass.Length > 0){
			int i = pass.Length-1;
			char c = pass[i];
			if(c == ' '){
				audioS_sound.clip = key_space_sound;
				audioS_sound.Play();
			}else{
				audioS_sound.clip = key_any_sound;
				audioS_sound.Play();
			}
			enterKey = true;
		}else if(pass.Length == 0 && enterKey){
			audioS_sound.clip = key_enter_sound;
			audioS_sound.Play();
			enterKey = false;
		}
		tempPass = pass;
	}

}
