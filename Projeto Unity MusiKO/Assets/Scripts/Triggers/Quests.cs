using UnityEngine;
using System.Collections;

public class Quests : MonoBehaviour {

	private GameMasterFora gameMaster;
	private ControladorQuests.questChain[] presentChains;

	public bool loadNewMap;
	public string newMapName;

	// Use this for initialization
	void Start () {
		gameMaster = FindObjectOfType<GameMasterFora> ();
		presentChains = gameMaster.presentChains;
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "Player") {
			for (int i=0;i<presentChains.Length;i++){
				if(!presentChains[i].chainActive)
					continue;

				for(int j=0;j<presentChains[i].chainSteps.Length;j++){
					if(!presentChains[i].chainSteps[j].stepActive)
						continue;

					/*if (--pseudo codigo--player.numeroDeItensQuest--final pseudo codigo-- >= presentChains[i].chainSteps[j].stepQtdeDeliver){
						advanceQuest(i,j,loadNewMap);
					}*/


					if (transform.position == presentChains[i].chainSteps[j].stepTarget.position){
						advanceQuest(i,j,loadNewMap);
					}

				}
			}
		}
	}

	void advanceQuest(int chain, int step, bool shouldLoad){
		presentChains[chain].chainSteps[step].stepComplete = true;
		presentChains[chain].chainSteps[step].stepActive = false;
		if (step < presentChains[chain].chainSteps.Length -1){
			presentChains[chain].chainSteps[step+1].stepActive = true;
			presentChains[chain].chainStep++;
			print ("Quest Step Complete");
		}
		else{
			presentChains[chain].chainComplete = true;
			presentChains[chain].chainActive = false;
			print ("Quest Chain Complete");
		}

		if (shouldLoad) {
			GameObject.FindGameObjectWithTag("Player").GetComponent<ForaCombate>().saveStatus();
			Application.LoadLevel (newMapName); 
		}
	}}
