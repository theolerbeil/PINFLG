using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class GameControl : MonoBehaviour {

	public static GameControl control;

	public float vie;
	public EnumArmes ArmeCourante;


	void Awake(){
		if (control == null) {
			DontDestroyOnLoad (gameObject);
			control = this;
		} else if (control != this) {
			Destroy (gameObject);
		}
	}

	void OnGUI()
	{
		GUI.Label (new Rect (10, 10, 100, 30), "Vie: " + vie);
		GUI.Label (new Rect (10, 30, 150, 60), "Arme Courante: " + ArmeCourante);
	}

	public void Save()
	{
		BinaryFormatter bf = new BinaryFormatter ();
		FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");

		PlayerData data = new PlayerData();
		data.vie = vie;
		data.ArmeCourante = ArmeCourante;

		bf.Serialize(file, data);
		file.Close();
	}

	public void Load()
	{
		if (File.Exists (Application.persistentDataPath + "/playerInfo.dat")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
			PlayerData data = (PlayerData)bf.Deserialize(file);
			file.Close();

			vie = data.vie;
			ArmeCourante = data.ArmeCourante;
		}
	}

}

[Serializable]
class PlayerData
{
	public float vie;
	public EnumArmes ArmeCourante;

}