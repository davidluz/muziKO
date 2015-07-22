using UnityEngine;
using System.Collections;

public class ScriptClicarTerreno : MonoBehaviour {

	public GameObject Mestre;
	public GameMasterCombate AjudanteMestre;
	
	
	public void Start(){
		Mestre = GameObject.Find("ControladorJogo");
		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();
	}

	void OnMouseUp(){

		if(GameMasterCombate.Rodando==false){
			AjudanteMestre.ClicarTerreno(gameObject.transform.position.x,gameObject.transform.position.z);
		}

	}
}
