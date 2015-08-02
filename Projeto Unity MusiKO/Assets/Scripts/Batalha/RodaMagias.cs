using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RodaMagias : MonoBehaviour {


	public GameObject Mestre;
	public GameMasterCombate AjudanteMestre;

	
	public void Start(){
		AjudanteMestre = Mestre.GetComponent<GameMasterCombate>();
	}
	

	public void Magia1(){
		GameMasterCombate.NumeroMagia = 0;
		
		AjudanteMestre.ClicarMagiaEspecifica();
	}
	public void Magia2(){
		GameMasterCombate.NumeroMagia = 1;
		
		AjudanteMestre.ClicarMagiaEspecifica();
	}
	public void Magia3(){
		GameMasterCombate.NumeroMagia = 2;
		
		AjudanteMestre.ClicarMagiaEspecifica();
	}
	public void Magia4(){
		GameMasterCombate.NumeroMagia = 3;
		
		AjudanteMestre.ClicarMagiaEspecifica();
	}
	
}