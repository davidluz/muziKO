using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PainelPersonagem : MonoBehaviour {


	public float posicaoOriginal;
	public float LimiteInferior;
	public float Velocidade = 0;
	public bool Subir = true;
	public Image Retrato;

	public Slider ControladorHP;
	public Slider ControladorMP;

	public Text Nome;
	public Text NumeroHP;
	public Text NumeroMP;

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

		}else{
			if(meuTranform.anchoredPosition.y<=LimiteInferior){
				
			}else{
				meuTranform.anchoredPosition = new Vector2(meuTranform.anchoredPosition.x,meuTranform.anchoredPosition.y-Velocidade);
			}
		}

	}

	public void AtualizarInformaçoes(string NomePersonagem,float HPMax,float HPAtual,float MPMax,float MPAtual,Sprite Personagem){

		float porcentagemHP  = 100*HPAtual/HPMax;
		float porcentagemMP  = 100*MPAtual/MPMax;

		ControladorHP.value = porcentagemHP;
		ControladorMP.value = porcentagemMP;


		Retrato.sprite = Personagem;

		Nome.text = NomePersonagem;

		NumeroHP.text = HPAtual.ToString()+"/"+HPMax.ToString();
		NumeroMP.text = MPAtual.ToString()+"/"+MPMax.ToString();

	}

	public void FazerSubir(){
		Subir = true;
	}
	public void FazerDescer(){
		Subir = false;
	}


}
