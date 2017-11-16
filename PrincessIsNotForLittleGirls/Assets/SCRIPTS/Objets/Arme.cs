using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arme : Objet {

    private princesse_arme princesse;
    public EnumArmes arme;
	private affichage_ObjetRamasser affichageObjetRamasser;

    // Use this for initialization
    void Start () {
        princesse = GameObject.FindGameObjectWithTag("Player").GetComponent<princesse_arme>();
		affichageObjetRamasser = GameObject.FindGameObjectWithTag ("Affichage_ObjetRamasser").GetComponent<affichage_ObjetRamasser> ();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    override
    public void Activation()
    {
		if (!princesse.listArmeTenu.Contains (arme)) {
			affichageObjetRamasser.activeObjet (GetComponent<Arme>());
			princesse.listArmeTenu.Add (arme);
			GameControl.control.listArmeTenu.Add (arme);
		}

		princesse.SetArmeActive(arme, this.gameObject);

    }
}
