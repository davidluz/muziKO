using UnityEngine;
using System.Collections;

public class Equipamentos : MonoBehaviour {

	public int ID;
	public int Slot; // 1 = Capacete, 2 = Armadura, 3 = Calça, 4 = Colar, 5 = Anel
	public int Raridade; // 1 = Nomal , 2 = Incomum, 3 = Raro, 4 = Epico, 5 = Lendario
	public string Nome;
	public int DefesaMin;
	public int DefesaMax;
	public string Descricao;
	public Sprite Icone;
	public int[] Adicionais;
}
