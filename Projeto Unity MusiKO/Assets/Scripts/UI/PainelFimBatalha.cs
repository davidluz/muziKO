using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PainelFimBatalha : MonoBehaviour {
	
	
	public float posicaoOriginal;
	public float LimiteInferior;
	public float Velocidade = 0;
	public bool Subir = true;
	public bool Vitoria = true;

	public Text MeuTexto;
	
	public RectTransform meuTranform;
	// Update is called once per frame
	void Start (){
		meuTranform = GetComponent<RectTransform>();
	}
	void Update () {
		
		if(Subir==true){
			
			if(meuTranform.anchoredPosition.y>=posicaoOriginal){
				
			}else{
				meuTranform.anchoredPosition = new Vector2(meuTranform.anchoredPosition.x,meuTranform.anchoredPosition.y+Velocidade);
			}

			if(Input.GetKeyDown("space")){
				if(Vitoria==true){
					Application.LoadLevel("Loot");
				}else{
					Application.LoadLevel("GameOver");
				}

			}
			
		}else{
			if(meuTranform.anchoredPosition.y<=LimiteInferior){
				
			}else{
				meuTranform.anchoredPosition = new Vector2(meuTranform.anchoredPosition.x,meuTranform.anchoredPosition.y-Velocidade);
			}
		}
		
	}

	public void MudarTexto(string Teste){
		MeuTexto.text = Teste;
		FazerSubir();
	}

	public void FazerSubir(){
		Subir = true;
	}
	public void FazerDescer(){
		Subir = false;
	}
	
	
}
