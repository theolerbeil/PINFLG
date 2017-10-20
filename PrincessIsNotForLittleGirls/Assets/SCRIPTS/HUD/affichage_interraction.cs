using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_interraction : MonoBehaviour {

	private UnityEngine.UI.Image image_detection;
	public List<Objet> list_objet;

	// Use this for initialization
	void Start () {
		image_detection=GetComponent< UnityEngine.UI.Image>();
		image_detection.enabled = false;
		list_objet = new List<Objet>();
	}
	
	// Update is called once per frame
	void Update () {
		Debug.Log("change le message");
		if (list_objet.Count > 0) {
			image_detection.enabled = true;
		} else {
			image_detection.enabled = false;
		}
	}

	public void activeObjet(Objet objet){
		if (!list_objet.Contains(objet)) {
			list_objet.Add(objet);
		}
	}

	public void desactiveObjet(Objet objet){
		list_objet.Remove (objet);
	}
		
}
