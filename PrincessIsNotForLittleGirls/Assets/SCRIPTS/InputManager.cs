using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager {
	
	public static bool GetKeyDown(KeyCode key){
		if (Time.timeScale == 0) {
			return false;
		}
		return Input.GetKeyDown (key);
	}

	public static float GetKeyAxis(string axis){
		if (Time.timeScale == 0) {
			return 0;
		}
		return Input.GetAxis(axis);
	}

	public static bool GetButtonDown(string button){
		if (Time.timeScale == 0) {
			return false;
		}
		return Input.GetButtonDown(button);
	}
}