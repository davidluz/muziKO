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
			float Vertical = Input.GetAxis("Vertical");
			float Horizontal = Input.GetAxis("Horizontal");
			Vector3 auxiliar = new Vector3 ((Horizontal * 1)+gameObject.transform.position.x, 0, (Vertical * 1)+gameObject.transform.position.z);
			AgenteNavMesh.destination = auxiliar;
		}

	}
}
