using UnityEngine;
using System.Collections;


public class DadosProgressoJogo : MonoBehaviour {
	
	public static DadosProgressoJogo ControleDadosProgresso;
	

	
	
	// Use this for initialization
	void Awake () {
		
		if(ControleDadosProgresso == null){
			
			DontDestroyOnLoad(gameObject);
			ControleDadosProgresso = this;
			
		}else if(ControleDadosProgresso != this){
			
			Destroy(gameObject);
		}
		
		
	}//awake
	
	
}