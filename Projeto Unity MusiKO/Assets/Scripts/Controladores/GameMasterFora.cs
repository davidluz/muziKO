using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterFora : MonoBehaviour {
	
	public GameObject Painel;
	public static bool Livre = true;
	public GameObject Personagem;
	public NPCNormal[] AjudanteNpcs;
	public GameObject [] Npcs;
	public int numeroNpcs = 0;
	public float Timer = 0;
	
	void Start(){
		for (int x = 0; x<numeroNpcs; x++) {
			AjudanteNpcs[x] = Npcs[x].GetComponent<NPCNormal>();
		}
	}

	void Update(){

		Timer += Time.deltaTime;
		if (Input.GetKey ("space")) {
			if(Timer>0.5f){

				if(Livre==true){
					
					for(int x = 0;x<numeroNpcs;x++){
						if(Vector3.Distance(Personagem.transform.position, Npcs[x].transform.position)<2){
							Painel.SetActive(true);
							StartCoroutine(AjudanteNpcs[x].Falar());
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
			AjudanteNpcs[0].Evento(0);
		}

	}
}
