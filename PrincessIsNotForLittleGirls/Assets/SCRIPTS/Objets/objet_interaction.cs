using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objet_interaction : MonoBehaviour {

	public GameObject princesse;
    public Objet objet;
	public float distance_activation;
    public float demiAngleActivationFrontal;
	public UnityEngine.UI.Image image_detection;

	// Use this for initialization
	void Start () {
		image_detection.enabled = false;	
	}
	
	// Update is called once per frame
	void Update () {

        Vector3 distance_princesse = this.transform.position - princesse.transform.position;

		bool action = Input.GetKeyDown (KeyCode.E);



		if (distance_princesse.magnitude < distance_activation) {
			// dans la distance d'activation de l'objet

			float angle = Vector3.Angle (princesse.transform.forward, distance_princesse.normalized);

			if (angle <= demiAngleActivationFrontal) {
				image_detection.enabled = true;
				if (action) {
					objet.Activation ();
					image_detection.enabled = false;
				}
                
			} else {
				image_detection.enabled = false;
			}
		} else {
			image_detection.enabled = false;
		}
	}
}
