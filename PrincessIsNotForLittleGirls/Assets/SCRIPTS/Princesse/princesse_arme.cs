using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_arme : MonoBehaviour {

    public EnumArmes armeActive;
	public List<EnumArmes> listArmeTenu;

    private GameObject actualHandArme;
	private GameObject actualWorldArme;

	public int degatsPoele;
	public int degatsBread;
	public int degatsBedfoot;
	public int degatsChandelier;
	public int degatsShowel;
	public int degatsMagicStaff;

	public float facteurReculePoele;
	public float facteurReculeBread;
	public float facteurReculeBedfoot;
	public float facteurReculeChandelier;
	public float facteurReculeShowel;
	public float facteurReculeMagicStaff;

    public GameObject handPoele;
    public GameObject handBread;
    public GameObject handBedfoot;
    public GameObject handChandelier;
	public GameObject handShowel;
	public GameObject handMagicStaff;

	private bool attaqueEnCours;
	private Animator anim;
	private List<ia_agent> listeMobsTouches;

	private int degatsArmeActuelle;
	private float facteurReculeArmeActuelle;

    // Use this for initialization
    void Start () {
		
		attaqueEnCours = false;
		anim = GetComponent<Animator> ();
		listeMobsTouches = new List<ia_agent> ();

		SetArmeActive (GameControl.control.ArmeCourante, CreerUneArmeDepuisLEnum (GameControl.control.ArmeCourante));

		listArmeTenu = new List<EnumArmes> ();
		listArmeTenu = GameControl.control.listArmeTenu;
	}
	
	// Update is called once per frame
	void Update () {
		
		if (attaqueEnCours && anim.GetCurrentAnimatorStateInfo (0).IsName (anim.GetLayerName (0) + ".idle1")) {
			
			attaqueEnCours = false;
			listeMobsTouches.Clear ();

		}
	}

	void OnTriggerEnter(Collider other){

		if (attaqueEnCours) {

			if (other.tag.Equals ("mob")) {
				
				ia_agent mobTouche = other.gameObject.GetComponent<ia_agent> ();

				if (!listeMobsTouches.Contains (mobTouche) && mobTouche.estEnVie()) {
					
					listeMobsTouches.Add (mobTouche);

					Vector3 hitPoint = other.ClosestPoint (this.transform.position);

					mobTouche.recevoirDegat (degatsArmeActuelle, hitPoint);
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

	public bool isAttaqueEnCours() {
		return attaqueEnCours;
	}

	private void defineActualsArmes(GameObject armeRamasse)
    {
		actualWorldArme = armeRamasse;
		GameControl.control.ArmeCourante = armeActive;
		switch (armeActive)
		{
		case EnumArmes.vide:

			actualHandArme = null;
			actualWorldArme = null;
			degatsArmeActuelle = 0;
			facteurReculeArmeActuelle = 0.0f;
			break;

		case EnumArmes.poele:

			actualHandArme = handPoele;
			degatsArmeActuelle = degatsPoele;
			facteurReculeArmeActuelle = facteurReculePoele;
			break;

		case EnumArmes.bread:

			actualHandArme = handBread;
			degatsArmeActuelle = degatsBread;
			facteurReculeArmeActuelle = facteurReculeBread;
			break;

		case EnumArmes.bedfoot:

			actualHandArme = handBedfoot;
			degatsArmeActuelle = degatsBedfoot;
			facteurReculeArmeActuelle = facteurReculeBedfoot;
			break;

		case EnumArmes.chandelier:

			actualHandArme = handChandelier;
			degatsArmeActuelle = degatsChandelier;
			facteurReculeArmeActuelle = facteurReculeChandelier;
			break;

		case EnumArmes.showel:

			actualHandArme = handShowel;
			degatsArmeActuelle = degatsShowel;
			facteurReculeArmeActuelle = facteurReculeShowel;
			break;

		case EnumArmes.magic_staff:

			actualHandArme = handMagicStaff;
			degatsArmeActuelle = degatsMagicStaff;
			facteurReculeArmeActuelle = facteurReculeMagicStaff;
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
        }
    }

	public float getFacteurReculeArmeActuelle(){
		return facteurReculeArmeActuelle;
	}

	public GameObject CreerUneArmeDepuisLEnum(EnumArmes arme)
	{
		GameObject template = null;
		switch (arme) {
		case EnumArmes.bedfoot:
			template = handBedfoot;
			break;
		case EnumArmes.poele:
			template = handPoele;
			break;
		case EnumArmes.vide:
			template = null;
			break;
		case EnumArmes.bread:
			template = handBread;
			break;
		case EnumArmes.chandelier:
			template = handChandelier;
			break;
		case EnumArmes.magic_staff:
			template = handMagicStaff;
			break;
		case EnumArmes.showel:
			template = handShowel;
			break;
		}

		if (template == null)
			return null;
		return GameObject.Instantiate (template);
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