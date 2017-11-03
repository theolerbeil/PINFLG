using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_ObjetRamasser : MonoBehaviour {

	private bool time;
	private Transform[] listImagePaused;

	// Use this for initialization
	void Start () {
		time = true;
		listImagePaused = gameObject.GetComponentsInChildren<Transform>();
		desaffiche ();

	}

	// Update is called once per frame
	void Update () {


		if (!time) {
			if (Time.timeScale != 0) {
				Time.timeScale = 0;
			} else {
				if (Input.GetKeyDown (KeyCode.E)) {
					time = true;
					desaffiche ();
					Time.timeScale = 1;
				}
			}
		} 
		
	}
		



	public void activeObjet(ObjetProgression objetProgression){
		time = false;
		
		for (int i = 1; i < listImagePaused.Length; i++) {
			switch (listImagePaused [i].name) {
			case "Panel":
				listImagePaused [i].gameObject.SetActive(true);
				break;
			case "Nom":
				listImagePaused [i].gameObject.SetActive (true);
				listImagePaused [i].GetComponent<UnityEngine.UI.Text> ().text = objetProgression.nomObjet;
				break;
			case "Description":
				listImagePaused [i].gameObject.SetActive (true);
				listImagePaused [i].GetComponent<UnityEngine.UI.Text> ().text = objetProgression.descriptionObjet;
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
