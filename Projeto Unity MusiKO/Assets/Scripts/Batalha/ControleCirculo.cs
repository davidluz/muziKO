using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControleCirculo : MonoBehaviour {
	
	public Button Ataque;
	public Button Correr;
	public Button Magia;

	public GameObject Mestre;
	public GameMasterCombate AjudanteMestre;


	public void Start(){
		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();
	}

	public void SelecionarAtaque(){

		GameMasterCombate.Opcao = 1;

	}

	public void SelecionarCorrer(){
		GameMasterCombate.Opcao = 2;
		AjudanteMestre.ClicarMover();
	}

	public void SelecionarMagia(){
		GameMasterCombate.Opcao = 3;
	}

}
