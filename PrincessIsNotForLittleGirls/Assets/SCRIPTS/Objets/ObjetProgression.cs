using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetProgression : Objet {

	private princesse_objetProgression princesse;
	private affichage_ObjetRamasser affichageObjetRamasser;
	public EnumObjetProgression objetProgression;

	// Use this for initialization
	void Start () {
		princesse= GameObject.FindGameObjectWithTag("Player").GetComponent<princesse_objetProgression>();
		affichageObjetRamasser = GameObject.FindGameObjectWithTag ("Affichage_ObjetRamasser").GetComponent<affichage_ObjetRamasser> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation()
	{
		affichageObjetRamasser.activeObjet (GetComponent<ObjetProgression>());
		princesse.addItem(objetProgression,this.gameObject);

	}
}
