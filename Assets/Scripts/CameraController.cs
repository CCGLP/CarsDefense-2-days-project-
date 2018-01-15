using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	[System.Serializable]
	public struct CameraMode{
		public Vector3 rotation; 

		public Vector3 position; 

		public Vector3 compensation; 
		public bool isOrto; 
	}

	[SerializeField]
	private CameraMode[] modesToChange; 

	private int indexMode; 
	private new Camera camera; 
	// Use this for initialization
	void Start () {
		camera = this.GetComponent<Camera> (); 
		indexMode = -1; 
		ChangeMode ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void ChangeMode(){
		indexMode++; 
		if (indexMode >= modesToChange.Length) {
			indexMode = 0; 
		}

		this.transform.position = new Vector3(this.transform.position.x, modesToChange [indexMode].position.y, this.transform.position.z) + modesToChange[indexMode].compensation; 
		this.transform.rotation = Quaternion.Euler (modesToChange [indexMode].rotation);
		this.camera.orthographic = modesToChange [indexMode].isOrto;

	}
}
