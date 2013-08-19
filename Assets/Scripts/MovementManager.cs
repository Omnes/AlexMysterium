using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

//Robin

public class MovementManager : MonoBehaviour {


    public float speed;
	public List<Vector3> path;
	private Pathfinding pathfinder;


	// Use this for initialization
	void Start () {
		path = new List<Vector3>();
		pathfinder = GetComponent<Pathfinding>();
	}
	
	// Update is called once per frame
	void Update () {
        /*
		if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit) && hit.transform.tag == "Interactive") {
                StartCoroutine(pathfindToObjectAndActivate(hit.transform));
            }
        }
		*/
        if (path.Count > 0){
			drawPath();
            moveTowards(path[0]);
            //transform.localScale = startScale * (3-transform.position.y);
            if (Vector3.Distance(transform.position,path[0])< speed*Time.deltaTime+0.1){
                //moving = false;
				path.RemoveAt(0);
            }
        }

	}
	
	bool compareVec3(Vector3 v1,Vector3 v2){
		return Vector3.Distance(v1,v2) < 0.6;
		
	}
	
	public void pathfindToPosition(Vector3 target){
		
		if(!compareVec3(target,getEndDestination())){
			path = pathfinder.findpath(transform.position,target);
		}
		
	}
	
	
	
	public Vector3 pathfindToObject(Transform target){
		Vector3 targetPosition = new Vector3();
		bool foundWaypoint = false;
		foreach(Transform child in target){
			if(child.CompareTag("Waypoint")){
				targetPosition = child.position;
				foundWaypoint = true;
			}
		}
		if(!foundWaypoint){
			Debug.LogError("Gameobject " + target.name + " is missing a child with a ´Waypoint´ tag"); //
		}
		pathfindToPosition(targetPosition);
		return targetPosition;
	
	}
	/*
	public IEnumerator pathfindToObjectAndActivate(Transform target){
		
		Vector3 targetPosition = pathfindToObject(target);
		while(true){
			if(pathfinder.worldposToGridpos(targetPosition) == pathfinder.worldposToGridpos(transform.position)){
				target.SendMessage("Interact"); // se till att matcha med de enskilda scripten bara 
				Debug.Log("Interact message sent! to " + target);
				break;
				
			}

			if(!pathfinder.worldposToGridpos(targetPosition).Equals(pathfinder.worldposToGridpos(path.Last()))){
				//StopCoroutine("pathfindToObjectAndActivate");
				Debug.Log("Activate aborted!");
				break;
			}
			
			yield return new WaitForSeconds(.1f);
		}
		
	}
	*/
	
	public bool isAtPosition(Vector3 target){
		bool isAt = pathfinder.worldposToGridpos(target) == pathfinder.worldposToGridpos(transform.position);
		return isAt;
		
	}
	
	public Vector3 getEndDestination(){
		if(path.Count == 0){
			return transform.position;
		}
		
		return path.Last();
	}
	

    public void moveTowards(Vector3 target){
        Vector3 direction = target - transform.position;
        direction = direction.normalized;
        //rigidbody.position += new Vector3(direction.x * speed.x, direction.y * speed.y, direction.z * speed.z) * Time.deltaTime;
		rigidbody.position += direction * speed * Time.deltaTime;
    
    }
	
	private void drawPath(){
		for(int i = 0;i < path.Count-1;i++){
			Debug.DrawLine(path[i]+Vector3.up,path[i+1]+Vector3.up,Color.black);
		}
		
	}

}
