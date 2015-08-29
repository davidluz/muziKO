using UnityEngine;
using System.Collections;


public class DadosCombate : MonoBehaviour {
	
	public static DadosCombate ControleDadosCombate;
	
	public int[] NumeroDeInimigos;
	public int[] QuaisInimigos;
	public int[] Terrenos;

	
	// Use this for initialization
	void Awake () {
		
		if(ControleDadosCombate == null){
			
			DontDestroyOnLoad(gameObject);
			ControleDadosCombate = this;
			
		}else if(ControleDadosCombate != this){
			
			Destroy(gameObject);
		}
		
		
	}//awake
	

}

