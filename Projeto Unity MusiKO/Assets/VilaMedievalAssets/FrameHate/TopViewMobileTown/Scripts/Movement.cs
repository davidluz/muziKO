using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	public float speed = 1f;
	public static Vector3 moviment;
	Vector3 look;
	public GameObject reference;
	Transform refTransform;

	
	void Start () {
		refTransform = reference.transform;
	}

	void FixedUpdate () {
	}

	void Update () {

		moviment = new Vector3(Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"))*speed;
		moviment = refTransform.TransformDirection (moviment);
		moviment.y = GetComponent<Rigidbody>().velocity.y;
		GetComponent<Rigidbody>().velocity = moviment;
		//look = new Vector3(transform.position.x,transform.position.y,transform.position.z) + new Vector3(moviment.x,0,moviment.z);
		//transform.LookAt (look);
	}
}
