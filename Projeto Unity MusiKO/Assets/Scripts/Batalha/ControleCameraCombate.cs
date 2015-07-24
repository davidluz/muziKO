using UnityEngine;
using System.Collections;

public class ControleCameraCombate : MonoBehaviour {

	public float Velocidade;

	public GameObject AjudanteCamera;
	// Use this for initialization
	
	// Update is called once per frame
	void Update () {

		float vertical = Input.GetAxis("Vertical") * Velocidade;
		float horinzontal = Input.GetAxis("Horizontal") * Velocidade;
		vertical *= Time.deltaTime;
		horinzontal *= Time.deltaTime;

		//gameObject.transform.position = new Vector3(horinzontal,gameObject.transform.position.y,vertical);
	}
}
