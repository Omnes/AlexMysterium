using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SceneSound_Packet : MonoBehaviour {
	
	public AudioClip[] audioList;
	public int clipCount;
	// Use this for initialization
	void Awake(){
		clipCount = audioList.Length;
	}
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	public AudioClip[]	getSceneSound(){
		return audioList;	
	}
	
	public AudioClip getElement(int x){
		return	audioList[x];	
	}
	/*
	public AudioClip getElement(int x){
		int temp = 0;
		while(temp <= x){
			audioList.GetEnumerator().MoveNext();// enumeration == not thread-safe
			++temp;
		}
		//Debug.Log("Current Song Name: " + (AudioClip)audioList.GetEnumerator().Current);
		return (AudioClip)audioList.GetEnumerator().Current;
	}
	*/
}
