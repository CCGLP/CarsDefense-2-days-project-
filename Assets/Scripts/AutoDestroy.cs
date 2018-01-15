using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {
    [SerializeField]
    private float timeToDestroy = 3;
    private float timer = 0; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        if (timer > timeToDestroy)
        {
            timer = 0;
            Destroy(this.gameObject); 
        }
	}
}
