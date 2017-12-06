using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class footSound : MonoBehaviour {

	public AudioClip[] bruitsPas;
	public float minPitch;
	public float maxPitch;
	public float minVolume;
	public float maxVolume;

	private SoundManager sm;
	private bool isTriggered;

	// Use this for initialization
	void Start () {
		sm = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager>();
		isTriggered = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		if (!isTriggered && other.tag.Equals ("wall")) {

			isTriggered = true;
			int indice = Random.Range (0, this.bruitsPas.Length);
			float volume = Random.Range (minVolume, maxVolume);
			float pitch = Random.Range (minPitch, maxPitch);
	//		sm.playOneShot(this.bruitsPas[indice], volume, pitch);
		}
	}

	void OnTriggerExit(Collider other){

		if (isTriggered && other.tag.Equals ("wall")) {
			isTriggered = false;
		}
	}
}
