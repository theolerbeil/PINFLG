using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_arme : MonoBehaviour {

    public EnumArmes armeActive;

    private GameObject actualHandArme;
    private GameObject actualWorldArme;

    public GameObject handPoele;
    public GameObject worldPoele;

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
        }
    }

    private void poserArme()
    {
        if(armeActive != EnumArmes.vide)
        {
            actualHandArme.SetActive(false);
            actualWorldArme.transform.SetPositionAndRotation(this.transform.position + this.transform.forward * 0.5f, new Quaternion());
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
    poele
}