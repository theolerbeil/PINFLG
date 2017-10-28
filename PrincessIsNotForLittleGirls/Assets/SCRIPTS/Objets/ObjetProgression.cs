using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetProgression : Objet {

	private princesse_objetProgression princesse;
	public EnumObjetProgression objetProgression;

	// Use this for initialization
	void Start () {
		princesse= GameObject.FindGameObjectWithTag("Player").GetComponent<princesse_objetProgression>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation()
	{
		princesse.addItem(objetProgression,this.gameObject);
	}
}
