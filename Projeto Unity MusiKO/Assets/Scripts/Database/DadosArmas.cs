using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DadosArmas : MonoBehaviour {
	
	public static DadosArmas ControleDadosArmas;
	
	public int[] IDOriginal;
	public int[] Dano;
	public int[] Raridade; // 1 = Nomal , 2 = Incomum, 3 = Raro, 4 = Epico, 5 = Lendario
	public int[,] Adicionais;
	
	// Use this for initialization
	void Awake () {
		
		if(ControleDadosArmas == null){
			
			DontDestroyOnLoad(gameObject);
			ControleDadosArmas = this;
			
		}else if(ControleDadosArmas != this){
			
			Destroy(gameObject);
		}
		
		
	}//awake
	
	public void Save(){
		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/MusiKO/DadosArmas.dat");
		
		DadosArmamento data = new DadosArmamento();
		data.IDOriginal = IDOriginal;
		data.Dano = Dano;
		data.Raridade = Raridade;
		data.Adicionais = Adicionais;
		
		bf.Serialize(file, data);
		file.Close();
		
	}//save
	
	public void Load(){
		
		if(File.Exists(Application.persistentDataPath + "/MusiKo/DadosArmas.dat")){
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/MusiKO/DadosArmas.dat",FileMode.Open);
			
			DadosArmamento data = (DadosArmamento)bf.Deserialize(file);
			file.Close();
			
			IDOriginal = data.IDOriginal;
			Dano = data.Dano;
			Raridade = data.Raridade;
			Adicionais = data.Adicionais;
		}
		
	}//load
}


[Serializable]
class DadosArmamento{
	
	public int[] IDOriginal;
	public int[] Dano;
	public int[] Raridade;
	public int[,] Adicionais;
}
