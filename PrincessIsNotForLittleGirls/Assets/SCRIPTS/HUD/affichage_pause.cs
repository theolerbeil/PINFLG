using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_pause : MonoBehaviour {
	
	private Transform[] affiche_Pause;


	// Use this for initialization
	void Start () {
		affiche_Pause = gameObject.GetComponentsInChildren<Transform>(true);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("pause")) {
			if (Time.timeScale == 0) {
				desaffichePause ();
				Time.timeScale = 1;

			} else {
				Time.timeScale = 0;
				affichePause ();
			}

		}
	}

	public void affichePause(){
		foreach (Transform t in affiche_Pause) {
			t.gameObject.SetActive (true);
		}
	}

	public void desaffichePause(){
		foreach (Transform t in affiche_Pause) {
			t.gameObject.SetActive (false);
		}
	}
}
