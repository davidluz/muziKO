using UnityEngine;
using System.Collections;

public class InicioDeJogo : MonoBehaviour {

	public void Iniciar(){

		Application.LoadLevel("TesteMecanicaCombate");
	}
	public void Carregar(){

	}
	public void VoltarMenu(){
		Application.LoadLevel("MenuPrincipal");
	}
}
