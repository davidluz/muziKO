using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterCombate : MonoBehaviour {

	public GameObject MestreMagias;
	public ContainerMagias AjudanteMestreMagias;

	public GameObject[] TodasMagias;
	public int ContadorMagias;
	public Magias[] AjudanteMagias;
	public static int NumeroMagia = 0;
	public bool prontoMagia = false;
	//public Image[] ImagensDasMagias;

	public GameObject ContainerMagias;
	public Image[] ContainerIcones;

	public Vector3 AjudanteDaCamera;
	public float VelocidadeCamera;
	public Camera MinhaCamera;
	public static int Opcao = 0;
	public bool prontoMover = false;
	public bool prontoAtacar = false;
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

	public GameObject PainelModelo;
	public PainelPersonagem ajudanteainelModelo;

	// Use this for initialization
	void Start () {
		AjudanteDaCamera = new Vector3(MinhaCamera.transform.position.x,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z);
		AjudanteTurno = new float[ContadorPersonagens];
		AjudanteNavMeshPersonagens = new NavMeshAgent[ContadorPersonagens];
		AjudanteMagias = new Magias[ContadorMagias];
		//ContainerIcones = new Image[ContadorMagias];

		AjudanteMestreMagias = MestreMagias.GetComponent<ContainerMagias>();
		ContadorMagias = AjudanteMestreMagias.Tamanho;
		TodasMagias = AjudanteMestreMagias.ListaMagias;

		ajudanteainelModelo = PainelModelo.GetComponent<PainelPersonagem>();

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
		for(int x = 0;x<ContadorMagias;x++){
			AjudanteMagias[x] = TodasMagias[x].GetComponent<Magias>();
			//ImagensDasMagias[x].sprite = AjudanteMagias[x].Icone.sprite;

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
					AjudanteDaCamera = new Vector3(0+Personagens[x].transform.position.x,10,-10+Personagens[x].transform.position.z);
					Rodando = false;
					Turno = x;
					PersonagensImg [x].rectTransform.anchoredPosition = new Vector2 (301, PersonagensImg [x].rectTransform.anchoredPosition.y);
					FazerPainel(AjudantePersonagens[x].posicaoX,AjudantePersonagens[x].posicaoY);

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
		if(MinhaCamera.transform.position!=AjudanteDaCamera){
			float diferencaX = (MinhaCamera.transform.position.x - AjudanteDaCamera.x)* Time.deltaTime * VelocidadeCamera;
			float diferencaZ = (MinhaCamera.transform.position.z - AjudanteDaCamera.z)* Time.deltaTime * VelocidadeCamera;
			MinhaCamera.transform.position = new Vector3(MinhaCamera.transform.position.x - diferencaX,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z - diferencaZ);

		}
	}//fim Update
	public void ClicarMagia(){
		ContainerMagias.SetActive(true);

		for(int x = 0;x<4;x++){

			ContainerIcones[x].sprite = AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[x]].Icone;
		}
	}
	public void ClicarMagiaEspecifica(){

		for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
			for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
				GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
				if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-ajud)<=AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Alcance){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-ajud1)<=AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Alcance){
						
						GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = true;
						if((AjudantePersonagens[Turno].posicaoX==ajud)&&(AjudantePersonagens[Turno].posicaoY==ajud1)){
							GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
						}else{
							GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.yellow;
						}
						for(int ajud2 =0;ajud2<ContadorPersonagens;ajud2++){
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)&&(AjudantePersonagens[ajud2].SouEu==false)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.red;
							}
						}
						
					}
				}
				
			}
		}//fimfor
		prontoMagia = true;
	}

	public void ClicarAtaque(){
		ContainerMagias.SetActive(false);
		for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
			for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
				GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
				if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-ajud)<=AjudantePersonagens[Turno].QuantidadeAtaque){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-ajud1)<=AjudantePersonagens[Turno].QuantidadeAtaque){
						
						GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = true;
						if((AjudantePersonagens[Turno].posicaoX==ajud)&&(AjudantePersonagens[Turno].posicaoY==ajud1)){
							GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
						}else{
							GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.yellow;
						}
						for(int ajud2 =0;ajud2<ContadorPersonagens;ajud2++){
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)&&(AjudantePersonagens[ajud2].SouEu==false)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.red;
							}
						}
						
					}
				}
				
			}
		}//fimfor
		prontoAtacar = true;


	}
	public void ClicarMover(){
		ContainerMagias.SetActive(false);
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
						for(int ajud2 =0;ajud2<ContadorPersonagens;ajud2++){
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
							}
						}
						
					}
				}
				
			}
		}//fimfor
		prontoMover = true;



	}//clicarMover

	public void FazerPainel(float xTerr,float zTerr){
		ajudanteainelModelo.FazerDescer();
		int indicePersonagem = -1;

		for(int x = 0;x<ContadorPersonagens;x++){

			if((AjudantePersonagens[x].posicaoX==xTerr)&&(AjudantePersonagens[x].posicaoY==zTerr)){
				indicePersonagem = x;
			}

		}
		ajudanteainelModelo.AtualizarInformaçoes(AjudantePersonagens[indicePersonagem].Nome,AjudantePersonagens[indicePersonagem].HPMax,AjudantePersonagens[indicePersonagem].HPAtual,AjudantePersonagens[indicePersonagem].MPMax,AjudantePersonagens[indicePersonagem].MPAtual,AjudantePersonagens[indicePersonagem].MinhaFoto);

		ajudanteainelModelo.FazerSubir();

		//ClicarTerreno(xTerr,zTerr);
	}

	public void ClicarTerreno(float xTerr,float zTerr){
		if(Opcao==0){

		}else if(Opcao==1){
			if(prontoAtacar==true){

				float x = xTerr/5;
				float z = zTerr/5;
				bool podeIr = false;
				int posicaoAtacado = 0;
				for(int ajud = 0;ajud<ContadorPersonagens;ajud++){
					if((ajud!=Turno)&&(AjudantePersonagens[ajud].posicaoX==x)&&(AjudantePersonagens[ajud].posicaoY==z)){
						podeIr = true;
						posicaoAtacado = ajud;
					}
				}//fimfor
				if(podeIr==true){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-x)<=AjudantePersonagens[Turno].QuantidadeAtaque){
						if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-z)<=AjudantePersonagens[Turno].QuantidadeAtaque){
							
							
							if((AjudantePersonagens[Turno].posicaoX==x)&&(AjudantePersonagens[Turno].posicaoY==z)){
								//Nao Faz Nada
							}else{

								//codigo ataque
								//inserir formula ataque
								//inserir Todos as outras coisas para
								AjudantePersonagens[posicaoAtacado].TirarVida(10);

								print(AjudantePersonagens[Turno].Nome+" Atacou "+AjudantePersonagens[posicaoAtacado].Nome);

								for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
									for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
										GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
									}
									
								}//fimfor
								
								prontoMover = false;
								prontoMagia = false;
								prontoAtacar = false;
								Rodando = true;
								AjudanteTurno[Turno] = -429;
								CirculoAcao.SetActive(false);
								ContainerMagias.SetActive(false);
								ajudanteainelModelo.FazerDescer();
								
								//Anda
							}
							
						}
					}
				}

			}//fim pronto atacar
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
								AjudanteDaCamera = new Vector3(x*5,10,z*5 - 10);
								
								
								for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
									for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
										GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
									}
									
								}//fimfor
								
								prontoMover = false;
								prontoMagia = false;
								prontoAtacar = false;
								Rodando = true;
								AjudanteTurno[Turno] = -429;
								print(AjudantePersonagens[Turno].Nome+" Moveu-se");
								CirculoAcao.SetActive(false);
								ContainerMagias.SetActive(false);
								ajudanteainelModelo.FazerDescer();

								//Anda
							}
							
						}
					}
				}

			}
		}else if(Opcao==3){

			if(prontoMagia==true){

				float x = xTerr/5;
				float z = zTerr/5;
				bool podeIr = false;
				int posicaoAtacado = 0;
				for(int ajud = 0;ajud<ContadorPersonagens;ajud++){
					if((ajud!=Turno)&&(AjudantePersonagens[ajud].posicaoX==x)&&(AjudantePersonagens[ajud].posicaoY==z)){
						podeIr = true;
						posicaoAtacado = ajud;
					}
				}//fimfor
				if(podeIr==true){
					if (Mathf.Abs(AjudantePersonagens[Turno].posicaoX-x)<=AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Alcance){
						if (Mathf.Abs(AjudantePersonagens[Turno].posicaoY-z)<=AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Alcance){
							
							
							if((AjudantePersonagens[Turno].posicaoX==x)&&(AjudantePersonagens[Turno].posicaoY==z)){
								//Nao Faz Nada
							}else{
								
								//codigo ataque Magico, Todas as verificaçoes de dano e diversas formulas de dano entram aqui
								//inserir formula ataque
								//inserir Todos as outras coisas para
								AjudantePersonagens[posicaoAtacado].TirarVida(10);
								
								print(AjudantePersonagens[Turno].Nome+" Soltou a Magia "+AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].Nome+" em "+AjudantePersonagens[posicaoAtacado].Nome);
								
								for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
									for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
										GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
									}
									
								}//fimfor
								
								prontoMover = false;
								prontoMagia = false;
								prontoAtacar = false;
								Rodando = true;
								AjudanteTurno[Turno] = -429;
								CirculoAcao.SetActive(false);
								ContainerMagias.SetActive(false);
								ajudanteainelModelo.FazerDescer();
								
								//Anda
							}
							
						}
					}
				}
				
			}//fim pronto Magia

		}

	}//fim clicar terreno

}
