using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_deplacement : MonoBehaviour {

    public GameObject cam;

	public float vitesse;
	public float forceSaut;
    public float vitesseAngulaire;

	private Rigidbody rb;

	void Start ()
	{
		rb = GetComponent<Rigidbody>();
	}

	void FixedUpdate ()
	{

        bool toucheDebug = Input.GetKeyDown(KeyCode.K);

        if (toucheDebug)
        {
            
        }
        /*
        bool toucheAvant = Input.GetKey(KeyCode.Z);
        bool toucheArriere = Input.GetKey(KeyCode.S);
        bool toucheGauche = Input.GetKey(KeyCode.Q);
        bool toucheDroite = Input.GetKey(KeyCode.D);

        if (toucheAvant || toucheArriere || toucheGauche || toucheDroite)
        {
            GererDeplacement(toucheAvant, toucheArriere, toucheGauche, toucheDroite);
        }
        */
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
        if(moveHorizontal != 0.0f || moveVertical != 0.0f)
        {
            GererDeplacement(moveHorizontal, moveVertical);
        }
        

        bool saut = Input.GetKeyDown(KeyCode.Space);

        if (saut)
        {
            rb.AddForce(new Vector3(0.0f, forceSaut, 0.0f));
        }
    }
    /*
    private void GererDeplacement(bool toucheAvant, bool toucheArriere, bool toucheGauche, bool toucheDroite)
    {
        float difRotation = cam.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y;



        if (toucheAvant)
        {
            
        }

        float rotation;

        if (difRotation > 180.0f)
        {
            difRotation -= 360.0f;
        }

        if (difRotation < -180.0f)
        {
            difRotation += 360.0f;
        }

        rotation = Mathf.Clamp(difRotation, -vitesseAngulaire, vitesseAngulaire);

        this.transform.Rotate(0.0f, rotation, 0.0f);

        // Vector3 mouvement = this.transform.forward * moveVertical + this.transform.right * moveHorizontal;

        Vector3 mouvement = this.transform.forward;

        mouvement.Normalize();

        this.transform.position += mouvement * vitesse;
    }*/

    
    private void GererDeplacement(float moveHorizontal, float moveVertical)
    {
        float difRotation = cam.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y;// + (90.0f * moveHorizontal);



        float rotation;

        if (difRotation > 180.0f)
        {
            difRotation -= 360.0f;
        }

        if (difRotation < -180.0f)
        {
            difRotation += 360.0f;
        }

        rotation = Mathf.Clamp(difRotation, -vitesseAngulaire, vitesseAngulaire);

        this.transform.Rotate(0.0f, rotation, 0.0f);

        Vector3 mouvement = this.transform.forward * moveVertical + this.transform.right * moveHorizontal;

        // Vector3 mouvement = this.transform.forward * moveVertical;

        // Vector3 mouvement = this.transform.forward * Mathf.Min(moveVertical + Mathf.Abs(moveHorizontal), 1.0f);

        mouvement.Normalize();

        this.transform.position += mouvement * vitesse;
    }
    
}
