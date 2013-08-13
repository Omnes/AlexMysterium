using UnityEngine;
using System.Collections;

public class GoToCenterThenDown : MonoBehaviour {

public double centerTime = 1.0f;
public float scalingModifier = 2.0f;
public bool trigger = false;
public float speed = 8.0f;
public float limit = 0.1f;
private Vector3 center;// = new Vector3(Screen.width/2, Screen.height/2, 0);
private Vector3 currentPosition;
private bool reachedCenter = false;
// Use this for initialization
void Start () {
center = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y, 0);
}

// Update is called once per frame
void Update () {
if(trigger)
{
currentPosition = transform.position;
Vector3 directionVec = center - currentPosition;// find the direction vector, then normalize it for your movment using vector length
float DirectionVecLength = Mathf.Sqrt((directionVec.x*directionVec.x + directionVec.y*directionVec.y + directionVec.z*directionVec.z));// vector length
if(DirectionVecLength > limit && !reachedCenter)// go to center
{
if(DirectionVecLength <= limit+1.0)// close enough to the center
{
reachedCenter = true;
}
currentPosition += directionVec.normalized*speed*Time.deltaTime;
gameObject.transform.position = currentPosition;
}
else if(centerTime > 0) // then go down after X seconds in the center of the screen
{
centerTime -= Time.deltaTime;
}
else
{
directionVec = new Vector3(0,-1*speed*Time.deltaTime,0);
gameObject.transform.position += directionVec;
}
transform.localScale = new Vector3((1/DirectionVecLength*scalingModifier)+1, (1/DirectionVecLength*scalingModifier)+1, 0);
}
}
}