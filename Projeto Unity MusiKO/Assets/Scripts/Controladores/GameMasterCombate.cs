using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterCombate : MonoBehaviour {
	public Camera MinhaCamera;
	public static int Opcao = 0;
	public bool prontoMover = false;
	public Canvas MeuCanvas;
	public Image Exemplo;
	public int ContadorPersonagens;
	public Image[] PersonagensImg;
	public GameObject Painel;
	public static bool Rodando = true;
	public ControladorTurno[] AjudantePersonagens;
	public float[] AjudanteTurno;
	public GameObject [] Personagens;
    NavMeshAgent[] AjudanteNavMeshPersonagens;
	public int Turno = 0;
	public float Timer = 0;
	public GameObject CirculoAcao;

	// Use this for initialization
	void Start () {
		AjudanteTurno = new float[ContadorPersonagens];
		AjudanteNavMeshPersonagens = new NavMeshAgent[ContadorPersonagens];
		for (int x = 0; x<ContadorPersonagens; x++) {
			AjudanteTurno[x] = -429.0f; 
			AjudantePersonagens[x] = Personagens[x].GetComponent<ControladorTurno>();
			AjudanteNavMeshPersonagens[x] = Personagens[x].GetComponent<NavMeshAgent>();
			PersonagensImg[x] = Instantiate(Exemplo) as Image;
			PersonagensImg[x].transform.SetParent(MeuCanvas.transform);
			PersonagensImg[x].transform.localScale = new Vector3(1,1,1);
			PersonagensImg[x].rectTransform.anchoredPosition = new Vector2(-429,-73); // Inicio -429 Final x 171
			PersonagensImg[x].name = x.ToString();

			PersonagensImg[x].sprite = AjudantePersonagens[x].MinhaFoto;
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (Rodando == true) {
			for (int x = 0; x<ContadorPersonagens; x++) {
				AjudanteTurno [x] = AjudanteTurno [x] + (AjudantePersonagens [x].Agilidade) / 10;
				PersonagensImg [x].rectTransform.anchoredPosition = new Vector2 (AjudanteTurno [x], PersonagensImg [x].rectTransform.anchoredPosition.y);
				if (AjudanteTurno [x] >= 171) {

					CirculoAcao.SetActive(true);
					CirculoAcao.transform.position = new Vector3(Personagens[x].transform.position.x+0.4f,2,Personagens[x].transform.position.z-1);
					MinhaCamera.transform.position = new Vector3(0+Personagens[x].transform.position.x,10,-10+Personagens[x].transform.position.z);
					Rodando = false;
					Turno = x;
					PersonagensImg [x].rectTransform.anchoredPosition = new Vector2 (301, PersonagensImg [x].rectTransform.anchoredPosition.y);

				}
			}//fim for
		} else {
			if(Input.GetKey("space")){

				for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
					for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){

								
								GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
								
								
							}
						}

				Rodando = true;
				AjudanteTurno[Turno] = -429;
				print("Foi o Turno de "+AjudantePersonagens[Turno].Nome);
			}
		}
	}//fim Update

	public void ClicarMover(){

		for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
			for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
				GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
				if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-ajud)<=AjudantePersonagens[Turno].QuantidadeMovimento){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-ajud1)<=AjudantePersonagens[Turno].QuantidadeMovimento){
						
						GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = true;
						if((AjudantePersonagens[Turno].posicaoX==ajud)&&(AjudantePersonagens[Turno].posicaoY==ajud1)){
							GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
						}else{
							GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.green;
						}
						
					}
				}
				
			}
		}//fimfor
		prontoMover = true;



	}//clicarMover
	public void ClicarTerreno(float xTerr,float zTerr){
		if(Opcao==0){

		}else if(Opcao==1){
		}else if(Opcao==2){
			if(prontoMover==true){
				float x = xTerr/5;
				float z = zTerr/5;
				bool podeIr = true;
				for(int ajud = 0;ajud<ContadorPersonagens;ajud++){
					if((ajud!=Turno)&&(AjudantePersonagens[ajud].posicaoX==x)&&(AjudantePersonagens[ajud].posicaoY==z)){
						podeIr = false;
					}
				}//fimfor

				if(podeIr==true){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-x)<=AjudantePersonagens[Turno].QuantidadeMovimento){
						if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-z)<=AjudantePersonagens[Turno].QuantidadeMovimento){
							
							
							if((AjudantePersonagens[Turno].posicaoX==x)&&(AjudantePersonagens[Turno].posicaoY==z)){
								//Nao Faz Nada
							}else{
								AjudantePersonagens[Turno].posicaoX = (int)x;
								AjudantePersonagens[Turno].posicaoY = (int)z;
								AjudanteNavMeshPersonagens[Turno].destination = new Vector3(x*5,1,z*5);
								
								
								for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
									for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
										GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
									}
									
								}//fimfor
								
								prontoMover = false;
								Rodando = true;
								AjudanteTurno[Turno] = -429;
								print("Foi o Turno de "+AjudantePersonagens[Turno].Nome);
								CirculoAcao.SetActive(false);
								
								//Anda
							}
							
						}
					}
				}

			}
		}else if(Opcao==3){
		}

	}//fim clicar terreno

}
