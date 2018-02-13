using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_Script : MonoBehaviour {

    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ExitPuzzledGame()
    {
        Application.Quit();
    }

    public void StartPuzzleGame()
    {
        SceneManager.LoadScene("level1", LoadSceneMode.Single);
    }
}
