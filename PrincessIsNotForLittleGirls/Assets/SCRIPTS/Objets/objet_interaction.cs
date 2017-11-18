using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objet_interaction : MonoBehaviour {

	private GameObject princesse;

    public Objet objet;
	public float distance_activation;
    public float demiAngleActivationFrontal;
	private affichage_interraction hud_refractor;


	// Use this for initialization
	void Start () {
        //image_detection.enabled = false;
        princesse = GameObject.FindGameObjectWithTag("Player");


		hud_refractor = GameObject.FindGameObjectWithTag ("Image_Action").GetComponent<affichage_interraction>();
    }
	
	// Update is called once per frame
	void Update () {
        Vector3 distance_princesse = this.transform.position - princesse.transform.position;

		bool action = InputManager.GetKeyDown (KeyCode.E);


		if (distance_princesse.magnitude < distance_activation) {
			// dans la distance d'activation de l'objet

			float angle = Vector3.Angle (princesse.transform.forward, distance_princesse.normalized);

			if (angle <= demiAngleActivationFrontal) {
				hud_refractor.activeObjet (objet);
				
				//image_detection.enabled = true;
				if (action) {
					objet.Activation ();
					hud_refractor.desactiveObjet (objet);
				}
                
			} else {
				hud_refractor.desactiveObjet (objet);
				//image_detection.enabled = false;
			}
		} else {
			hud_refractor.desactiveObjet (objet);
			//image_detection.enabled = false;
		}
	}
}
