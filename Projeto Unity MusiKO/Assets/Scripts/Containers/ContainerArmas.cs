using UnityEngine;
using System.Collections;

public class ContainerArmas : MonoBehaviour {

	public int Tamanho = 0;
	
	public GameObject[] ListaArmas;

	public Armas[] AjudanteListaArmas;

	void Awake(){
		AjudanteListaArmas = new Armas[ListaArmas.Length];
		for(int x = 0;x<ListaArmas.Length;x++){
			AjudanteListaArmas[x] = ListaArmas[x].GetComponent<Armas>();
		}
	}
}
