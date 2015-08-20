using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GerarLoot : MonoBehaviour {

	public GameObject ContainerArmamento;
	public GameObject ContainerArmaduras;

	public ContainerArmas AjudanteContainerArmamento;
	public ContainerEquipamentos AjudanteContainerArmaduras;

	public RectTransform MeuRect;
	public GameObject ExemploLoot;
	public Text Nenhum;

	public int ChanceLendario = 20;
	public int ChanceEpico = 80;
	public int ChanceRaro = 150;
	public int ChanceMagico = 250;
	public int ChanceNormal = 500;
	
	void Start () {
		AjudanteContainerArmamento = ContainerArmamento.GetComponent<ContainerArmas>();
		AjudanteContainerArmaduras = ContainerArmaduras.GetComponent<ContainerEquipamentos>();

		AjudanteContainerArmamento.AjudanteListaArmas = new Armas[AjudanteContainerArmamento.ListaArmas.Length];
		for(int x = 0;x<AjudanteContainerArmamento.ListaArmas.Length;x++){
			AjudanteContainerArmamento.AjudanteListaArmas[x] = AjudanteContainerArmamento.ListaArmas[x].GetComponent<Armas>();
		}

		AjudanteContainerArmaduras.AjudanteListaEquipamentos = new Equipamentos[AjudanteContainerArmaduras.ListaEquipamentos.Length];
		for(int x = 0;x<AjudanteContainerArmaduras.ListaEquipamentos.Length;x++){
			AjudanteContainerArmaduras.AjudanteListaEquipamentos[x] = AjudanteContainerArmaduras.ListaEquipamentos[x].GetComponent<Equipamentos>();
		}

		NovoLoot();
	}

	public void NovoLoot () {

		int x = DadosBatalha.ControleDadosBatalha.TipoLoot;
		int numeroArmas = 0;
		int numeroEquipamentos = 0;

		switch (x){

		case 1:
			numeroArmas = Random.Range(0,2);
			numeroEquipamentos = Random.Range(0,4);
			break;

		}//fim switch

		MeuRect.sizeDelta = new Vector2(156.35f,(float)(66*(numeroArmas+numeroEquipamentos)));

		for(int ajud = 0;ajud<numeroArmas;ajud++){

			GameObject Criacao = Instantiate(ExemploLoot) as GameObject;
			ControladorPainelLootIdividual AjudanteCriacao = Criacao.GetComponent<ControladorPainelLootIdividual>();

			Criacao.transform.SetParent(gameObject.transform);
			Criacao.transform.localScale = new Vector3(1,1,1);
			float ajudanteCalculo = (float)(-35 -65*ajud);
			AjudanteCriacao.MeuRect.anchoredPosition = new Vector2(0,ajudanteCalculo);
			AjudanteCriacao.MeuRect.sizeDelta = new Vector2(132,54);

			Criacao.name = ajud.ToString();

			int sorteio = Random.Range(0,AjudanteContainerArmamento.Tamanho-1);
		
			int dano = Random.Range(AjudanteContainerArmamento.AjudanteListaArmas[sorteio].DanoMin,AjudanteContainerArmamento.AjudanteListaArmas[sorteio].DanoMax);

			int valorRaridade = 0;
			int sorteioRaridade = Random.Range(0,(ChanceLendario+ChanceEpico+ChanceRaro+ChanceMagico+ChanceNormal));

			if(sorteioRaridade<=ChanceLendario){
				valorRaridade = 5;
				Criacao.GetComponent<Image>().color = new Color((float)255/255,(float)174/255,(float)53/255,(float)200/255);
			}else if(sorteioRaridade<=(ChanceLendario+ChanceEpico)){
				valorRaridade = 4;
				Criacao.GetComponent<Image>().color = new Color((float)177/255,(float)53/255,(float)255/255,(float)200/255);
			}else if(sorteioRaridade<=(ChanceLendario+ChanceEpico+ChanceRaro)){
				Criacao.GetComponent<Image>().color = new Color((float)53/255,(float)79/255,(float)255/255,(float)200/255);
				valorRaridade = 3;
			}else if(sorteioRaridade<=(ChanceLendario+ChanceEpico+ChanceRaro+ChanceMagico)){
				Criacao.GetComponent<Image>().color = new Color((float)53/255,(float)255/255,(float)87/255,(float)200/255);
				valorRaridade = 2;
			}else{
				Criacao.GetComponent<Image>().color = new Color((float)197/255,(float)197/255,(float)197/255,(float)200/255);
				valorRaridade = 1;
			}


			//_____________Adicionar os Adicionais__________________

			//DadosArmas.ControleDadosArmas.

			int[] ajudIDOriginal = new int[DadosArmas.ControleDadosArmas.IDOriginal.Length+1];
			int[] ajudDano = new int[DadosArmas.ControleDadosArmas.IDOriginal.Length+1];
			int[] ajudRaridade = new int[DadosArmas.ControleDadosArmas.IDOriginal.Length+1]; // 1 = Nomal , 2 = Incomum, 3 = Raro, 4 = Epico, 5 = Lendario


			for(int ajud2 = 0;ajud2<DadosArmas.ControleDadosArmas.IDOriginal.Length;ajud2++){
				ajudIDOriginal[ajud2] = DadosArmas.ControleDadosArmas.IDOriginal[ajud2];
				ajudDano[ajud2] = DadosArmas.ControleDadosArmas.Dano[ajud2];
				ajudRaridade[ajud2] = DadosArmas.ControleDadosArmas.Raridade[ajud2];
			}
			ajudIDOriginal[DadosArmas.ControleDadosArmas.IDOriginal.Length] = sorteio;
			ajudDano[DadosArmas.ControleDadosArmas.IDOriginal.Length] = dano;
			ajudRaridade[DadosArmas.ControleDadosArmas.IDOriginal.Length] = valorRaridade;


			DadosArmas.ControleDadosArmas.IDOriginal = ajudIDOriginal;
			DadosArmas.ControleDadosArmas.Dano = ajudDano;
			DadosArmas.ControleDadosArmas.Raridade = ajudRaridade;

			AjudanteCriacao.Raridade = valorRaridade;
			AjudanteCriacao.IconeItem.sprite = AjudanteContainerArmamento.AjudanteListaArmas[sorteio].Icone;
			AjudanteCriacao.IconeAtaque.enabled = true;
			AjudanteCriacao.IconeDef.enabled = false;
			AjudanteCriacao.Valor.text = dano.ToString();
			AjudanteCriacao.NomeItem.text = AjudanteContainerArmamento.AjudanteListaArmas[sorteio].Nome;


			Nenhum.enabled = false;
		}//fim For Armas

		for(int ajud = 0;ajud<numeroEquipamentos;ajud++){
			
			GameObject Criacao = Instantiate(ExemploLoot) as GameObject;
			ControladorPainelLootIdividual AjudanteCriacao = Criacao.GetComponent<ControladorPainelLootIdividual>();
			
			Criacao.transform.SetParent(gameObject.transform);
			Criacao.transform.localScale = new Vector3(1,1,1);
			float ajudanteCalculo = (float)(-35 -65*(ajud+numeroArmas));
			AjudanteCriacao.MeuRect.anchoredPosition = new Vector2(0,ajudanteCalculo);
			AjudanteCriacao.MeuRect.sizeDelta = new Vector2(132,54);

			Criacao.name = ajud.ToString();


			int sorteio = Random.Range(0,AjudanteContainerArmaduras.Tamanho-1);
			print(sorteio);
			
			int dano = Random.Range(AjudanteContainerArmaduras.AjudanteListaEquipamentos[sorteio].DefesaMin,AjudanteContainerArmaduras.AjudanteListaEquipamentos[sorteio].DefesaMax);
			
			int valorRaridade = 0;
			int sorteioRaridade = Random.Range(0,(ChanceLendario+ChanceEpico+ChanceRaro+ChanceMagico+ChanceNormal));
			
			if(sorteioRaridade<=ChanceLendario){
				valorRaridade = 5;

				AjudanteCriacao.MinhaCor.color = new Color((float)255/255,(float)174/255,(float)53/255,(float)200/255);
			}else if(sorteioRaridade<=(ChanceLendario+ChanceEpico)){
				valorRaridade = 4;
				AjudanteCriacao.MinhaCor.color = new Color((float)177/255,(float)53/255,(float)255/255,(float)200/255);
			}else if(sorteioRaridade<=(ChanceLendario+ChanceEpico+ChanceRaro)){
				AjudanteCriacao.MinhaCor.color = new Color((float)53/255,(float)79/255,(float)255/255,(float)200/255);
				valorRaridade = 3;
			}else if(sorteioRaridade<=(ChanceLendario+ChanceEpico+ChanceRaro+ChanceMagico)){
				AjudanteCriacao.MinhaCor.color = new Color((float)53/255,(float)255/255,(float)87/255,(float)200/255);
				valorRaridade = 2;
			}else{
				AjudanteCriacao.MinhaCor.color = new Color((float)197/255,(float)197/255,(float)197/255,(float)200/255);
				valorRaridade = 1;
			}
			
			
			//_____________Adicionar os Adicionais__________________
			
			//DadosArmas.ControleDadosArmas.
			
			int[] ajudIDOriginal = new int[DadosInventario.ControleDadosInventario.IDOriginal.Length+1];
			int[] ajudArmadura = new int[DadosInventario.ControleDadosInventario.IDOriginal.Length+1];
			int[] ajudRaridade = new int[DadosInventario.ControleDadosInventario.IDOriginal.Length+1]; // 1 = Nomal , 2 = Incomum, 3 = Raro, 4 = Epico, 5 = Lendario
			
			
			for(int ajud2 = 0;ajud2<DadosInventario.ControleDadosInventario.IDOriginal.Length;ajud2++){
				ajudIDOriginal[ajud2] = DadosInventario.ControleDadosInventario.IDOriginal[ajud2];
				ajudArmadura[ajud2] = DadosInventario.ControleDadosInventario.Armadura[ajud2];
				ajudRaridade[ajud2] = DadosInventario.ControleDadosInventario.Raridade[ajud2];
			}
			ajudIDOriginal[DadosInventario.ControleDadosInventario.IDOriginal.Length] = sorteio;
			ajudArmadura[DadosInventario.ControleDadosInventario.IDOriginal.Length] = dano;
			ajudRaridade[DadosInventario.ControleDadosInventario.IDOriginal.Length] = valorRaridade;
			
			
			DadosInventario.ControleDadosInventario.IDOriginal = ajudIDOriginal;
			DadosInventario.ControleDadosInventario.Armadura = ajudArmadura;
			DadosInventario.ControleDadosInventario.Raridade = ajudRaridade;

			AjudanteCriacao.Raridade = valorRaridade;
			AjudanteCriacao.IconeItem.sprite = AjudanteContainerArmaduras.AjudanteListaEquipamentos[sorteio].Icone;
			AjudanteCriacao.IconeAtaque.enabled = false;
			AjudanteCriacao.IconeDef.enabled = true;
			AjudanteCriacao.Valor.text = dano.ToString();
			AjudanteCriacao.NomeItem.text = AjudanteContainerArmaduras.AjudanteListaEquipamentos[sorteio].Nome;
			
			Nenhum.enabled = false;
		}//fim For Armas


	}


}
