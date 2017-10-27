using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ia_agent : MonoBehaviour {

    private NavMeshAgent nav;
    private Animator anim;
	private Rigidbody rb;
    private GameObject princesse;
	private princesse_vie princesseVie;
	private princesse_arme princesseArme;
    private ia_pointInteret[] pointsInteret;
	private mob_vie mobVie;
    
    public ia_etat etatCourant;

	public float demiAngleVision;
	public float distanceVision;
	public float rayonAudition;
	public float distanceCombatOptimale;
	public float vitesseAngulaire;
	public ia_etat etatDegatsRecu;
	public ia_etat etatMort;

    void Awake()
    {
        nav = this.GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
		rb = GetComponent<Rigidbody> ();
        princesse = GameObject.FindGameObjectWithTag("Player");
        princesseVie = princesse.GetComponent<princesse_vie>();
		princesseArme = princesse.GetComponent<princesse_arme>();
        pointsInteret = GameObject.FindObjectsOfType<ia_pointInteret>();
		mobVie = GetComponent<mob_vie> ();
    }

    // Use this for initialization
    void Start () {
        etatCourant.entrerEtat();
    }
	
	// Update is called once per frame
	void Update () {

        etatCourant.faireEtat();

	}

	public NavMeshAgent getNav()
	{
		return nav;
	}

	public Animator getAnimator(){
		return anim;
	}

	public Rigidbody getRigidbody()
	{
		return rb;
	}

    public GameObject getPrincesse()
    {
        return princesse;
	}

	public princesse_vie getPrincesse_Vie()
	{
		return princesseVie;
	}

	public princesse_arme getPrincesse_Arme()
	{
		return princesseArme;
	}

	public ia_pointInteret[] getPointsInteret()
	{
		return pointsInteret;
	}

	public mob_vie getMobVie()
	{
		return mobVie;
	}

	/// <summary>
	/// Définit la position de la destination actuel de l'agent.
	/// </summary>
	public void definirDestination(Vector3 positionDestination)
	{
		nav.SetDestination(positionDestination);
	}

	/// <summary>
	/// Définit le point d'interet de destination actuel de l'agent.
	/// </summary>
	public void definirDestination(ia_pointInteret pi)
	{
		nav.SetDestination(pi.transform.position);
	}

    /// <summary>
    /// Définit le nom du point d'interet de destination actuel de l'agent.
    /// </summary>
    public void definirDestination(string nomDestination)
    {
        foreach (ia_pointInteret pi in pointsInteret)
        {
			if (pi.name.Equals(nomDestination))
            {
                nav.SetDestination(pi.transform.position);
                return;
            }
        }
    }

    /// <summary>
    /// Permet de savoir si l'agent a atteint sa destination.
    /// </summary>
    public bool destinationCouranteAtteinte()
    {
		Vector3 v = nav.pathEndPosition - nav.nextPosition;
		v.y = 0;

        return v.magnitude <= nav.stoppingDistance;
	}

	/// <summary>
	/// Tourne l'agent vers la position en fonction de sa vitesse angulaire maximale.
	/// Retourne false si la rotation est finie.
	/// </summary>
	public bool seTournerVersPosition(Vector3 positionCible) {

		Vector3 v = positionCible - this.transform.position;

		return seTournerEnDirectionDe (v);
	}

	/// <summary>
	/// Tourne l'agent vers la direction en fonction de sa vitesse angulaire maximale.
	/// Retourne false si la rotation est finie.
	/// </summary>
	public bool seTournerEnDirectionDe(Vector3 forward) {

		Quaternion q = new Quaternion ();
		q.SetLookRotation (forward);

		return seTourner (q);
	}

	/// <summary>
	/// Tourne l'agent vers l'orientation en fonction de sa vitesse angulaire maximale.
	/// Retourne false si la rotation est finie.
	/// </summary>
	public bool seTournerDansOrientationDe(GameObject obj) {

		return seTourner (obj.transform.rotation);
	}

	private bool seTourner(Quaternion q) {

		float difRotation = q.eulerAngles.y - this.transform.rotation.eulerAngles.y;

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

		return Mathf.Abs(difRotation) > vitesseAngulaire ;
	}

    public void setAnimation(string nomAnimation)
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            anim.SetBool(parameter.name, false);
        }

		anim.SetBool(nomAnimation, true);
    }

	public bool isActualAnimation(string nomAnimation){
		return anim.GetCurrentAnimatorStateInfo (0).IsName (anim.GetLayerName(0) + "." + nomAnimation);
	}

    /// <summary>
    /// Permet de sortir de l'état courant puis d'entrer dans le nouvel état.
    /// </summary>
    public void changerEtat(ia_etat nouvelEtat)
    {
        etatCourant.sortirEtat();
        etatCourant = nouvelEtat;
//		Debug.Log ("Entree état " + etatCourant.ToString());
        etatCourant.entrerEtat();
	}

	/// <summary>
	/// Permet de savoir si l'agent a repéré la princesse en fonction de son audition et de sa vision
	/// </summary>
	public bool princesseReperee(){

		return chercherPrincesse (1.0f);
	}

	/// <summary>
	/// Permet de savoir si l'agent a repéré la princesse en fonction de son audition et de sa vision en faissant vraiment attention
	/// </summary>
	public bool princesseRepereeAvecAttention(){

		return chercherPrincesse (2.0f);
	}

	private bool chercherPrincesse(float niveauAttention){

		Vector3 vecDistancePrincesse = princesse.transform.position - this.transform.position;

		float distancePrincesse = vecDistancePrincesse.magnitude;

		if (distancePrincesse <= this.rayonAudition * niveauAttention) {
			return true;
		}

		float angle = Vector3.Angle (this.transform.forward, vecDistancePrincesse.normalized);

		if(angle <= this.demiAngleVision * niveauAttention) {

			RaycastHit hitInfo;

			Physics.Raycast(this.transform.position, vecDistancePrincesse.normalized, out hitInfo);

			if (hitInfo.distance <= this.distanceVision && hitInfo.collider.gameObject.Equals(princesse)) {
				return true;
			}
		}

		return false;
	}

	public float distanceToPrincesse() {
		return (princesse.transform.position - this.transform.position).magnitude;
	}

	public void recevoirDegat(int valeurDegats) {
		changerEtat (etatDegatsRecu);
		mobVie.blesser (valeurDegats);
	}

	public bool estEnVie() {
		return mobVie.estEnVie ();
	}

	public void mourir() {
		changerEtat (etatMort);
	}

	public bool estAuSol(){

		RaycastHit hitInfo;

		Physics.Raycast (this.transform.position, -this.transform.up, out hitInfo);

		return hitInfo.distance <= 0.065f;
	}
}
