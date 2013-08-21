using UnityEngine;
using System.Collections;

public class OrbitAndLookAt : MonoBehaviour {
	
	public Transform target;
	public float distance = 3;
	public float speed = 2;
	
	void Update () {
		float TimeSpeed = Time.time * speed;
		transform.position = target.position + new Vector3(Mathf.Sin(TimeSpeed)*distance, 0, Mathf.Cos(TimeSpeed)*distance);
		transform.LookAt(target);
	
	}
}
