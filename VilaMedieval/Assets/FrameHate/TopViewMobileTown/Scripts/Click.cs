using UnityEngine;
using System.Collections;

public class Click : MonoBehaviour {

	
	RaycastHit click;

	public GameObject Root;
	public float movspeed = 8;
	public static Vector3 movement;

	public GameObject reference;
	Transform refTransform;
	Vector3 tempmovement;

	public float rotatespeed = 4;
	public float zoomspeed = 4;
	public GameObject Exit;
	public GameObject Up;
	public GameObject Down;
	public GameObject Left;
	public GameObject Right;
	public GameObject ZoomIn;
	public GameObject ZoomOut;
	public GameObject CCW;
	public GameObject CW;
	public GameObject CameraPivot;
	Vector3 rotation = new Vector3 (0,15,0);
	Vector3 zoom = new Vector3 (0,1,0);

	void Start () {
		refTransform = reference.transform;
	}

	void Update () {

		movement = tempmovement*movspeed;
		movement = refTransform.TransformDirection (movement);
		movement.y = Root.rigidbody.velocity.y;
		Root.rigidbody.velocity = movement;
			
		if(Input.GetMouseButton(0)){
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			
			Debug.DrawRay(ray.origin, ray.direction * 10, Color.yellow);
			if (Physics.Raycast (ray,out click)) {
				if (click.collider.gameObject == CCW){
					CameraPivot.transform.Rotate(rotation*-1*Time.deltaTime*rotatespeed,Space.World);
				}
				if (click.collider.gameObject == CW){
					CameraPivot.transform.Rotate(rotation*Time.deltaTime*rotatespeed,Space.World);
				}
				if (click.collider.gameObject == ZoomIn){
					transform.Translate(zoom*-1*Time.deltaTime*zoomspeed,Space.World);
				}
				if (click.collider.gameObject == ZoomOut){
					transform.Translate(zoom*Time.deltaTime*zoomspeed,Space.World);
				}
				if (click.collider.gameObject == Up){
					tempmovement = new Vector3 (0,0,1);
				}
				if (click.collider.gameObject == Down){
					tempmovement = new Vector3 (0,0,-1);
				}
				if (click.collider.gameObject == Left){
					tempmovement = new Vector3 (-1,0,0);
				}
				if (click.collider.gameObject == Right){
					tempmovement = new Vector3 (1,0,0);
				}
				if (click.collider.gameObject == Exit){
					Debug.Log ("exit");
					Application.Quit();
				}
			}
		}else{
			tempmovement = new Vector3 (Input.GetAxis ("Horizontal"), 0, Input.GetAxis ("Vertical"));
		}
	}
}
