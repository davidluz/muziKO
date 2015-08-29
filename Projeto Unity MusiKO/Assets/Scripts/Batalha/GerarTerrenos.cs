using UnityEngine;
using System.Collections;

public class GerarTerrenos : MonoBehaviour {


	public GameObject[] ListaDeTerrenos;
	public Terreno[] AjudanteListaTerrenos;

	public int QuantidadeListaTerrenos = 0;

	public GameObject[,] TerrenosContruidos;
	public int xTerrenos = 0;
	public int yTerrenos = 0;

	public static int[,] MatrizTerrenos;
	public static bool[,] MatrizTerrenosAndaveis;

	public Light ExemploLuz;
	public static Light[,] MatrizLuz;

	// Use this for initialization
	void Start () {

		AjudanteListaTerrenos = new Terreno[QuantidadeListaTerrenos];
		for(int x = 0;x<QuantidadeListaTerrenos;x++){

			AjudanteListaTerrenos[x] = ListaDeTerrenos[x].GetComponent<Terreno>();
		}
		TerrenosContruidos = new GameObject[xTerrenos,yTerrenos];
		MatrizLuz = new Light[xTerrenos,yTerrenos];
		MatrizTerrenos = new int[xTerrenos,yTerrenos];
		MatrizTerrenosAndaveis = new bool[xTerrenos,yTerrenos];
		ContruirTerrenos();

	}


