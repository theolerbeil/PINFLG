using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_objetActuel : MonoBehaviour {

	private Dictionary<EnumObjetProgression,GameObject> dicoObjet;
	private Dictionary<EnumObjetProgression,int> objetAffiche;



	// Use this for initialization
	void Start () {
		dicoObjet = new Dictionary<EnumObjetProgression, GameObject> ();
		objetAffiche = new Dictionary<EnumObjetProgression,int> ();
		foreach(enum_objetP_icon enu in GetComponentsInChildren<enum_objetP_icon>(true)){
			dicoObjet.Add (enu.typeObjet, enu.gameObject);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void objetRamasse(EnumObjetProgression enu){
		if (objetAffiche.ContainsKey (enu)) {
			objetAffiche [enu]++;
			dicoObjet [enu].GetComponentInChildren<UnityEngine.UI.Text> ().text = "x" + objetAffiche [enu];
		} else {
			dicoObjet [enu].SetActive (true);
			objetAffiche.Add (enu,1);
		}
	}



}
