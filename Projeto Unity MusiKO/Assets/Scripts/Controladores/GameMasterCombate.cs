using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterCombate : MonoBehaviour {

	//Variaveis Referentes a geraçao automatica de jogadores
	public GameObject ModeloPlayerExemplo;
	public ControladorTurno AjudanteModeloPlayerExemplo;

	public GameObject ContainerDeInimigos;
	public Inimigos[] AjudanteContainerInimigos;

	public GameObject ContainerDePersonagens;
	public Personagens[] AjudanteContainerDePersonagens;




	public int TamanhoTurno = 1000;
	public float LimiteInferior = -429.0f;
	public float LimiteSuperior = 171.0f;
	public float RateDaBarra = 0;

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


	public GameObject Painel;
	public static bool Rodando = true;

	public Image Exemplo;
	public int ContadorPersonagens;
	public Image[] PersonagensImg;
	public ControladorTurno[] AjudantePersonagens;
	public float[] AjudanteTurno;
	public GameObject [] Personagens;
    NavMeshAgent[] AjudanteNavMeshPersonagens;

	public int Turno = 0;
	public float Timer = 0;
	public GameObject CirculoAcao;

	public GameObject PainelModelo;
	public PainelPersonagem ajudanteainelModelo;

	public Text Errinho;
	public GameObject TextoErro;
	Animator AjudanteTextoErro;

	// Use this for initialization
	void Start () {

		ContainerInimigos AjudanteTemporarioInimigos = ContainerDeInimigos.GetComponent<ContainerInimigos>();

		AjudanteContainerInimigos = new Inimigos[AjudanteTemporarioInimigos.ListaInimigos.Length];
		for(int x = 0;x<AjudanteTemporarioInimigos.ListaInimigos.Length;x++){
			AjudanteContainerInimigos[x] = AjudanteTemporarioInimigos.ListaInimigos[x].GetComponent<Inimigos>();
		}

		ContainerPersonagens AjudanteTemporarioPersonagens = ContainerDePersonagens.GetComponent<ContainerPersonagens>();
		
		AjudanteContainerDePersonagens = new Personagens[AjudanteTemporarioPersonagens.ListaPersonagens.Length];
		for(int x = 0;x<AjudanteTemporarioPersonagens.ListaPersonagens.Length;x++){
			AjudanteContainerDePersonagens[x] = AjudanteTemporarioPersonagens.ListaPersonagens[x].GetComponent<Personagens>();
		}

		IniciarGeracaoRandomica();



		PersonagensImg = new Image[ContadorPersonagens];

		AjudanteMestreMagias = MestreMagias.GetComponent<ContainerMagias>();

		ContadorMagias = AjudanteMestreMagias.ListaMagias.Length;
		AjudanteTextoErro = TextoErro.GetComponent<Animator>();

		RateDaBarra = (Mathf.Abs(LimiteSuperior) + Mathf.Abs(LimiteInferior))/TamanhoTurno;

		AjudanteDaCamera = new Vector3(MinhaCamera.transform.position.x,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z);
		AjudanteTurno = new float[ContadorPersonagens];
		AjudanteNavMeshPersonagens = new NavMeshAgent[ContadorPersonagens];
		AjudanteMagias = new Magias[ContadorMagias];
		//ContainerIcones = new Image[ContadorMagias];


		//ContadorMagias = AjudanteMestreMagias.Tamanho;
		TodasMagias = AjudanteMestreMagias.ListaMagias;

		ajudanteainelModelo = PainelModelo.GetComponent<PainelPersonagem>();

		for (int x = 0; x<ContadorPersonagens; x++) {
			AjudanteTurno[x] = 0; 
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
				PersonagensImg [x].rectTransform.anchoredPosition = new Vector2 ((AjudanteTurno [x]*RateDaBarra+LimiteInferior), PersonagensImg [x].rectTransform.anchoredPosition.y);
				if (AjudanteTurno [x] >= TamanhoTurno) {

					AjudanteTextoErro.SetBool("Hit",false);

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
				AjudanteTurno[Turno] = 0;
				print("Foi o Turno de "+AjudantePersonagens[Turno].Nome);
			}
		}
		if(MinhaCamera.transform.position!=AjudanteDaCamera){
			float diferencaX = (MinhaCamera.transform.position.x - AjudanteDaCamera.x)* Time.deltaTime * VelocidadeCamera;
			float diferencaZ = (MinhaCamera.transform.position.z - AjudanteDaCamera.z)* Time.deltaTime * VelocidadeCamera;
			MinhaCamera.transform.position = new Vector3(MinhaCamera.transform.position.x - diferencaX,MinhaCamera.transform.position.y,MinhaCamera.transform.position.z - diferencaZ);

		}
	}//fim Update


	void IniciarGeracaoRandomica(){

		//ContarElementos
		int contador = 0;
		for(int x = 0;x<DadosPersonagens.ControleDadosPersonagem.EstouNaParty.Length;x++){
			if(DadosPersonagens.ControleDadosPersonagem.EstouNaParty[x]==true){
				contador = contador+1;
			}
		}

		for(int x = 0;x<DadosCombate.ControleDadosCombate.NumeroDeInimigos.Length;x++){
			contador = contador + DadosCombate.ControleDadosCombate.NumeroDeInimigos[x];
		}

		ContadorPersonagens = contador;
		Personagens = new GameObject[ContadorPersonagens];
		AjudantePersonagens = new ControladorTurno[ContadorPersonagens];
		contador = 0;

		for(int x = 0;x<DadosPersonagens.ControleDadosPersonagem.EstouNaParty.Length;x++){
			if(DadosPersonagens.ControleDadosPersonagem.EstouNaParty[x]==true){

				Personagens[contador] = Instantiate(ModeloPlayerExemplo) as GameObject;
				Personagens[contador].transform.SetParent(gameObject.transform);
				Personagens[contador].transform.localScale = new Vector3(1,1,1);
				Personagens[contador].transform.position = new Vector3(0,1,0);
				Personagens[contador].name = x.ToString();


				AjudantePersonagens[contador] = Personagens[contador].GetComponent<ControladorTurno>();

				//-------------------Detalhe Mudar a Fonte Assim que For Terminado o Inventario -----------------------------
				AjudantePersonagens[contador].SouEu = true;
				AjudantePersonagens[contador].Nome = AjudanteContainerDePersonagens[x].Nome;
				AjudantePersonagens[contador].Agilidade = (float)AjudanteContainerDePersonagens[x].Agilidade;
				AjudantePersonagens[contador].HPMax = AjudanteContainerDePersonagens[x].HP;
				AjudantePersonagens[contador].HPAtual = AjudanteContainerDePersonagens[x].HP;
				AjudantePersonagens[contador].MPMax = AjudanteContainerDePersonagens[x].MP;
				AjudantePersonagens[contador].MPAtual = AjudanteContainerDePersonagens[x].MP;
				AjudantePersonagens[contador].Stamina = AjudanteContainerDePersonagens[x].Stamina;
				AjudantePersonagens[contador].Forca = AjudanteContainerDePersonagens[x].Forca;
				AjudantePersonagens[contador].Defesa = AjudanteContainerDePersonagens[x].Defesa;
				AjudantePersonagens[contador].MinhaFoto = AjudanteContainerDePersonagens[x].MinhaFoto;
				AjudantePersonagens[contador].QuantidadeMovimento = AjudanteContainerDePersonagens[x].QuantidadeMovimento;
				AjudantePersonagens[contador].QuantidadeAtaque = AjudanteContainerDePersonagens[x].QuantidadeAtaque;
				
				AjudantePersonagens[contador].ListaMagias = AjudanteContainerDePersonagens[x].Magias;
				
				AjudantePersonagens[contador].Dano = 20;
				
				
				AjudantePersonagens[contador].posicaoX = 0;
				AjudantePersonagens[contador].posicaoY = 0;

				switch(contador){

				case 0: 
					AjudantePersonagens[contador].posicaoX = 10;
					AjudantePersonagens[contador].posicaoY = 0;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 1: 
					AjudantePersonagens[contador].posicaoX = 11;
					AjudantePersonagens[contador].posicaoY = 0;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 2: 
					AjudantePersonagens[contador].posicaoX = 12;
					AjudantePersonagens[contador].posicaoY = 0;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 3: 
					AjudantePersonagens[contador].posicaoX = 10;
					AjudantePersonagens[contador].posicaoY = 1;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 4: 
					AjudantePersonagens[contador].posicaoX = 11;
					AjudantePersonagens[contador].posicaoY = 1;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				case 5: 
					AjudantePersonagens[contador].posicaoX = 12;
					AjudantePersonagens[contador].posicaoY = 1;
					Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
					break;
				}
					contador = contador+1;

			}
		}
		int zona1 = 0;
		int zona2 = 0;
		int zona3 = 0;

		for(int x = 0;x<DadosCombate.ControleDadosCombate.NumeroDeInimigos.Length;x++){
			for(int y =0;y<DadosCombate.ControleDadosCombate.NumeroDeInimigos[x];y++){

				Personagens[contador] = Instantiate(ModeloPlayerExemplo) as GameObject;
				Personagens[contador].transform.SetParent(gameObject.transform);
				Personagens[contador].transform.localScale = new Vector3(1,1,1);
				Personagens[contador].transform.position = new Vector3(0,1,0);
				Personagens[contador].name = x.ToString();

				AjudantePersonagens[contador] = Personagens[contador].GetComponent<ControladorTurno>();

				AjudantePersonagens[contador].SouEu = false;
				AjudantePersonagens[contador].Nome = AjudanteContainerInimigos[x].Nome;
				AjudantePersonagens[contador].Agilidade = (float)AjudanteContainerInimigos[x].Agilidade;
				AjudantePersonagens[contador].HPMax = AjudanteContainerInimigos[x].HP;
				AjudantePersonagens[contador].HPAtual = AjudanteContainerInimigos[x].HP;
				AjudantePersonagens[contador].MPMax = AjudanteContainerInimigos[x].MP;
				AjudantePersonagens[contador].MPAtual = AjudanteContainerInimigos[x].MP;
				AjudantePersonagens[contador].Stamina = AjudanteContainerInimigos[x].Stamina;
				AjudantePersonagens[contador].Forca = AjudanteContainerInimigos[x].Forca;
				AjudantePersonagens[contador].Defesa = AjudanteContainerInimigos[x].Defesa;
				AjudantePersonagens[contador].MinhaFoto = AjudanteContainerInimigos[x].MinhaFoto;
				AjudantePersonagens[contador].QuantidadeMovimento = AjudanteContainerInimigos[x].QuantidadeMovimento;
				AjudantePersonagens[contador].QuantidadeAtaque = AjudanteContainerInimigos[x].QuantidadeAtaque;

				AjudantePersonagens[contador].ListaMagias = AjudanteContainerInimigos[x].Magias;

				AjudantePersonagens[contador].Dano = Random.Range((float)AjudanteContainerInimigos[x].DanoMin,(float)AjudanteContainerInimigos[x].DanoMax);

				bool continuar = true;
				int ZonaRandom = 0;
				while(continuar==true){
					ZonaRandom = Random.Range(1,4);

					if((ZonaRandom==1)&&(zona1<6)){
						continuar = false;
					}else if((ZonaRandom==2)&&(zona2<6)){
						continuar = false;
					}else if((ZonaRandom==3)&&(zona3<6)){
						continuar = false;
					}

				}

				switch(ZonaRandom){
					
				case 1: 

					switch(zona1){


					case 0:

						AjudantePersonagens[contador].posicaoX = 10;
						AjudantePersonagens[contador].posicaoY = 23;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 1:

						AjudantePersonagens[contador].posicaoX = 11;
						AjudantePersonagens[contador].posicaoY = 23;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 2:

						AjudantePersonagens[contador].posicaoX = 12;
						AjudantePersonagens[contador].posicaoY = 23;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 3:

						AjudantePersonagens[contador].posicaoX = 10;
						AjudantePersonagens[contador].posicaoY = 22;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 4:

						AjudantePersonagens[contador].posicaoX = 11;
						AjudantePersonagens[contador].posicaoY = 22;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;

					case 5:

						AjudantePersonagens[contador].posicaoX = 12;
						AjudantePersonagens[contador].posicaoY = 22;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);

						break;
						

					}//fim swich interno
					zona1 = zona1+1;
					break;//fim case 1
				case 2: 

					switch(zona2){
						
						
					case 0:
						
						AjudantePersonagens[contador].posicaoX = 0;
						AjudantePersonagens[contador].posicaoY = 11;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 1:
						
						AjudantePersonagens[contador].posicaoX = 1;
						AjudantePersonagens[contador].posicaoY = 11;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 2:
						
						AjudantePersonagens[contador].posicaoX = 0;
						AjudantePersonagens[contador].posicaoY = 12;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 3:
						
						AjudantePersonagens[contador].posicaoX = 1;
						AjudantePersonagens[contador].posicaoY = 12;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 4:
						
						AjudantePersonagens[contador].posicaoX = 0;
						AjudantePersonagens[contador].posicaoY = 13;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 5:
						
						AjudantePersonagens[contador].posicaoX = 1;
						AjudantePersonagens[contador].posicaoY = 13;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
						
					}//fim swich interno
					zona2 = zona2+1;
					break;
				case 3: 


					switch(zona3){
						
						
					case 0:
						
						AjudantePersonagens[contador].posicaoX = 23;
						AjudantePersonagens[contador].posicaoY = 11;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 1:
						
						AjudantePersonagens[contador].posicaoX = 22;
						AjudantePersonagens[contador].posicaoY = 11;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 2:
						
						AjudantePersonagens[contador].posicaoX = 23;
						AjudantePersonagens[contador].posicaoY = 12;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 3:
						
						AjudantePersonagens[contador].posicaoX = 22;
						AjudantePersonagens[contador].posicaoY = 12;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 4:
						
						AjudantePersonagens[contador].posicaoX = 23;
						AjudantePersonagens[contador].posicaoY = 13;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
					case 5:
						
						AjudantePersonagens[contador].posicaoX = 22;
						AjudantePersonagens[contador].posicaoY = 13;
						Personagens[contador].transform.position = new Vector3(AjudantePersonagens[contador].posicaoX*5,1,AjudantePersonagens[contador].posicaoY*5);
						
						break;
						
						
					}//fim swich interno
					zona3 = zona3+1;
					break;
				}





				contador = contador+1;
			}


		}


	}

	public void ClicarMagia(){
		AjudanteTextoErro.SetBool("Hit",false);
		ContainerMagias.SetActive(true);

		for(int x = 0;x<4;x++){

			ContainerIcones[x].sprite = AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[x]].Icone;
		}
	}
	public void ClicarMagiaEspecifica(){
		AjudanteTextoErro.SetBool("Hit",false);
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
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)&&(AjudantePersonagens[ajud2].SouEu==true)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.blue;
							}
						}
						
					}
				}
				
			}
		}//fimfor
		prontoMagia = true;
	}

	public void ClicarAtaque(){
		AjudanteTextoErro.SetBool("Hit",false);
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
							if((AjudantePersonagens[ajud2].posicaoX==ajud)&&(AjudantePersonagens[ajud2].posicaoY==ajud1)&&(AjudantePersonagens[ajud2].SouEu==true)){
								GerarTerrenos.MatrizLuz[ajud,ajud1].color  = Color.blue;
							}
						}
						
					}
				}
				
			}
		}//fimfor
		prontoAtacar = true;


	}
	public void ClicarMover(){
		AjudanteTextoErro.SetBool("Hit",false);
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
				if(GerarTerrenos.MatrizTerrenosAndaveis[ajud,ajud1]==false){
					GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;
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


								Errinho.text = "Nao Posso me Atacar";
								AjudanteTextoErro.SetBool("Hit",true);
				
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

								AjudantePersonagens[Turno].MPAtual = AjudantePersonagens[Turno].MPAtual +10;
								if(AjudantePersonagens[Turno].MPAtual>AjudantePersonagens[Turno].MPMax){
									AjudantePersonagens[Turno].MPAtual = AjudantePersonagens[Turno].MPMax;
								}

								prontoMover = false;
								prontoMagia = false;
								prontoAtacar = false;
								Rodando = true;
								AjudanteTurno[Turno] = 0;
								CirculoAcao.SetActive(false);
								ContainerMagias.SetActive(false);
								ajudanteainelModelo.FazerDescer();
								
								//Anda
							}
							
						}else{
							Errinho.text = "Alvo Invalido";
							AjudanteTextoErro.SetBool("Hit",true);
						}
					}else{
						Errinho.text = "Alvo Invalido";
						AjudanteTextoErro.SetBool("Hit",true);
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

								//AjudanteTextoErro.SetBool("Hit",false);
								Errinho.text = "Ja Estou Nesse Lugar";
								AjudanteTextoErro.SetBool("Hit",true);
							

							}else{

								if(GerarTerrenos.MatrizTerrenosAndaveis[(int)x,(int)z]==true){

									AjudantePersonagens[Turno].posicaoX = (int)x;
									AjudantePersonagens[Turno].posicaoY = (int)z;
									AjudanteNavMeshPersonagens[Turno].destination = new Vector3(x*5,1,z*5);
									AjudanteDaCamera = new Vector3(x*5,10,z*5 - 10);
									
									
									for(int ajud = 0;ajud<GerarTerrenos.MatrizTerrenos.GetLength(0);ajud++){
										for(int ajud1 = 0;ajud1<GerarTerrenos.MatrizTerrenos.GetLength(1);ajud1++){
											GerarTerrenos.MatrizLuz[ajud,ajud1].enabled = false;	
										}
										
									}//fimfor
									
									AjudantePersonagens[Turno].MPAtual = AjudantePersonagens[Turno].MPAtual +20;
									if(AjudantePersonagens[Turno].MPAtual>AjudantePersonagens[Turno].MPMax){
										AjudantePersonagens[Turno].MPAtual = AjudantePersonagens[Turno].MPMax;
									}
									prontoMover = false;
									prontoMagia = false;
									prontoAtacar = false;
									Rodando = true;
									AjudanteTurno[Turno] = 0;
									print(AjudantePersonagens[Turno].Nome+" Moveu-se");
									CirculoAcao.SetActive(false);
									ContainerMagias.SetActive(false);
									ajudanteainelModelo.FazerDescer();
									
									//Anda

								}else{
									Errinho.text = "Local Invalido";
									AjudanteTextoErro.SetBool("Hit",true);
								}


							}
							
						}else{
							Errinho.text = "Local Invalido";
							AjudanteTextoErro.SetBool("Hit",true);
						}
					}else{
						Errinho.text = "Local Invalido";
						AjudanteTextoErro.SetBool("Hit",true);
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


								Errinho.text = "Nao Posso me Atacar";
								AjudanteTextoErro.SetBool("Hit",true);


							}else{
								if(AjudantePersonagens[Turno].MPAtual<AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].CustoMP){
									//Nao Faz Nada
									// Toca som  de Erro
									Errinho.text = "Preciso de Mais Mana";
									AjudanteTextoErro.SetBool("Hit",true);

								}else{
									AjudantePersonagens[Turno].MPAtual = AjudantePersonagens[Turno].MPAtual - AjudanteMagias[AjudantePersonagens[Turno].ListaMagias[NumeroMagia]].CustoMP;
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
									AjudanteTurno[Turno] = 0;
									CirculoAcao.SetActive(false);
									ContainerMagias.SetActive(false);
									ajudanteainelModelo.FazerDescer();
									
									//Anda
								}

							}
							
						}else{
							Errinho.text = "Alvo Invalido";
							AjudanteTextoErro.SetBool("Hit",true);
						}
					}else{
						Errinho.text = "Alvo Invalido";
						AjudanteTextoErro.SetBool("Hit",true);
					}
				}
				
			}//fim pronto Magia

		}

	}//fim clicar terreno

}
