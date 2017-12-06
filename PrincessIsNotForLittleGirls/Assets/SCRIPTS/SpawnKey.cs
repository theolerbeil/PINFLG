using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour {

	private mob_vie VieTroll;
	private GameObject Key;
	private bool PremiereFois;
	// Use this for initialization
	void Start () {
		VieTroll = GetComponent<mob_vie>() ;
		Key = GameObject.FindGameObjectWithTag ("key");
		PremiereFois = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (VieTroll.estEnVie ()) {
			Key.SetActive (false);
			PremiereFois = true;
		}else if(!VieTroll.estEnVie() && PremiereFois == true){
			Key.SetActive (true);
			PremiereFois = false;
		}
	}
}
