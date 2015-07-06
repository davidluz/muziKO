using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NPCNormal : MonoBehaviour {
	public bool SubRotinas = true;
	public NavMeshAgent AuxiliarNavMesh;
	public Vector3 PosicaoInicial;
	public int Comportamento = 0;
	public Text NomePersonagem;
	public Text AjudaTexto;
	public Image Moldura;
	public int ContadorFalas = 0;
	public int NumeroDeFalas = 0;
	public string [] Falas;
	public string Nome;
	public float Timer = 0.0f;

	void Start(){
		PosicaoInicial = gameObject.transform.position;
	}
	void Update(){

		if (GameMasterFora.Livre == true) {
			if(SubRotinas==true){
				Timer += Time.deltaTime;
				
				if (Timer > 1.0f) {
					switch(Comportamento){
					case 0:
						if(Vector3.Distance(PosicaoInicial,gameObject.transform.position)>2){
							AuxiliarNavMesh.destination = PosicaoInicial;
							Timer = 0;
						}
						break;
					case 1:
						AuxiliarNavMesh.destination = new Vector3((Random.Range(-2,2)+gameObject.transform.position.x),gameObject.transform.position.y,(Random.Range(-2,2)+gameObject.transform.position.z));
						Timer = 0;
						break;
					}//fim switch
				}//fim if
			}//subrotinas

		}//livre

	}

	public void Evento(int numeroEvento){
		switch(numeroEvento){
		case 0:
				print("Teste Evento");
				break;
		}//fim switch
	}//fim evento

	public IEnumerator Falar(){
		GameMasterFora.Livre = false;
		NomePersonagem.text = Nome;
		if (ContadorFalas < NumeroDeFalas) {
			int Comprimento = Falas[ContadorFalas].Length;
			AjudaTexto.text = "";
			for(int x = 0;x<Comprimento;x++){
				AjudaTexto.text =AjudaTexto.text+Falas[ContadorFalas][x];
				yield return new WaitForSeconds(0.01f);
			}

			print(Falas[ContadorFalas]);
			ContadorFalas++;
			
		}
	}//falar
}
