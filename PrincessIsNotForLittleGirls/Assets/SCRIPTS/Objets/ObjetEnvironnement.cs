using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjetEnvironnement : Objet {

	public EnumObjetEnvironnement objetEnvrironnement;
	public bool utilisable;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	override
	public void Activation()
	{

	}
}

public enum EnumObjetEnvironnement
{
	porte,
}
