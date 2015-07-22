using UnityEngine;
using System.Collections;

public class ControleCameraCombate : MonoBehaviour {


	public GameObject AjudanteCamera;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = new Vector3(AjudanteCamera.transform.position.x,gameObject.transform.position.y,AjudanteCamera.transform.position.z-10);
	}
}
