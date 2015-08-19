using UnityEngine;
using System.Collections;

using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class DadosInventario : MonoBehaviour {
	
	public static DadosInventario ControleDadosInventario;
	
	public int[] IDOriginal;
	public int[] Armadura;
	public int[] Raridade; // 1 = Nomal , 2 = Incomum, 3 = Raro, 4 = Epico, 5 = Lendario
	public int[,] Adicionais;
	
	// Use this for initialization
	void Awake () {
		
		if(ControleDadosInventario == null){
			
			DontDestroyOnLoad(gameObject);
			ControleDadosInventario = this;
			
		}else if(ControleDadosInventario != this){
			
			Destroy(gameObject);
		}
		
		
	}//awake
	
	public void Save(){
		
		BinaryFormatter bf = new BinaryFormatter();
		FileStream file = File.Create(Application.persistentDataPath + "/MusiKO/DadosInventario.dat");
		
		DadosEquipamento data = new DadosEquipamento();
		data.IDOriginal = IDOriginal;
		data.Armadura = Armadura;
		data.Raridade = Raridade;
		data.Adicionais = Adicionais;
		
		bf.Serialize(file, data);
		file.Close();
		
	}//save
	
	public void Load(){
		
		if(File.Exists(Application.persistentDataPath + "/MusiKo/DadosArmas.dat")){
			
			BinaryFormatter bf = new BinaryFormatter();
			FileStream file = File.Open(Application.persistentDataPath + "/MusiKO/DadosInventario.dat",FileMode.Open);
			
			DadosEquipamento data = (DadosEquipamento)bf.Deserialize(file);
			file.Close();
			
			IDOriginal = data.IDOriginal;
			Armadura = data.Armadura;
			Raridade = data.Raridade;
			Adicionais = data.Adicionais;
		}
		
	}//load
}


[Serializable]
class DadosEquipamento{
	
	public int[] IDOriginal;
	public int[] Armadura;
	public int[] Raridade;
	public int[,] Adicionais;
}