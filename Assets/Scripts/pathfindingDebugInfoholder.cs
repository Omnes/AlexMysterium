using UnityEngine;
using System.Collections;

public class pathfindingDebugInfoholder : MonoBehaviour {
	
	public Vector2 place;
	public bool closed;
	private float t = Time.time;

	public void updColor(){
		if(closed){
			renderer.material.color = new Color(1,0,0);
		}else{
			renderer.material.color = new Color(0,1,0);
		}
	}
	
	void OnMouseOver(){
		if(t+0.2f < Time.time){
			if(Input.GetMouseButton(2)){
				closed = false;
				updColor();
				t = Time.time;
			}
			if(Input.GetMouseButton(1)){
				closed = true;
				updColor();
				t = Time.time;
			}
		}
	}
}
