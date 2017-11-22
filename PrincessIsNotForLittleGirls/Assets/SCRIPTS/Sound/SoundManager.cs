using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

	public AudioClip[] listeClips;
	public AudioClip defaultLevelMusic;
	public float dureeTransition;

	public AudioSource levelAudioSingle;
	public AudioSource levelAudioLoop1;
	public AudioSource levelAudioLoop2;

	private List<AudioSource> listAudioSource;

	private bool changeMusicTo2;
	private bool changeMusicTo1;
	private Queue<AudioClip> musicQueue;

	private float timer;
	private int i;
	private bool paused;

	// Use this for initialization
	void Awake() {
		levelAudioLoop1.clip = defaultLevelMusic;
		levelAudioLoop1.Play ();

		levelAudioLoop2.volume = 0.0f;

		listAudioSource = new List<AudioSource> ();
		musicQueue = new Queue<AudioClip> ();
		changeMusicTo2 = false;
		changeMusicTo1 = false;

		i = 0;
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyDown (KeyCode.O)) {
			i = (i + 1) % listeClips.Length;
			setBackgroundMusic (i);
		}

		if (Input.GetKeyDown (KeyCode.P)) {
			if (paused) {
				paused = false;
				resumeAllSound ();
			} else {
				paused = true;
				pauseAllSound ();
			}
		}
		
		if (!changeMusicTo2 && !changeMusicTo1 && musicQueue.Count > 0) {

			timer = Time.time;

			if (levelAudioLoop1.volume == 1.0f) {
				changeMusicTo2 = true;
				levelAudioLoop2.clip = musicQueue.Dequeue ();
				levelAudioLoop2.Play ();
			} else {
				changeMusicTo1 = true;
				levelAudioLoop1.clip = musicQueue.Dequeue ();
				levelAudioLoop1.Play ();
			}
		}

		if (changeMusicTo2) {
			bool end = switchMusicFromTo (levelAudioLoop1, levelAudioLoop2);
			if (end) {
				changeMusicTo2 = false;
				levelAudioLoop1.Stop ();
			}
		}

		if (changeMusicTo1) {
			bool end = switchMusicFromTo (levelAudioLoop2, levelAudioLoop1);
			if (end) {
				changeMusicTo1 = false;
				levelAudioLoop2.Stop ();
			}
		}
	}

	public void addAudioSource(AudioSource source) {
		listAudioSource.Add (source);
	}

	public void pauseAllSound() {
		foreach(AudioSource a in listAudioSource) {
			a.Pause ();
		}
		levelAudioSingle.Pause ();
		levelAudioLoop1.Pause ();
		levelAudioLoop2.Pause ();
	}

	public void resumeAllSound() {
		foreach(AudioSource a in listAudioSource) {
			a.UnPause ();
		}
		levelAudioSingle.UnPause ();
		levelAudioLoop1.UnPause ();
		levelAudioLoop2.UnPause ();
	}

	public void setBackgroundMusic(int indice) {
		setBackgroundMusic (listeClips [indice]);
	}

	public void setBackgroundMusic(AudioClip music) {
		musicQueue.Enqueue (music);
	}

	public void playOneShot(AudioClip music) {
		playOneShot (music, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume) {
		playOneShot (music, volume, 1.0f);
	}

	public void playOneShot(AudioClip music, float volume, float pitch) {
		levelAudioSingle.pitch = pitch;
		levelAudioSingle.PlayOneShot (music, volume);
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

	private bool switchMusicFromTo(AudioSource from, AudioSource to) {
		float volume = Mathf.Min (1.0f, (Time.time - timer) / dureeTransition);
		from.volume = 1.0f - volume;
		to.volume = volume;

		return volume == 1.0f;
	}
}
