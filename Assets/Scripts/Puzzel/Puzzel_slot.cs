using UnityEngine;
using System.Collections;

public class Puzzel_slot : MonoBehaviour {
	
	public Vector3 		position;
	public Vector3		centerPos;
	public float 		radius;
	public int 			correct_slot_value;
	public AudioClip insertFuse;
	public AudioClip removeFuse;
	
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
		centerPos = transform.position;
		Debug.Log("Z: " + centerPos.z );
		//centerPos.z += (int)gameObject.transform.localScale.z;
		Transform front = GameObject.Find ("front").transform;
		//centerPos.z -= (int)((gameObject.collider.bounds.size.z/2)-(gameObject.collider.bounds.size.z/4));
		centerPos.z = front.position.z + (renderer.bounds.size.z/2+0.1f);
		Debug.Log("z: " + gameObject.collider.bounds.size.z/2 );
		
		speaker = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if(ptrPuzzel_Piece != null && !ptrPuzzel_Piece.holding && Vector3.Distance(ptrPuzzel_Piece.transform.position,centerPos) > 0.5){
			//Debug.Log("Current pos: " + ptrPuzzel_Piece.transform.position + " new pos: " + centerPos);
			ptrPuzzel_Piece.transform.position = areaSnapping();
			//Debug.Log(" AFTERSNAPCurrent pos: " + ptrPuzzel_Piece.transform.position + " new pos: " + centerPos);
			speaker.clip = insertFuse;
			speaker.Play();
			Debug.Log("PLAYING A SOUND");// YESH
		}
		if(ptrPuzzel_Piece != null && ptrPuzzel_Piece.Piece_KeyValue == correct_slot_value && ptrPuzzel_Piece.correct && !ptrPuzzel_Piece.holding && !Correct){
			Correct = true;
			Debug.Log("Correct!");
			//}
		}
	}
	
	Vector3 areaSnapping(){// puzzel piece | puzzel target position and radius
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
		if(!Input.GetMouseButtonDown(0)){
			if(other.tag == "PusselBit" && ptrPuzzel_Piece == null){
					ptrPuzzel_Piece = other.GetComponent<Puzzel_piece>();
					ptrPuzzel_Piece.inUse = true;
					if( slot_empty && !ptrPuzzel_Piece.holding){
						slot_empty = false;

							ptrPuzzel_Piece.transform.position = areaSnapping();
							Debug.Log("pusselbit");
							speaker.clip = insertFuse;
							speaker.Play();
					}
				//}
			}else{ 
				if(other.tag == "PusselBit" && !slot_empty && other != ptrPuzzel_Piece){
					Puzzel_piece temp = other.GetComponent<Puzzel_piece>();
					temp.inUse = false;
					//speaker.Play();
					temp.Reposition();
					Debug.LogWarning("Pussel-spot occupied");
				}
			}
		}
	}
	
	void OnTriggerExit(Collider other){// om du tar bort en bit från dess rätta plats så får platsen ett "unfinished state"
		if(other.tag == "PusselBit"){
		
			Puzzel_piece temp = other.GetComponent<Puzzel_piece>();
			if(temp == ptrPuzzel_Piece){
				slot_empty = true;
				if(ptrPuzzel_Piece.Piece_KeyValue == correct_slot_value){
					Correct = false;
				}
				ptrPuzzel_Piece.inUse = false;
				ptrPuzzel_Piece = null;
					//enter_exit = false;
				speaker.clip = removeFuse;
				speaker.Play();
			}
		}
	}
	
	bool getCorrect(){
		return Correct;
	}
}
