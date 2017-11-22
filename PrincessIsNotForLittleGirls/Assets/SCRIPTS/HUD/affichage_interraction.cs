using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_interraction : MonoBehaviour {

	private HashSet<Arme> arme;
	private HashSet<ObjetProgression> objetProgression;
	private HashSet<ObjetEnvironnement> objetEnvironnement;
	private Dictionary<EnumArmes,GameObject> dico;


	// Use this for initialization
	void Start () {
		arme = new HashSet<Arme> ();
		objetProgression = new HashSet<ObjetProgression> ();
		objetEnvironnement = new HashSet<ObjetEnvironnement> ();
		dico = new Dictionary<EnumArmes, GameObject> ();
		foreach(enum_arme_icon enu in GetComponentsInChildren<enum_arme_icon>(true)){
			dico.Add (enu.typeArme, enu.gameObject);
		}

	}

	// Update is called once per frame
	void Update () {
		if (arme.Count > 0) {
			var enu = arme.GetEnumerator ();
			enu.MoveNext ();
			var a = enu.Current;
			afficheObjet (a.arme);
		} else if (objetProgression.Count > 0) {
			afficheObjet (EnumArmes.vide);
		} else if (objetEnvironnement.Count > 0) { 
			afficheObjet (EnumArmes.vide);
		}else {
			desafficheObjet ();
		}
	}

	public void activeObjet(Arme objet){
		arme .Add(objet);
	}

	public void activeObjet(ObjetProgression objet){
		objetProgression.Add(objet);
	}

	public void activeObjet(ObjetEnvironnement objet){
		if(objet.utilisable)
		objetEnvironnement.Add(objet);
	}


	public void activeObjet(Objet objet){
		Arme arme = objet as Arme;
		ObjetProgression objetP = objet as ObjetProgression;
		ObjetEnvironnement objetE = objet as ObjetEnvironnement;
		if (arme != null) {
			activeObjet (arme);
		} else if (objetP != null) {
			activeObjet (objetP);
		} else if(objetE != null){
			activeObjet (objetE);
		}
	}


	public void desactiveObjet(Arme objet){
		arme.Remove (objet);
	}	
	public void desactiveObjet(ObjetProgression objet){
		objetProgression.Remove (objet);
	}

	public void desactiveObjet(ObjetEnvironnement objet){
		objetEnvironnement.Remove (objet);
	}

	public void desactiveObjet(Objet objet){
		Arme arme = objet as Arme;
		ObjetProgression objetP = objet as ObjetProgression;
		ObjetEnvironnement objetE = objet as ObjetEnvironnement;
		if (arme != null) {
			desactiveObjet (arme);
		} else if (objetP != null) {
			desactiveObjet (objetP);
		}else if(objetE != null){
			desactiveObjet (objetE);
		}
	}

	private void afficheObjet(EnumArmes nomImage){
		if (dico.ContainsKey (nomImage)) {
			dico [nomImage].SetActive (true);
		} else {
			dico [EnumArmes.vide].SetActive (true);
		}
	}

	private void desafficheObjet(){
		foreach (GameObject game in dico.Values) {
			game.SetActive (false);
		}
	}
		
}
