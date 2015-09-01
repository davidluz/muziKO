using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorTurno : MonoBehaviour {

	public bool SouEu;
	public bool EstouVivo;
	public string Nome;
	public float Agilidade;
	public float HPMax;
	public float HPAtual;
	public float MPMax;
	public float MPAtual;
	public float Stamina;
	public float Forca;
	public float Defesa;
	public Sprite MinhaFoto;
	public int QuantidadeMovimento = 2;
	public int QuantidadeAtaque = 1;
	public float Dano;
	public int posicaoX = 0;
	public int posicaoY = 0;
	public int[] ListaMagias;
	public Slider ExemploSlider;
	public Slider ControladorVida;
	public GameObject AjudanteCanvas;

	public Text ExemploTextoCombate;



	void Start(){
		EstouVivo = true;
		AjudanteCanvas = GameObject.Find("MeuPreciosoCanvas");

		ControladorVida = Instantiate(ExemploSlider) as Slider;
		ControladorVida.transform.SetParent(AjudanteCanvas.transform);
		ControladorVida.transform.localScale = new Vector3(0.015f,0.015f,1);
		ControladorVida.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,gameObject.transform.position.z);
		ControladorVida.name = Nome.ToString();

	}
	

	void Update(){

		ControladorVida.transform.position = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+1,gameObject.transform.position.z);
	}

	public bool TirarVida(float quantidade){

		if((HPAtual-quantidade)<=0){

			TextoDeCombate("Morto");

			EstouVivo = false;

			ControladorVida.value = 0;

			return false;
			//Morrer
		}else{
			HPAtual = HPAtual-quantidade;

			float porcentagem  = 100*HPAtual/HPMax;

			TextoDeCombate(quantidade.ToString());

			ControladorVida.value = porcentagem;

			return true;



			//// Colocar Codigo Dano ////////
		}

	}

	public void TextoDeCombate(string quantidade){
		Text temp = Instantiate(ExemploTextoCombate) as Text;
		temp.transform.SetParent(AjudanteCanvas.transform);
		temp.transform.localScale = new Vector3(0.08f,0.08f,1);
		temp.rectTransform.localPosition = new Vector3(gameObject.transform.position.x,gameObject.transform.position.y+0.9f,gameObject.transform.position.z);
		//temp.transform.position = 
		temp.name = Nome.ToString();

		temp.text = quantidade;
		temp.GetComponent<Animator>().SetTrigger("Hit");
		Destroy(temp.gameObject,2);

	}
}
