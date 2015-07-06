using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameMasterCombate : MonoBehaviour {

	public Canvas MeuCanvas;
	public Image Exemplo;
	public int ContadorPersonagens;
	public Image[] PersonagensImg;
	public GameObject Painel;
	public static bool Rodando = true;
	public ControladorTurno[] AjudantePersonagens;
	public float[] AjudanteTurno;
	public GameObject [] Personagens;
	public int Turno = 0;
	public float Timer = 0;

	// Use this for initialization
	void Start () {
		AjudanteTurno = new float[ContadorPersonagens];
		for (int x = 0; x<ContadorPersonagens; x++) {
			AjudanteTurno[x] = -429.0f; 
			AjudantePersonagens[x] = Personagens[x].GetComponent<ControladorTurno>();
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
					Rodando = false;
					Turno = x;
					PersonagensImg [x].rectTransform.anchoredPosition = new Vector2 (301, PersonagensImg [x].rectTransform.anchoredPosition.y);

				}
			}//fim for
		} else {
			if(Input.GetKey("space")){
				Rodando = true;
				AjudanteTurno[Turno] = -429;
				print("Foi o Turno de "+AjudantePersonagens[Turno].Nome);
			}
		}
	}//fim Update
}
