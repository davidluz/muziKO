using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorInventario : MonoBehaviour {

	public Camera CameraPrincipal;
	public Camera CameraInventario;




	// Use this for initialization
	void Start () {
		CameraPrincipal.enabled = true;
		CameraInventario.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKey("I")){
			IrInventario();
		}

	}

	public void VoltarJogo(){
		CameraPrincipal.enabled = true;
		CameraInventario.enabled = false;
	}

	public void IrInventario(){
		CameraPrincipal.enabled = false;
		CameraInventario.enabled = true;
	}
}