	public void ContruirTerrenos(){

		for(int x = 0;x<xTerrenos;x++){
			for(int y = 0;y<yTerrenos;y++){

				bool Vai = false;
				int NumeroTerreno = 0;
				while(Vai==false){
					NumeroTerreno = Random.Range(0,QuantidadeListaTerrenos);

					if(AjudanteListaTerrenos[NumeroTerreno].Andavel==true){
						Vai = true;
					}else{

						int primeiroContador = 0;
						int segundoContador = 0;
						bool irParaSegunda = false;
						//primeira Verificacao

						if((y>=1)&&(x>=1)&&((y+1)<yTerrenos)&&((x+1)<xTerrenos)){

							if(MatrizTerrenosAndaveis[x,y-1]==false){
								primeiroContador = primeiroContador+1;
							}
							if(MatrizTerrenosAndaveis[x-1,y-1]==false){
								primeiroContador = primeiroContador+1;
							}
							if(MatrizTerrenosAndaveis[x-1,y]==false){
								primeiroContador = primeiroContador+1;
							}
							if(MatrizTerrenosAndaveis[x+1,y+1]==false){
								primeiroContador = primeiroContador+1;
							}
							if(primeiroContador>=2){
								irParaSegunda = true;
							}else{
								Vai = true;
							}
						}else{
							Vai = true;
						}//fim primeira Verificacao

						//segunda verificacao

						if(irParaSegunda==true){
							if((y>=2)&&(x>=2)&&((y+2)<yTerrenos)&&((x+2)<xTerrenos)){

								if(MatrizTerrenosAndaveis[x,y-2]==false){
									segundoContador = segundoContador+1;
								}
								if(MatrizTerrenosAndaveis[x-1,y-2]==false){
									segundoContador = segundoContador+1;
								}
								if(MatrizTerrenosAndaveis[x-2,y-2]==false){
									segundoContador = segundoContador+1;
								}
								if(MatrizTerrenosAndaveis[x-2,y-1]==false){
									segundoContador = segundoContador+1;
								}
								if(MatrizTerrenosAndaveis[x-2,y]==false){
									segundoContador = segundoContador+1;
								}
								if(MatrizTerrenosAndaveis[x-2,y+1]==false){
									segundoContador = segundoContador+1;
								}
								if(MatrizTerrenosAndaveis[x-2,y+2]==false){
									segundoContador = segundoContador+1;
								}
								if(MatrizTerrenosAndaveis[x-1,y+2]==false){
									segundoContador = segundoContador+1;
								}


								if(segundoContador>0){

								}else{
									Vai = true;
								}


							}else{
								Vai = true;
							}

						}//fimsegunda

						// Verificaçao Zonas Spawn
						if(Vai==true){

							//Primeira Zona Spawn
							if((x==10)&&(y==0)){
								Vai=false;
							}
							if((x==11)&&(y==0)){
								Vai=false;
							}
							if((x==12)&&(y==0)){
								Vai=false;
							}

							if((x==10)&&(y==1)){
								Vai=false;
							}
							if((x==11)&&(y==1)){
								Vai=false;
							}
							if((x==12)&&(y==1)){
								Vai=false;
							}

							if((x==10)&&(y==2)){
								Vai=false;
							}
							if((x==11)&&(y==2)){
								Vai=false;
							}
							if((x==12)&&(y==2)){
								Vai=false;
							}

							//SegundaZona Spawn

							if((x==10)&&(y==23)){
								Vai=false;
							}
							if((x==11)&&(y==23)){
								Vai=false;
							}
							if((x==12)&&(y==23)){
								Vai=false;
							}
							
							if((x==10)&&(y==22)){
								Vai=false;
							}
							if((x==11)&&(y==22)){
								Vai=false;
							}
							if((x==12)&&(y==22)){
								Vai=false;
							}
							
							if((x==10)&&(y==21)){
								Vai=false;
							}
							if((x==11)&&(y==21)){
								Vai=false;
							}
							if((x==12)&&(y==21)){
								Vai=false;
							}

							//Terceira Zona Spawn

							if((x==0)&&(y==11)){
								Vai=false;
							}
							if((x==1)&&(y==11)){
								Vai=false;
							}
							if((x==2)&&(y==11)){
								Vai=false;
							}
							
							if((x==0)&&(y==12)){
								Vai=false;
							}
							if((x==1)&&(y==12)){
								Vai=false;
							}
							if((x==2)&&(y==12)){
								Vai=false;
							}
							
							if((x==0)&&(y==13)){
								Vai=false;
							}
							if((x==1)&&(y==13)){
								Vai=false;
							}
							if((x==2)&&(y==13)){
								Vai=false;
							}

							//Quarta Zona Spawn

							if((x==23)&&(y==11)){
								Vai=false;
							}
							if((x==22)&&(y==11)){
								Vai=false;
							}
							if((x==21)&&(y==11)){
								Vai=false;
							}
							
							if((x==23)&&(y==12)){
								Vai=false;
							}
							if((x==22)&&(y==12)){
								Vai=false;
							}
							if((x==21)&&(y==12)){
								Vai=false;
							}
							
							if((x==23)&&(y==13)){
								Vai=false;
							}
							if((x==22)&&(y==13)){
								Vai=false;
							}
							if((x==21)&&(y==13)){
								Vai=false;
							}


						}//Fim Verificaçao Zonas Spawn

					}
				}


				MatrizTerrenos[x,y] = NumeroTerreno;
				MatrizTerrenosAndaveis[x,y] = AjudanteListaTerrenos[NumeroTerreno].Andavel;

				TerrenosContruidos[x,y] = Instantiate(ListaDeTerrenos[NumeroTerreno]) as GameObject;
				TerrenosContruidos[x,y].transform.SetParent(gameObject.transform);
				TerrenosContruidos[x,y].transform.position = new Vector3(x*5,0,y*5);
				TerrenosContruidos[x,y].name = ("Terreno "+NumeroTerreno+" Posicao: "+x+" - "+y);


				MatrizLuz[x,y] = Instantiate(ExemploLuz) as Light;
				MatrizLuz[x,y].transform.SetParent(gameObject.transform);
				MatrizLuz[x,y].transform.position = new Vector3(x*5,10,y*5);
				MatrizLuz[x,y].name = ("Luz "+x+" - "+y);
				MatrizLuz[x,y].enabled = false;
			}
		}
	}//fim Contruir Terrenos

}
