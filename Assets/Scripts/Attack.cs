using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {
    [SerializeField]
    private GameObject bulletPrefab;
    [SerializeField]
    private float rateOfFire;
    private float timer; 
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime; 
        if (timer > rateOfFire)
        {
            timer = 0;
            Instantiate(bulletPrefab, this.transform.position, bulletPrefab.transform.rotation);
        }
	}
}
