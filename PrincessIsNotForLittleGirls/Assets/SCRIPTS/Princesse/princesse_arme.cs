using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_arme : MonoBehaviour {

    public EnumArmes armeActive;

    private GameObject actualHandArme;
    private GameObject actualWorldArme;

    public GameObject handPoele;
    public GameObject worldPoele;

    public GameObject handBread;
    public GameObject worldBread;

    public GameObject handBedfoot;
    public GameObject worldBedfoot;

    public GameObject handChandelier;
    public GameObject worldChandelier;

    public GameObject handShowel;
    public GameObject worldShowel;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetArmeActive(EnumArmes arme)
    {
        poserArme();
        armeActive = arme;
        defineActualsArmes();
        activerArme();
    }

    private void defineActualsArmes()
    {
        switch (armeActive)
        {
            case EnumArmes.vide:

                actualHandArme = null;
                actualWorldArme = null;
                break;

            case EnumArmes.poele:

                actualHandArme = handPoele;
                actualWorldArme = worldPoele;
                break;

            case EnumArmes.bread:

                actualHandArme = handBread;
                actualWorldArme = worldBread;
                break;

            case EnumArmes.bedfoot:

                actualHandArme = handBedfoot;
                actualWorldArme = worldBedfoot;
                break;

            case EnumArmes.chandelier:

                actualHandArme = handChandelier;
                actualWorldArme = worldChandelier;
                break;

            case EnumArmes.showel:

                actualHandArme = handShowel;
                actualWorldArme = worldShowel;
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
}

public enum EnumArmes
{
    vide,
    poele,
    bread,
    bedfoot,
    chandelier,
    showel
}