using UnityEngine;
using System.Collections;

public class ContainerEquipamentos : MonoBehaviour {

	public int Tamanho = 0;
	
	public GameObject[] ListaEquipamentos;

	public Equipamentos[] AjudanteListaEquipamentos;
	
	void Awake(){
		AjudanteListaEquipamentos = new Equipamentos[ListaEquipamentos.Length];
		for(int x = 0;x<ListaEquipamentos.Length;x++){
			AjudanteListaEquipamentos[x] = ListaEquipamentos[x].GetComponent<Equipamentos>();
		}
	}

}
