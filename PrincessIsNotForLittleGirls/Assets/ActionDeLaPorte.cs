using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionDeLaPorte : MonoBehaviour {

	private  Animator anim;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator>();
	}


	void OnTriggerEnter(Collider other)
	{
		if (GameControl.control.ArmeCourante == EnumArmes.bedfoot)
			anim.SetBool("isOpen", true);
	}
	// Update is called once per frame
	void Update () {
		
	}
}
