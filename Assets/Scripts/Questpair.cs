using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Questpair {
	
	public string mID;
	public string mContent;
	public bool mFinished = false;
	public int mID2;
	public List<Questpair> mQuestlog = new List<Questpair>();
	
	public Questpair(string id, string content){
		
		mID = id;
		mContent = content;
	}
	
	// Use this for initialization
	void editContent(string newcontent){
		
		mContent = newcontent;
	}
	
	public void addSubQuest(Questpair subquest){
	
		mQuestlog.Add(subquest);
	}
	
	// Update is called once per frame
	public string getID(){
		
		return mID;
	}
	
	public string getContent(){
	
		return mContent;
	}

	public int getID2(){

		return mID2;
	}

	public void setID2(int id){

		mID2 = id;
	}
}
