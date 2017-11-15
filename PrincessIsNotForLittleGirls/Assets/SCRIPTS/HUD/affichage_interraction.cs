using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_interraction : MonoBehaviour {

	private HashSet<Arme> arme;
	private HashSet<ObjetProgression> objetProgression;
	private Dictionary<EnumArmes,GameObject> dico;

	// Use this for initialization
	void Start () {
		arme = new HashSet<Arme> ();
		objetProgression = new HashSet<ObjetProgression> ();
		dico = new Dictionary<EnumArmes, GameObject> ();
		foreach(enum_icon enu in GetComponentsInChildren<enum_icon>(true)){
				dico.Add (enu.typeArme, enu.gameObject);
		}

	}

	// Update is called once per frame
	void Update () {
		Debug.Log (arme);
		if (arme.Count>0) {
			var enu = arme.GetEnumerator ();
			enu.MoveNext();
			var a = enu.Current;
			afficheObjet (a.arme);
		} else if (objetProgression.Count>0) {
			afficheObjet (EnumArmes.vide);
		} else {
			desafficheObjet ();
		}
	}

	public void activeObjet(Arme objet){
		arme .Add(objet);
	}

	public void activeObjet(ObjetProgression objet){
		objetProgression.Add(objet);
	}

	public void activeObjet(Objet objet){
		var a = objet as Arme;
		var o = objet as ObjetProgression;
		if (a != null) {
			activeObjet (a);
		} else if (o != null) {
			activeObjet (o);
		}
	}


	public void desactiveObjet(Arme objet){
		arme.Remove (objet);
	}	
	public void desactiveObjet(ObjetProgression objet){
		objetProgression.Remove (objet);
	}

	public void desactiveObjet(Objet objet){
		var a = objet as Arme;
		var o = objet as ObjetProgression;
		if (a != null) {
			desactiveObjet (a);
		} else if (o != null) {
			desactiveObjet (o);
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
