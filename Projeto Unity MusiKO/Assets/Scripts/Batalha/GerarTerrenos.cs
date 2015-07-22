using UnityEngine;
using System.Collections;

public class GerarTerrenos : MonoBehaviour {


	public GameObject[] ListaDeTerrenos;
	public int QuantidadeListaTerrenos = 0;

	public GameObject[,] TerrenosContruidos;
	public int xTerrenos = 0;
	public int yTerrenos = 0;

	public static int[,] MatrizTerrenos;

	public Light ExemploLuz;
	public static Light[,] MatrizLuz;

	// Use this for initialization
	void Start () {

		TerrenosContruidos = new GameObject[xTerrenos,yTerrenos];
		MatrizLuz = new Light[xTerrenos,yTerrenos];
		MatrizTerrenos = new int[xTerrenos,yTerrenos];
		ContruirTerrenos();

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void ContruirTerrenos(){

		for(int x = 0;x<xTerrenos;x++){
			for(int y = 0;y<yTerrenos;y++){
				int NumeroTerreno = Random.Range(0,QuantidadeListaTerrenos);
		
				MatrizTerrenos[x,y] = NumeroTerreno;

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
