using UnityEngine;
using System.Collections;

public class Puzzel_slot : MonoBehaviour {
	
	public Vector3 		position;
	public Vector3		centerPos;
	public float 		radius;
	public int 			correct_slot_value;
	
	//public AudioClip	sound;
	public AudioSource 	speaker;// finish to make it play sound when a piece is set.OBS!!!!!!!!!!!!!!!!!!!!!
	//public bool 	correct_piece;
	
	public bool Correct;
	//private bool enter_exit = true;
	
	//seans skit
	private Puzzel_piece ptrPuzzel_Piece;
	private bool slot_empty = true;
	// Use this for initialization
	void Start () {
		position = transform.position;
		//centerPos = renderer.bounds.center;
		centerPos = renderer.bounds.center;
		Debug.Log("Z: " + centerPos.z );
		//centerPos.z += (int)gameObject.transform.localScale.z;
		centerPos.z -= (int)((gameObject.collider.bounds.size.z/2)-(gameObject.collider.bounds.size.z/4));
		Debug.Log("z: " + gameObject.collider.bounds.size.z/2 );
		
		speaker = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ptrPuzzel_Piece != null && !ptrPuzzel_Piece.holding && Vector3.Distance(ptrPuzzel_Piece.transform.position,centerPos) > 0.2){
			//Debug.Log("Current pos: " + ptrPuzzel_Piece.transform.position + " new pos: " + centerPos);
			ptrPuzzel_Piece.transform.position = areaSnapping();
			//Debug.Log(" AFTERSNAPCurrent pos: " + ptrPuzzel_Piece.transform.position + " new pos: " + centerPos);
			speaker.Play();
			Debug.Log("PLAYING A SOUND");
		}
		if(ptrPuzzel_Piece != null && ptrPuzzel_Piece.Piece_KeyValue == correct_slot_value && ptrPuzzel_Piece.correct && !ptrPuzzel_Piece.holding && !Correct){
			//if(ptrPuzzel_Piece.movable){
			//ptrPuzzel_Piece.correct = false;
			Correct = true;
			Debug.Log("Correct!");
			//}
		}
	}
	
	Vector3 areaSnapping(){// puzzel piece | puzzel target position and radius
		/*
		Vector3 directionVec 		= position - piece_position;// find the direction vector, then normalize it for your movment using vector length
		float DirectionVecLength 	= Mathf.Sqrt((directionVec.x*directionVec.x + directionVec.y*directionVec.y + directionVec.z*directionVec.z));// vector length
		
		if(DirectionVecLength < radius){
			piece_position = position;
		}*/
		//return piece_position;
		return centerPos;
	}
	
	// check if it's the correct value
	bool checkCorrect(int piece_value, int slot_value){
		bool check = false;
		if(piece_value == slot_value){
			check = true;
		}
		return check;
	}

	void OnTriggerEnter(Collider other) {
			//enter_exit = true;
	}

	void OnTriggerStay(Collider other) {// bara kolla bitarna när man har släppt dem.
				//if(enter_exit){
			if(other.tag == "PusselBit"){
				ptrPuzzel_Piece = other.GetComponent<Puzzel_piece>();
				if( slot_empty && !ptrPuzzel_Piece.holding){
					//ptrPuzzel_Piece = other.GetComponent<Puzzel_piece>();
					slot_empty = false;
				
					if(!ptrPuzzel_Piece.holding){
						ptrPuzzel_Piece.transform.position = areaSnapping();
						Debug.Log("pusselbit");
						//speaker.Play();
						//ptrPuzzel_Piece.transform.position = areaSnapping();
						
					/*if(ptrPuzzel_Piece.Piece_KeyValue == correct_slot_value && ptrPuzzel_Piece.movable){
						//if(ptrPuzzel_Piece.movable){
							ptrPuzzel_Piece.movable = false;
							Correct = true;
							Debug.Log("Correct!");
						//}
					}*/
					}
				}
			//}
		}else{ 
			if(other.tag == "PusselBit" && !slot_empty && other != ptrPuzzel_Piece){
				Puzzel_piece temp = other.GetComponent<Puzzel_piece>();
				//speaker.Play();
				temp.Reposition();
				Debug.LogWarning("Pussel-spot occupied");
			}
		}
		/*if(ptrPuzzel_Piece != null && !ptrPuzzel_Piece.holding && Vector3.Distance(ptrPuzzel_Piece.transform.position,centerPos) < 0.2){
			//Debug.Log("Current pos: " + ptrPuzzel_Piece.transform.position + " new pos: " + centerPos);
			ptrPuzzel_Piece.transform.position = areaSnapping();
			//Debug.Log(" AFTERSNAPCurrent pos: " + ptrPuzzel_Piece.transform.position + " new pos: " + centerPos);
			speaker.Play();
			Debug.Log("PLAYING A SOUND");
		}
		if(ptrPuzzel_Piece != null && ptrPuzzel_Piece.Piece_KeyValue == correct_slot_value && ptrPuzzel_Piece.correct && !ptrPuzzel_Piece.holding){
			//if(ptrPuzzel_Piece.movable){
				//ptrPuzzel_Piece.correct = false;
				Correct = true;
				Debug.Log("Correct!");
			//}
			}
		}*/
			//enter_exit = false;
		//}
	}
	
	void OnTriggerExit(Collider other){// om du tar bort en bit från dess rätta plats så får platsen ett "unfinished state"
		
		if(other.tag == "PusselBit"){
			
			ptrPuzzel_Piece = other.GetComponent<Puzzel_piece>();
			slot_empty = true;
			if(ptrPuzzel_Piece.Piece_KeyValue == correct_slot_value){
				Correct = false;
			}
			ptrPuzzel_Piece = null;
				//enter_exit = false;
		}
	}
	
	/*void OnDrawGizmosSelected () {
		// Display the explosion radius when selected
		Gizmos.color = Color.white;
		Gizmos.DrawWireSphere (transform.position, radius);
	}*/
	
	bool getCorrect(){
		return Correct;
	}
}
