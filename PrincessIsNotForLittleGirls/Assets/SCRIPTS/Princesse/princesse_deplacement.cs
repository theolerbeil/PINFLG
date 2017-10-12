using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class princesse_deplacement : MonoBehaviour {

    public GameObject cam;
	static Animator anim;
	public float vitesse;
	public float forceSaut;
    public float vitesseAngulaire;
	public bool isGrounded;
	public float feetDist = 0.1f;

	private bool CanDash;
	private Rigidbody rb;
	private bool DoubleClickZ;
	private bool DoubleClickS;
	private bool DoubleClickQ;
	private bool DoubleClickD;



	void Start ()
	{
		CanDash = true;
		DoubleClickZ = false;
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
	}

	void Update ()
	{
		
        bool toucheDebug = Input.GetKeyDown(KeyCode.K);

        if (toucheDebug)
        {
            
        }
        
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");
        
		if (moveHorizontal != 0.0f || moveVertical != 0.0f) {
			GererDeplacement (moveHorizontal, moveVertical);
			if (anim.GetBool ("IsJumping") == false) {
				if (moveHorizontal < 0.0f && moveVertical == 0.0f || moveHorizontal > 0.0f && moveVertical == 0.0f) {
					anim.SetBool ("IsSidewalk", true);
					anim.SetBool ("IsBackwalk", false);
					anim.SetBool ("IsRunning", false);
					anim.SetBool ("IsIdle", false);
				} else if (moveVertical < 0.0f && moveHorizontal == 0.0f) {
					anim.SetBool ("IsBackwalk", true);
					anim.SetBool ("IsSidewalk", false);
					anim.SetBool ("IsRunning", false);
					anim.SetBool ("IsIdle", false);
				} else if (moveVertical > 0.0f) {
					anim.SetBool ("IsBackwalk", false);
					anim.SetBool ("IsSidewalk", false);
					anim.SetBool ("IsRunning", true);
					anim.SetBool ("IsIdle", false);
				}
			} else {
				anim.SetBool ("IsRunning", false);
				anim.SetBool ("IsBackwalk", false);
				anim.SetBool ("IsSidewalk", false);
				anim.SetBool ("IsIdle", false);
			}
		} else {
			anim.SetBool ("IsRunning", false);
			anim.SetBool ("IsBackwalk", false);
			anim.SetBool ("IsSidewalk", false);
			if (isGrounded == false) {
				anim.SetBool ("IsIdle", false);
			} else {
				anim.SetBool ("IsIdle", true);
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


		bool toucheAttack1 = Input.GetButtonDown("Fire1");
		if (toucheAttack1) {
			if (anim.GetBool ("IsIdle") == true) {
				anim.Play ("attack1");
			}
			if(anim.GetBool ("IsJumping") == true){
				anim.Play ("attack_jump");
				rb.AddForce (transform.forward * 500f);
				rb.AddForce (new Vector3 (0.0f, -1000f, 0.0f));
			}
			if(anim.GetBool ("IsRunning") == true){
				anim.Play ("attack_run");

			}
		}

		if(Input.GetKeyDown(KeyCode.Z)){
			if (DoubleClickZ == true && CanDash == true && isGrounded == true) {
				anim.Play ("fwdash");
				rb.AddForce (transform.forward * 2000f);
				CanDash = false;

				StartCoroutine (WaitBeforDash ());

			} else {
				DoubleClickZ = true;
				StartCoroutine (WaitForDashZ ());
			}
		}
		if(Input.GetKeyDown(KeyCode.S)){
			if (DoubleClickS == true && CanDash == true && isGrounded == true) {
				anim.Play ("backdash");
				rb.AddForce (transform.forward * -1750f);
				CanDash = false;

				StartCoroutine (WaitBeforDash ());

			} else {
				DoubleClickS = true;
				StartCoroutine (WaitForDashS ());
			}
		}
		if(Input.GetKeyDown(KeyCode.D)){
			if (DoubleClickD == true && CanDash == true && isGrounded == true) {
				anim.Play ("rightdash");
				rb.AddForce (transform.right * 2000f);
				CanDash = false;

				StartCoroutine (WaitBeforDash ());

			} else {
				DoubleClickD = true;
				StartCoroutine (WaitForDashD ());
			}
		}
		if(Input.GetKeyDown(KeyCode.Q)){
			if (DoubleClickQ == true && CanDash == true && isGrounded == true) {
				anim.Play ("leftdash");
				rb.AddForce (transform.right * -2000f);
				CanDash = false;

				StartCoroutine (WaitBeforDash ());

			} else {
				DoubleClickQ = true;
				StartCoroutine (WaitForDashQ ());
			}
		}

    }
    
    private void GererDeplacement(float moveHorizontal, float moveVertical)
    {
        float difRotation = cam.transform.rotation.eulerAngles.y - this.transform.rotation.eulerAngles.y;



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

        Vector3 mouvement = this.transform.forward * Mathf.Max(moveVertical, -0.5f);
        float norme = Mathf.Max(mouvement.magnitude, 0.5f);

        mouvement += this.transform.right * moveHorizontal * 0.5f;

        mouvement = (mouvement / mouvement.magnitude) * norme;

        this.transform.position += mouvement * vitesse;
    }

	void FixedUpdate(){
		Vector3 fwd = transform.TransformDirection (Vector3.down);
		if(Physics.Raycast (transform.position, fwd, feetDist)){
			isGrounded = true;
			anim.SetBool ("IsJumping", false);
		}else{
			isGrounded = false;
			anim.SetBool ("IsJumping", true);
		}
	}
	IEnumerator WaitForDashZ(){
		
		yield return new WaitForSeconds (0.2f);
		DoubleClickZ = false;


	}
	IEnumerator WaitForDashS(){

		yield return new WaitForSeconds (0.2f);
		DoubleClickS = false;


	}
	IEnumerator WaitForDashD(){

		yield return new WaitForSeconds (0.2f);
		DoubleClickD = false;


	}
	IEnumerator WaitForDashQ(){

		yield return new WaitForSeconds (0.2f);
		DoubleClickQ = false;


	}
	IEnumerator WaitBeforDash(){

		DoubleClickZ = false;
		DoubleClickD = false;
		DoubleClickQ = false;
		DoubleClickS = false;
		yield return new WaitForSeconds (1f);
		CanDash = true;

	}
    
}
