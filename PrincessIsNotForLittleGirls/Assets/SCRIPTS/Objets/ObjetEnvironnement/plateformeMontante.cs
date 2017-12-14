using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformeMontante : ObjetEnvironnement {

    public Vector3 posHaut;
    public Vector3 posBas;
    public bool isMoving;
	public bool CanUp;
	public bool CanDown;
	public float vitesse;
	public Animator animLevier;

    // Use this for initialization
    void Start () {
        posBas.x = this.transform.position.x;
        posBas.z = this.transform.position.z;
        posHaut.x = this.transform.position.x;
        posHaut.z = this.transform.position.z;
		CanUp = false;
		CanDown = false;
    }
	
	// Update is called once per frame
	void Update () {
		if ( utilisable == false && CanUp == true)
		{
			Monte();
		} else if (utilisable == false && CanDown == true)
		{
			Descend();
		}
    }

    public void Monte()
    {
		
		gameObject.transform.Translate(Vector3.up * vitesse * Time.deltaTime, Space.World);
        if (this.transform.position.y >= posHaut.y)
        {
			this.gameObject.transform.position = new Vector3(posHaut.x, posHaut.y, posHaut.z);
			utilisable = true;
			GetComponent<AudioSource> ().Stop ();
			CanUp = false;  
        }  
    }

    public void Descend()
    {
		gameObject.transform.Translate(Vector3.down * vitesse * Time.deltaTime, Space.World);
        if (this.transform.position.y <= posBas.y)
        {
			this.gameObject.transform.position = new Vector3(posBas.x, posBas.y, posBas.z);
			utilisable = true;
			GetComponent<AudioSource> ().Stop ();
			CanDown = false; 
        }
    }

    public

    override void Activation()
    {
		
        if ( this.transform.position.y == posBas.y && isMoving == false)
        {
			utilisable = false;
			CanUp = true;
			animLevier.SetBool ("isUp", true);
			animLevier.SetBool ("isDown", false);
			GetComponent<AudioSource> ().Play ();
        } else if (this.transform.position.y == posHaut.y && isMoving == false)
        {
			utilisable = false;
			CanDown = true;
			animLevier.SetBool ("isDown", true);
			animLevier.SetBool ("isUp", false);
			GetComponent<AudioSource> ().Play ();
        }
    }
}
