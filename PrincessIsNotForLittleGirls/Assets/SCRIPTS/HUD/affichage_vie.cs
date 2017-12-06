using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class affichage_vie : MonoBehaviour {

	private princesse_vie princesse;
	private Transform[] listImageHeart;

	// Use this for initialization
	void Start () {
		princesse= GameObject.FindGameObjectWithTag("Player").GetComponent<princesse_vie>();
		listImageHeart=gameObject.GetComponentsInChildren<Transform>(true);
	}
	
	// Update is called once per frame
	void Update () {
		//Debug.Log (princesse.getVieCourante ());
		for (int i = 1; i < listImageHeart.Length; i++) {
			if (listImageHeart[i].name == princesse.getVieCourante () + "_heart"){
				listImageHeart[i].gameObject.SetActive (true);
			} else {
				listImageHeart[i].gameObject.SetActive (false);
			}
		}
	}
}
