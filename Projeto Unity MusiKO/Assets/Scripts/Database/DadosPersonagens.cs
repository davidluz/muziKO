using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DadosPersonagens : MonoBehaviour {

	public static DadosPersonagens ControleDadosPersonagem;

	public int[] Experiencia;
	public int[] Nivel;
	public float[] VidaAtual;
	public int[,] Equipamento;
	public int[] Arma;
	public bool[] EstouNaParty;

	// Use this for initialization
	void Awake () {

		if(ControleDadosPersonagem == null){

			DontDestroyOnLoad(gameObject);
			ControleDadosPersonagem = this;

		}else if(ControleDadosPersonagem != this){

			Destroy(gameObject);
		}


	}//awake

	public void Save(){

		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/MusiKO/DadosPersonagens.dat");

		DadosJogadores data = new DadosJogadores();
		data.Experiencia = Experiencia;
		data.Nivel = Nivel;
		data.VidaAtual = VidaAtual;
		data.Equipamento = Equipamento;
		data.Arma = Arma;
		data.EstouNaParty = EstouNaParty;

		bf.Serialize(file, data);
		file.Close();

	}//save

	public void Load(){

		if(File.Exists(Application.persistentDataPath + "/MusiKo/DadosPersonagens.dat")){

			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/MusiKO/DadosPersonagens.dat",FileMode.Open);

			DadosJogadores data = (DadosJogadores)bf.Deserialize(file);
			file.Close();

			Experiencia = data.Experiencia;
			Nivel = data.Nivel;
			VidaAtual = data.VidaAtual;
			Equipamento = data.Equipamento;
			Arma = data.Arma;
			EstouNaParty = data.EstouNaParty;
		}

	}//load
}


[Serializable]
class DadosJogadores{

	public int[] Experiencia;
	public int[] Nivel;
	public float[] VidaAtual;
	public int[,] Equipamento;
	public int[] Arma;
	public bool[] EstouNaParty;
}
