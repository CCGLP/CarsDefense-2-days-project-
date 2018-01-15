using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour {
    [SerializeField]
    private Text textTime; 



	// Use this for initialization
	void Start () {
        textTime.text = "Time: " + ((int)PlayerPrefs.GetFloat("time")).ToString() + "s";
	}
	
	public void OnRestartClick()
    {
        SceneManager.LoadScene(0);
    }
}
