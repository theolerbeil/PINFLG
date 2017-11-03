using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_interraction : MonoBehaviour {

	private List<Objet> list_objet;
	private Transform[] listImageInterraction;

	// Use this for initialization
	void Start () {
		//image_detection=GetComponent< UnityEngine.UI.Image>();
		listImageInterraction = gameObject.GetComponentsInChildren<Transform>();


	//	image_detection.enabled = false;
		list_objet = new List<Objet>();
	}

	// Update is called once per frame
	void Update () {

		if (list_objet.Count > 0 ) {
			Objet objetfutur = list_objet.ToArray ()[ list_objet.ToArray().Length-1];
			if (objetfutur is Arme) {			
				afficheObjet ("interraction_"+objetfutur.name);
			}else if(objetfutur is ObjetProgression){
				afficheObjet ("interraction_objetProgression");
			}

		} else {
			desafficheObjet ();
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

	private void afficheObjet(string nomImage){
		bool trouve = false;
		foreach(Transform t in listImageInterraction){
			if (t.name.Equals(nomImage)) {
				trouve = true;
				t.gameObject.SetActive (true);
			}
		}
		if (!trouve) {
			afficheObjet ("interraction_objetProgression");
		}
	}

	private void desafficheObjet(){
		for(int i=1;i<listImageInterraction.Length;i++){
			listImageInterraction [i].gameObject.SetActive (false);
		}
	}
		
}
