using UnityEngine;
using System.Collections;

public class DadosBatalha : MonoBehaviour {


	public static DadosBatalha ControleDadosBatalha;

	public int TotalXPBatalha;
	public int[] PersonagensVivos;
	public int TipoLoot;


	void Awake () {
		
		if(ControleDadosBatalha == null){
			
			DontDestroyOnLoad(gameObject);
			ControleDadosBatalha = this;
			
		}else if(ControleDadosBatalha != this){
			
			Destroy(gameObject);
		}
		
		
	}//awake


}
