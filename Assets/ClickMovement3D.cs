using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class ClickMovement3D : MonoBehaviour {


    Vector3 target;
    bool moving;

    public Vector3 speed;
	public List<Vector3> path;


	// Use this for initialization
	void Start () {
        moving = false;
		path = new List<Vector3>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray,out hit) && hit.transform.tag == "floor") {
                path = GetComponent<Pathfinding>().findpath(transform.position , hit.point);
                path.Reverse();
                moving = true;
            }
        }

        if (moving && path.Count > 0){
			drawPath();
            moveTowards(path[0]);
            //transform.localScale = startScale * (3-transform.position.y);
            if (Vector2.Distance(transform.position,path[0])< 0.05){
                //moving = false;
				path.RemoveAt(0);
            }
        }

	}

    void moveTowards(Vector3 target){
        Vector3 direction = target - transform.position;
        direction = direction.normalized;
        rigidbody.position += new Vector3(direction.x * speed.x, direction.y * speed.y, direction.z * speed.z);
    
    }


    void OnCollisionEnter(Collision other) {
        target = transform.position;
        moving = false;
    }
	
	void drawPath(){
		for(int i = 0;i < path.Count-1;i++){
			Debug.DrawLine(path[i]+Vector3.up,path[i+1]+Vector3.up,Color.black);
		}
		
	}

}
