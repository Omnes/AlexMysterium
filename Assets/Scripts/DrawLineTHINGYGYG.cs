using UnityEngine;
using System.Collections;

public class DrawLineTHINGYGYG : MonoBehaviour {
	
	public int amountOfColumns;
	public int amountOfRows;
	public float columnHeight;
	public float rowWidth;
	
	void OnDrawGizmosSelected(){
		
		for(int i = 0; i < amountOfColumns; i++){
			Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, transform.position + new Vector3(columnHeight * i,0,0));
			
			Vector3 lastPos=transform.position + new Vector3(columnHeight * i,0,0);
			
				for(int j = 0; j < amountOfRows; j++){
					Gizmos.color = Color.white;
					Gizmos.DrawLine(lastPos, lastPos + new Vector3(0,rowWidth * j,0));	
				}
		}
		
		for(int i = 0; i < amountOfRows; i++){
			Gizmos.color = Color.white;
			Gizmos.DrawLine(transform.position, transform.position + new Vector3(0,rowWidth * i,0));
			
			Vector3 lastPos = transform.position + new Vector3(0,rowWidth * i,0);
			
			for(int j = 0; j < amountOfColumns; j++){
				Gizmos.color = Color.white;
				Gizmos.DrawLine(lastPos, lastPos + new Vector3(columnHeight * j,0,0));
				
				Vector3 lastPos2 = lastPos + new Vector3(columnHeight * j,0,0);
				
			}
		}
	}
}
