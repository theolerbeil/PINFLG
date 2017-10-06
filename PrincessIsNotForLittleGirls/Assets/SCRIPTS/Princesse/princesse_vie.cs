using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_vie : MonoBehaviour {

	public int vie_max;
	private int vie_courante;

    private bool gameover;

	// Use this for initialization
	void Start () {
		vie_courante = vie_max;
        gameover = false;

    }
	
	// Update is called once per frame
	void Update () {

		if (!enVie() && !gameover) {
			Debug.Log ("GAME OVER");
            gameover = true;
		}
        
		if (Input.GetKeyDown (KeyCode.X)) {
            soigner(10);

		} else if (Input.GetKeyDown (KeyCode.W)) {
            blesser(10);
		}

    }

    public void soigner(int valeurSoin)
    {
        vie_courante = Mathf.Min(vie_courante + valeurSoin, vie_max);
        Debug.Log("vie courante : " + vie_courante);
    }

    public void blesser(int valeurDegats)
    {
        vie_courante = Mathf.Max(vie_courante - valeurDegats, 0);
        Debug.Log("vie courante : " + vie_courante);
    }

    public bool enVie()
    {
        return vie_courante > 0;
    }
}
