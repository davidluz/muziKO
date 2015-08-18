using UnityEngine;
using System.Collections;

public class ForaCombate : MonoBehaviour {
	public int Valor = 1;

	private GameMasterFora gameMaster;
	private GameObject player;

	public static Transform playerPos = null;
	public static bool shouldLoad = false;
	public static ControladorQuests.questChain[] staticChains;

	// Use this for initialization
	void Start () {
		gameMaster = FindObjectOfType<GameMasterFora> ();
		player = GameObject.FindGameObjectWithTag ("Player");
	}
	
	// Update is called once per frame
	void Update () {
		if (gameMaster.Livre == true) {
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

	public void saveStatus(){
		playerPos = player.transform;
		shouldLoad = true;
		staticChains = gameMaster.presentChains;
		print ("Status saved");
	}
}
