using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangementDeNiveau : MonoBehaviour {

	public string NomDeLaSceneaCharger;

	void OnTriggerEnter(Collider other){
		GameControl.control.Save ();
		SceneManager.LoadScene (NomDeLaSceneaCharger);
	}

}
