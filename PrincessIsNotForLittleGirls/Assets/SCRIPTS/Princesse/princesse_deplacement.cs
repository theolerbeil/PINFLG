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
	private bool isPushing;
	private princesse_arme princesseArme;
	private GameObject pushableCube;

	void Start ()
	{
		isPushing = false;
		CanDash = true;
		rb = GetComponent<Rigidbody>();
		anim = GetComponent<Animator> ();
		princesseArme = GetComponent<princesse_arme> ();
	}

	void Update ()
	{

		bool toucheDebug = Input.GetKeyDown(KeyCode.K);

		if (toucheDebug)
		{

		}

		float moveHorizontal = InputManager.GetKeyAxis("Horizontal");
		float moveVertical = InputManager.GetKeyAxis("Vertical");

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


		bool saut = InputManager.GetButtonDown("Jump");
		//	Input.GetKeyDown(KeyCode.Space);

		if (saut && isGrounded == true) {
			rb.AddForce (new Vector3 (0.0f, forceSaut, 0.0f));
			isGrounded = false;
		}
		/*
		Vector3 fwd = transform.TransformDirection (Vector3.down);
		if(Physics.Raycast (transform.position, fwd, feetDist)){
			isGrounded = true;
		}else{
			isGrounded = false;
		}
*/

		bool toucheAttack1 = InputManager.GetButtonDown("Fire1");
		if (toucheAttack1) {
			if (anim.GetBool ("IsIdle") == true) {
				anim.Play ("attack1");
				princesseArme.lancerAttaque ();
			}
			if(anim.GetBool ("IsJumping") == true){
				anim.Play ("attack_jump");
				rb.AddForce (transform.forward * 500f);
				rb.AddForce (new Vector3 (0.0f, -1000f, 0.0f));
				princesseArme.lancerAttaque ();
			}
			if(anim.GetBool ("IsRunning") == true){
				anim.Play ("attack_run");
				princesseArme.lancerAttaque ();
			}
            if (anim.GetBool("IsSidewalk") == true)
            {
                anim.Play("attack_run");
                princesseArme.lancerAttaque();
            }
        }

		if(InputManager.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Fire3")){
			if (CanDash == true && isGrounded == true) {
				anim.Play ("fwdash");
                rb.AddForce(transform.rotation * new Vector3(moveHorizontal, 0f, moveVertical).normalized * 45f, ForceMode.Impulse);
                StartCoroutine(WaitForVelocityZero());
                /*
                if (moveVertical > 0.0f)
				{
					rb.AddForce(transform.rotation * new Vector3(moveHorizontal, 0f, moveVertical).normalized * 50f, ForceMode.Impulse);
                    StartCoroutine(WaitForVelocityZero());
                } else
				{
					rb.AddForce(transform.rotation * new Vector3(moveHorizontal, 0f, moveVertical).normalized * 36.6f, ForceMode.Impulse);
                    StartCoroutine(WaitForVelocityZero());
                }
                */
				CanDash = false;
				StartCoroutine (WaitBeforDash ());

			}
		}

		/*------------------ gerer la poussage du cube --------*/

		if (isPushing == true) {
			anim.SetBool ("isPushing", true);

		} else {
			anim.SetBool ("isPushing", false);

		}


	}

    IEnumerator WaitForVelocityZero()
    {
        yield return new WaitForSeconds(0.3f);
        rb.velocity = Vector3.zero;
    }


    private void GererDeplacement(float moveHorizontal, float moveVertical) {

		if (anim.GetCurrentAnimatorStateInfo (0).IsName (anim.GetLayerName (0) + ".hurt")) {

			return;
		}

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

		if (isPushing == false) {
			this.transform.position += mouvement * vitesse * Time.deltaTime;
		} else {
			this.transform.position += mouvement * vitesse/2 * Time.deltaTime;
			//pushableCube.transform.position += mouvement * vitesse/2 * Time.deltaTime;
		}
	}

	void FixedUpdate(){
		Vector3 fwd = transform.TransformDirection (Vector3.down);
		if(Physics.Raycast (transform.position, fwd, feetDist)){

			anim.SetBool ("IsJumping", false);
		}else{

			anim.SetBool ("IsJumping", true);
		}
	}

	IEnumerator WaitBeforDash(){
		yield return new WaitForSeconds (1f);
		CanDash = true;
	}

	void OnTriggerStay(Collider collision){
		if (collision.tag == "wall" || collision.tag == "cube") {
			isGrounded = true;
		}
		if (collision.tag == "cube") {
			isPushing = true;
			Debug.Log ("touche la caisse");
			//pushableCube = collision.gameObject;
		} 
	}
	void OnTriggerExit(Collider collision){

		if (collision.tag == "cube") {
			isPushing = false;

		} 
	}

}
