using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_arme : MonoBehaviour {

    public EnumArmes armeActive;

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
        activerArme();
    }

    private void poserArme()
    {
        switch (armeActive)
        {
            case EnumArmes.vide:


                break;

            case EnumArmes.poele:

                handPoele.SetActive(false);
                worldPoele.transform.SetPositionAndRotation(this.transform.position + this.transform.forward * 0.5f, new Quaternion());
                worldPoele.SetActive(true);
                break;
        }
    }

    private void activerArme()
    {
        switch (armeActive)
        {
            case EnumArmes.vide:


                break;

            case EnumArmes.poele:

                worldPoele.SetActive(false);
                handPoele.SetActive(true);
                
                break;
        }
    }
}

public enum EnumArmes
{
    vide,
    poele
}