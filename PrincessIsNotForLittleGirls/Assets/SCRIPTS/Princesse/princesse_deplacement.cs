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
	private Rigidbody rb;
	public float feetDist = 0.1f;
	void Start ()
	{
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
    
}
