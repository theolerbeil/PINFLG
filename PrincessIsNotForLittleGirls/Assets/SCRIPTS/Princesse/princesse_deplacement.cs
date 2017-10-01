using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_deplacement : MonoBehaviour {

    public GameObject cam;
	static Animator annim;
	public float vitesse;
	public float forceSaut;
    public float vitesseAngulaire;
	public bool isGrounded;
	private Rigidbody rb;
	public float feetDist = 0.1f;
	void Start ()
	{
		rb = GetComponent<Rigidbody>();
		annim = GetComponent<Animator> ();
	}

	void Update ()
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
        
		if (moveHorizontal != 0.0f || moveVertical != 0.0f) {
			GererDeplacement (moveHorizontal, moveVertical);
			if (annim.GetBool ("IsJumping") == false) {
				if (moveHorizontal < 0.0f && moveVertical == 0.0f || moveHorizontal > 0.0f && moveVertical == 0.0f) {
					annim.SetBool ("IsSidewalk", true);
					annim.SetBool ("IsBackwalk", false);
					annim.SetBool ("IsRunning", false);
					annim.SetBool ("IsIdle", false);
				} else if (moveVertical < 0.0f && moveHorizontal == 0.0f) {
					annim.SetBool ("IsBackwalk", true);
					annim.SetBool ("IsSidewalk", false);
					annim.SetBool ("IsRunning", false);
					annim.SetBool ("IsIdle", false);
				} else if (moveVertical > 0.0f) {
					annim.SetBool ("IsBackwalk", false);
					annim.SetBool ("IsSidewalk", false);
					annim.SetBool ("IsRunning", true);
					annim.SetBool ("IsIdle", false);
				}
			} else {
				annim.SetBool ("IsRunning", false);
				annim.SetBool ("IsBackwalk", false);
				annim.SetBool ("IsSidewalk", false);
				annim.SetBool ("IsIdle", false);
			}
		} else {
			annim.SetBool ("IsRunning", false);
			annim.SetBool ("IsBackwalk", false);
			annim.SetBool ("IsSidewalk", false);
			if (isGrounded == false) {
				annim.SetBool ("IsIdle", false);
			} else {
				annim.SetBool ("IsIdle", true);
			}
		}
        

        bool saut = Input.GetKeyDown(KeyCode.Space);

		if (saut && isGrounded == true) {
			rb.AddForce (new Vector3 (0.0f, forceSaut, 0.0f));
			isGrounded = false;
		}

		Vector3 fwd = transform.TransformDirection (Vector3.down);
		if(Physics.Raycast (transform.position, fwd, feetDist)){
			isGrounded = true;
		}else{
			isGrounded = false;
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

	void FixedUpdate(){
		Vector3 fwd = transform.TransformDirection (Vector3.down);
		if(Physics.Raycast (transform.position, fwd, feetDist)){
			isGrounded = true;
			annim.SetBool ("IsJumping", false);
		}else{
			isGrounded = false;
			annim.SetBool ("IsJumping", true);
		}
	}
    
}
