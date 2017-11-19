using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_ObjetRamasser : MonoBehaviour {

	private bool time;
	private Transform[] listImagePaused;
	private Dictionary<EnumObjetProgression,GameObject> dicoObjet;
	private Dictionary<EnumArmes,GameObject> dicoArme;

	// Use this for initialization
	void Start () {
		time = true;
		listImagePaused = gameObject.GetComponentsInChildren<Transform>(true);
		dicoObjet = new Dictionary<EnumObjetProgression, GameObject> ();
		foreach(enum_objetP_icon enu in GetComponentsInChildren<enum_objetP_icon>(true)){
			dicoObjet.Add (enu.typeObjet, enu.gameObject);
		}
		dicoArme = new Dictionary<EnumArmes, GameObject> ();
		foreach(enum_arme_icon enu in GetComponentsInChildren<enum_arme_icon>(true)){
			dicoArme.Add (enu.typeArme, enu.gameObject);
		}


	}

	// Update is called once per frame
	void Update () {



		if (!time) {
			if (Time.timeScale != 0) {
				Time.timeScale = 0;
			} else {
				if (Input.GetButtonDown("Cancel")) {
					time = true;
					desaffiche ();
					Time.timeScale = 1;
				}
			}
		} 
		
	}
		
	private void affichageObjet(Arme arme){
		if (dicoArme.ContainsKey (arme.arme)) {
			dicoArme [arme.arme].SetActive (true);
		}
	}

	private void affichageObjet(ObjetProgression objetProgression ){
		if (dicoObjet.ContainsKey (objetProgression.objetProgression)) {
			dicoObjet [objetProgression.objetProgression].SetActive (true);
		}
	}


	public void activeObjet(Objet objet){
		time = false;

		Arme arme = objet as Arme;
		ObjetProgression objetProgression = objet as ObjetProgression;

		if (arme != null) {
			affichageObjet (arme);
		} else if (objetProgression != null) {
			affichageObjet (objetProgression);
		}

		
		for (int i = 1; i < listImagePaused.Length; i++) {
			switch (listImagePaused [i].name) {
			case "Panel":
				listImagePaused [i].gameObject.SetActive(true);
				break;
			case "Nom":
				listImagePaused [i].gameObject.SetActive (true);
				listImagePaused [i].GetComponent<UnityEngine.UI.Text> ().text = objet.nomObjet;
				break;
			case "Description":
				listImagePaused [i].gameObject.SetActive (true);
				listImagePaused [i].GetComponent<UnityEngine.UI.Text> ().text = objet.descriptionObjet;
				break;
			}
		}

	}

	public void desaffiche(){
		for (int i = 1; i < listImagePaused.Length; i++) {
			switch (listImagePaused [i].name) {
			case "Nom":
				listImagePaused [i].gameObject.SetActive (false);
				break;
			case "Description":
				listImagePaused [i].gameObject.SetActive (false);
				break;
			default : 
				listImagePaused [i].gameObject.SetActive (false);
				break;
			}
		}
	}
	
}
