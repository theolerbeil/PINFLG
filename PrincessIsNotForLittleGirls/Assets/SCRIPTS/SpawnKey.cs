using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnKey : MonoBehaviour {

	private mob_vie VieTroll;
	private GameObject Key;
	private bool PremiereFois;
	private float theta;
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
			Key.transform.position = this.transform.position;
			this.GetComponent<CapsuleCollider> ().enabled = false;
			Destroy (GetComponent<Rigidbody> ());
			Key.SetActive (true);
			theta = Random.value * 360;
			Key.GetComponent<Rigidbody> ().AddForce (new Vector3 (Mathf.Cos (theta) * 200 , 200,Mathf.Sin (theta) * 200));
			PremiereFois = false;
		}
	}
}
