using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour {

	private mob_vie VieTroll;
	private GameObject Key;
	// Use this for initialization
	void Start () {
		VieTroll = GetComponent<mob_vie>() ;
		Key = GameObject.FindGameObjectWithTag ("key");
	}
	
	// Update is called once per frame
	void Update () {
		if (VieTroll.estEnVie ()) {
			Key.SetActive (false);
		}else{
			Key.SetActive (true);
		}
	}
}
