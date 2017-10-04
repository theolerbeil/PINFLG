using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_vie : MonoBehaviour {

	public int vie_max;
	private int vie_courante;

	// Use this for initialization
	void Start () {
		vie_courante = vie_max;
	}
	
	// Update is called once per frame
	void Update () {

		if (vie_courante == 0) {
			Debug.Log ("GAME OVER");
		}

		/*
		bool soin = Input.GetKeyDown (KeyCode.X);
		bool degat = Input.GetKeyDown (KeyCode.W);

		if (soin) {
			vie_courante += 10;
			vie_courante = (vie_courante > vie_max) ? vie_max : vie_courante;
			Debug.Log ("vie courante : " + vie_courante);
		}

		if (degat) {
			vie_courante -= 10;
			vie_courante = (vie_courante < 0) ? 0 : vie_courante;
			Debug.Log ("vie courante : " + vie_courante);
		}*/


		/* déslolé mais ce code est tellement plus opti :D */
		if (Input.GetKeyDown (KeyCode.X) && vie_courante < vie_max) {
			vie_courante += 10;
			Debug.Log ("vie courante : " + vie_courante);

		} else if (Input.GetKeyDown (KeyCode.W) && vie_courante > 0) {
			vie_courante -= 10;
			Debug.Log ("vie courante : " + vie_courante);
		}

	}
}
