using UnityEngine;
using System.Collections;

public class CameraScript : MonoBehaviour {
	Transform cameraTransform;
	private GameObject player;

	private Vector3 cameraPos = new Vector3 (0, 0, 0);
	private Vector3 tempOffset = new Vector3 (0, 0, 0);

	public Vector3 cameraOffset = new Vector3(0,3,-5);
	public float xSpeed = 2;
	public float ySpeed = 2;

	void Awake (){
		player = GameObject.FindGameObjectWithTag ("Player");
		
		if (player)
			cameraPos = player.transform.position + cameraOffset;
		else
			Debug.Log("Please assign a target to the camera.");

		transform.LookAt (player.transform);
		transform.position = cameraPos;
	}

	void LateUpdate () {


		if (Input.GetMouseButton (1)) {
			float vertical = Input.GetAxis ("Mouse X");
			float horizontal = Input.GetAxis ("Mouse Y");
			if (vertical != 0)
				transform.RotateAround (player.transform.position, Vector3.down, xSpeed * vertical * Time.deltaTime);
			if (horizontal != 0)
				transform.RotateAround (player.transform.position, Vector3.left, ySpeed * horizontal * Time.deltaTime);

			transform.LookAt (player.transform);
			tempOffset = transform.position - cameraPos;
		}

		cameraPos = player.transform.position + cameraOffset;
		transform.position = cameraPos + tempOffset;
	}
}
