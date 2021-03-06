﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCNormal : MonoBehaviour {
	public bool SubRotinas = true;
	public NavMeshAgent AuxiliarNavMesh;
	public int Comportamento = 0;
	public Text NomePersonagem;
	public Text AjudaTexto;
	public Image Moldura;
	public string [] Falas;
	public string Nome;

	public float Timer = 0.0f;

	public bool questGiver;
	public string chainToStart = "";


	private GameMasterFora gameMaster;
	private int ContadorFalas = 0;
	private Vector3 PosicaoInicial;

	void Start(){
		PosicaoInicial = gameObject.transform.position;
		gameMaster = FindObjectOfType<GameMasterFora> ();

		gameObject.transform.FindChild("Quest").gameObject.SetActive(questGiver);
	}
	void Update(){

		if (gameMaster.Livre == true) {
			if(SubRotinas==true){
				Timer += Time.deltaTime;
				
				if (Timer > 1.0f) {
					switch(Comportamento){
					case 0:
						if(Vector3.Distance(PosicaoInicial,gameObject.transform.position)>2){
							AuxiliarNavMesh.destination = PosicaoInicial;
							Timer = 0;
						}
						break;
					case 1:
						AuxiliarNavMesh.destination = new Vector3((Random.Range(-2,2)+gameObject.transform.position.x),gameObject.transform.position.y,(Random.Range(-2,2)+gameObject.transform.position.z));
						Timer = 0;
						break;
					}//fim switch
				}//fim if
			}//subrotinas

		}//livre

	}

	public void Evento(int numeroEvento){
		switch(numeroEvento){
		case 0:
				print("Teste Evento");				
				break;
		}//fim switch
	}//fim evento

	public IEnumerator Falar(){
		gameMaster.bWriting = true;
		gameMaster.Livre = false;
		NomePersonagem.text = Nome;
		int NumeroDeFalas = Falas.Length;
		if (ContadorFalas < NumeroDeFalas) {
			int Comprimento = Falas [ContadorFalas].Length;
			AjudaTexto.text = "";
			for (int x = 0; x<Comprimento; x++) {
				AjudaTexto.text = AjudaTexto.text + Falas [ContadorFalas] [x];
				yield return new WaitForSeconds (0.01f);
			}

			print (Falas [ContadorFalas]);
			ContadorFalas++;
			gameMaster.bWriting = false;
			
		} else {
			gameMaster.Livre = true;
			gameMaster.Painel.SetActive(false);
			ContadorFalas = 0;
			gameMaster.resetEngagedNPC();

			if (questGiver){
				questGiver = !questGiver;
				gameObject.transform.FindChild("Quest").gameObject.SetActive(questGiver);
				Falas = new string[1];
				Falas[0] = "Esta missao ja foi aceita";

				for(int i=0;i<gameMaster.presentChains.Length;i++){
					if(gameMaster.presentChains[i].chainName != chainToStart)
						continue;

					gameMaster.presentChains[i].chainActive = true;
					gameMaster.presentChains[i].chainSteps[0].stepActive = true;
				}
			}

		}
	}//falar
}
