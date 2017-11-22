using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEntity : MonoBehaviour {

	public AudioClip[] listeClips;

	private AudioSource audio;
	private SoundManager sm;

	void Awake() {
		sm = GameObject.FindGameObjectWithTag ("SoundManager").GetComponent<SoundManager>();
		audio = GetComponent<AudioSource> ();
	}

	// Use this for initialization
	void Start () {
		sm.addAudioSource (this.audio);
	}

	// Update is called once per frame
	void Update () {
		
	}

	public void stop() {
		audio.Stop();
	}

	public void pause() {
		audio.Pause ();
	}

	public void resume() {
		audio.UnPause ();
	}

	public bool isPlaying() {
		return audio.isPlaying;
	}

	public void playOneShot(AudioClip music) {
		playOneShot (music, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume) {
		playOneShot (music, volume, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume, float pitch) {
		audio.pitch = pitch;
		audio.PlayOneShot (music, volume);
	}

	public void playOneShot(int indice) {
		playOneShot (indice, 1.0f);
	}

	public void playOneShot(int indice, float volume) {
		playOneShot (indice, volume, 1.0f);
	}

	public void playOneShot(int indice, float volume, float pitch) {
		playOneShot (listeClips [indice], volume, pitch);
	}
}
