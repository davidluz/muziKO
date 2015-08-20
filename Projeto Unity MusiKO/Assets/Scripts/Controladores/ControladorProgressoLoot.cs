using UnityEngine;
using System.Collections;

public class ControladorProgressoLoot : MonoBehaviour {

	public static bool prontoParaIr = false;
	public static bool ComeçarXP = false;

	public GameObject TelaLoot;
	public GameObject Sombras;
	public GameObject TextoLoot;

	void Start(){
		prontoParaIr = false;
		ComeçarXP = false;
	}
	public void ClicarLoot(){
		TelaLoot.SetActive(false);
		Sombras.SetActive(false);
		TextoLoot.SetActive(false);
		ComeçarXP = true;
	}
	public void ClicarAvancar(){
		if(prontoParaIr==false){
			prontoParaIr = true;
		}else{
			//CodigoIrProximaTela
		}
	}
}
