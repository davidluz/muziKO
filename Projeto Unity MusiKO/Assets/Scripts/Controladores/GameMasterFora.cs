using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterFora : MonoBehaviour {
	
	public ControladorQuests.questChain[] presentChains;
	public static bool Livre = true;

	private int offsetToNPC = 2;
	private GameObject Personagem;
	private NPCNormal EngagedNPC;
	private int numeroNpcs = 0;
	private GameObject [] Npcs;
	private NPCNormal[] AjudanteNpcs;
	private float Timer = 0;

	[HideInInspector]
	public GameObject Painel;
	
	void Start(){
		Painel = GameObject.Find ("ChatPanel");

		Personagem = GameObject.FindGameObjectWithTag ("Player");
		Npcs = GameObject.FindGameObjectsWithTag ("NPC");
		numeroNpcs = Npcs.Length;

		Painel.SetActive(false);
		EngagedNPC = null;
	}

	void Update(){

		Timer += Time.deltaTime;
		if (Input.GetKey ("t")) {
			if(Timer>0.5f){

				if(Livre==true){
					
					for(int x = 0;x<numeroNpcs;x++){
						if(Vector3.Distance(Personagem.transform.position, Npcs[x].transform.position)<offsetToNPC){
							Painel.SetActive(true);
							StartCoroutine(Npcs[x].GetComponent<NPCNormal>().Falar());
							EngagedNPC = Npcs[x].GetComponent<NPCNormal>();
						}
					}
					
				}else{
					Painel.SetActive(false);
					Livre = true;
				}

			}




			Timer  =0.0f;
		}
		if (Input.GetKey ("e")) {
			Npcs[0].GetComponent<NPCNormal>().Evento(0);
		}

		if (Input.GetMouseButtonUp (0) && !Livre && EngagedNPC != null) {
			StartCoroutine(EngagedNPC.Falar());
		}

	}

	public void resetEngagedNPC(){
		EngagedNPC = null;
	}

}
