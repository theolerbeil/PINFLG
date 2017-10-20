using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_arme : MonoBehaviour {

    public EnumArmes armeActive;

    private GameObject actualHandArme;
    private GameObject actualWorldArme;

    public GameObject handPoele;
    public GameObject handBread;
    public GameObject handBedfoot;
    public GameObject handChandelier;
	public GameObject handShowel;
	public GameObject handMagicStaff;

//	public Collider colliderArmeActuelle;

	private bool attaqueEnCours;
	private Animator anim;
	private List<ia_agent> listeMobsTouches;

    // Use this for initialization
    void Start () {
		attaqueEnCours = false;
		anim = GetComponent<Animator> ();
		listeMobsTouches = new List<ia_agent> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter(Collider other){

		if (attaqueEnCours) {

			if (anim.GetCurrentAnimatorStateInfo (0).IsName (anim.GetLayerName (0) + ".idle1")) {

				attaqueEnCours = false;
				listeMobsTouches.Clear ();

			} else {

				if (other.tag.Equals ("mob")) {
					
					ia_agent mobTouche = other.gameObject.GetComponent<ia_agent> ();

					if (!listeMobsTouches.Contains (mobTouche) && mobTouche.estEnVie()) {
						
						listeMobsTouches.Add (mobTouche);
						mobTouche.recevoirDegat (100);
					}
				}
			}
		}
	}

	public void SetArmeActive(EnumArmes typeArme, GameObject armeRamasse)
    {
        poserArme();
        armeActive = typeArme;
		defineActualsArmes(armeRamasse);
        activerArme();
    }

	public void lancerAttaque() {
		attaqueEnCours = true;
	}

	private void defineActualsArmes(GameObject armeRamasse)
    {
		actualWorldArme = armeRamasse;
        switch (armeActive)
        {
            case EnumArmes.vide:

                actualHandArme = null;
                actualWorldArme = null;
                break;

            case EnumArmes.poele:

                actualHandArme = handPoele;
                break;

            case EnumArmes.bread:

                actualHandArme = handBread;
                break;

            case EnumArmes.bedfoot:

                actualHandArme = handBedfoot;
                break;

            case EnumArmes.chandelier:

                actualHandArme = handChandelier;
                break;

            case EnumArmes.showel:

                actualHandArme = handShowel;
                break;

			case EnumArmes.magic_staff:

				actualHandArme = handMagicStaff;
				break;
        }
    }

    private void poserArme()
    {
        if(armeActive != EnumArmes.vide)
        {
            actualHandArme.SetActive(false);
            actualWorldArme.transform.SetPositionAndRotation(this.transform.position + this.transform.forward + this.transform.up, new Quaternion());
            actualWorldArme.SetActive(true);
        }
    }

    private void activerArme()
    {
        if(armeActive != EnumArmes.vide)
        {
            actualWorldArme.SetActive(false);
            actualHandArme.SetActive(true);
//			colliderArmeActuelle = actualHandArme.GetComponent<Collider>();
        }
    }
}

public enum EnumArmes
{
    vide,
    poele,
    bread,
    bedfoot,
    chandelier,
    showel,
	magic_staff
}