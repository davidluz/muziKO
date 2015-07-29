using UnityEngine;
using System.Collections;

public class ForaCombate : MonoBehaviour {

	public NavMeshAgent AgenteNavMesh;
	public int Valor = 1;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (GameMasterFora.Livre == true) {
			if (gameObject.GetComponent<CharacterController>() != null) {
				gameObject.GetComponent<CharacterController>().enabled = true;
			}
		} else {
			if (gameObject.GetComponent<CharacterController>() != null) {
				gameObject.GetComponent<CharacterController>().enabled = false;
				gameObject.GetComponent<Animation>().Play("idle");
			}
		}
	}
}
