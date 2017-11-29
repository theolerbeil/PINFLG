using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManagerMenu : MonoBehaviour {
    public string NomDeLaSceneaCharger;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void LancerPartie()
    {
        SceneManager.LoadScene(NomDeLaSceneaCharger);
    }
    public void QuiterJeu()
    {

        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}
