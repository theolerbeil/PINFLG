using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderTest : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collision){
		if (collision.tag == "SceneChange") {
			Application.LoadLevel("scene2");
		}
	}

}
