using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateformeMontante : ObjetEnvironnement {

    public Vector3 posHaut;
    public Vector3 posBas;
    public bool isMoving;
    // Use this for initialization
    void Start () {
        posBas.x = this.transform.position.x;
        posBas.z = this.transform.position.z;
        posHaut.x = this.transform.position.x;
        posHaut.z = this.transform.position.z;
        isMoving = false;
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void Monte()
    {
        while (this.transform.position.y <= posHaut.y)
        {
            gameObject.transform.Translate(Vector3.up * 0.1f * Time.deltaTime, Space.World);
           
        }
        this.gameObject.transform.position = new Vector3(posHaut.x, posHaut.y, posHaut.z);
        isMoving = false;
    }

    public void Descend()
    {
        while (this.transform.position.y >= posBas.y)
        {

            gameObject.transform.Translate(Vector3.down * 0.1f * Time.deltaTime, Space.World);

        }
        this.gameObject.transform.position = new Vector3(posBas.x, posBas.y, posBas.z);
        isMoving = false;
    }

    public

    override void Activation()
    {
        if ( this.transform.position.y == posBas.y && isMoving == false)
        {
            isMoving = true;
            Monte();
        } else if (this.transform.position.y == posHaut.y && isMoving == false)
        {
            isMoving = true;
            Descend();
        }
    }
}
