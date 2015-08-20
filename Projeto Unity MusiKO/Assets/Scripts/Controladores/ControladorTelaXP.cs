using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ControladorTelaXP : MonoBehaviour {

	public int ID;
	public Image FotoPersonagem;
	public Text NomePersonagem;
	public Text NivelPersonagem;
	public Slider BarraXP;
	public int XPRestante;
	public int XPAtual;
	public int Nivel;

	public GameObject ModeloXP;
	public Experiencia AjudanteModeloXP;

	void Start(){
		XPRestante = DadosBatalha.ControleDadosBatalha.TotalXPBatalha;
		AjudanteModeloXP = ModeloXP.GetComponent<Experiencia>();
		Nivel = DadosPersonagens.ControleDadosPersonagem.Nivel[ID];
		XPAtual = DadosPersonagens.ControleDadosPersonagem.Experiencia[ID];
	}

	void Update(){

		if(ControladorProgressoLoot.ComeçarXP==true){

			if(XPRestante>0){
				if(ControladorProgressoLoot.prontoParaIr==true){
					bool ficar = true;
					while(ficar){
						if((XPAtual+XPRestante)>=AjudanteModeloXP.NivelEValorProximo[Nivel]){
							XPRestante = (XPAtual+XPRestante)-AjudanteModeloXP.NivelEValorProximo[Nivel];
							XPAtual = 0;
							Nivel = Nivel+1;
							NivelPersonagem.text = Nivel.ToString();
							DadosPersonagens.ControleDadosPersonagem.Nivel[ID] = Nivel;
							DadosPersonagens.ControleDadosPersonagem.Experiencia[ID] = XPAtual;

							float porcentagem  = 100*XPAtual/AjudanteModeloXP.NivelEValorProximo[Nivel];
							
							BarraXP.value = porcentagem;
						}else{
							XPAtual = XPAtual+XPRestante;
							XPRestante = 0;
							DadosPersonagens.ControleDadosPersonagem.Experiencia[ID] = XPAtual;

							float porcentagem  = 100*XPAtual/AjudanteModeloXP.NivelEValorProximo[Nivel];
							
							BarraXP.value = porcentagem;
							ficar = false;
						}

					}
				}else{
					XPRestante = XPRestante - 1;
					XPAtual = XPAtual+1;
					
					float porcentagem  = 100*XPAtual/AjudanteModeloXP.NivelEValorProximo[Nivel];
					
					BarraXP.value = porcentagem;
					
					if(XPAtual>=AjudanteModeloXP.NivelEValorProximo[Nivel]){
						XPAtual = 0;
						BarraXP.value = 0;
						Nivel = Nivel+1;
						NivelPersonagem.text = Nivel.ToString();
						DadosPersonagens.ControleDadosPersonagem.Nivel[ID] = Nivel;

						float porcentagem1  = 100*XPAtual/AjudanteModeloXP.NivelEValorProximo[Nivel];
						
						BarraXP.value = porcentagem1;
					}
				}


			}else{
				DadosPersonagens.ControleDadosPersonagem.Experiencia[ID] = XPAtual;
			}

		}


	}
}
