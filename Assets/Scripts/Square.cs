using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour {

	private int xPosition;
	private int yPosition; 
	private MapController mapController; 
	private Renderer rend; 
	[SerializeField]
	private Material selectableMaterial, redMaterial; 
	private Material normalMaterial; 
	bool onMouseClicked; 
	void Start () {
		mapController = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapController> ();
		rend = this.GetComponent<Renderer> (); 
		normalMaterial = rend.material; 

	}

    public bool CheckIfColocable()
    {
        RaycastHit hit;
       return !Physics.Raycast(new Vector3(this.transform.position.x, this.transform.position.y/* + 0.1f + this.transform.localScale.y * 0.5f*/, this.transform.position.z),
            Vector3.up, out hit);

        
    }

    public void SetBadMaterial()
    {
        this.rend.material = redMaterial;
    }

	void OnMouseDown(){
		mapController.OnMouseClicked = true; 
		OnMouseEnter ();
	}
	void OnMouseUp(){
		//print ("Pepe" + " x: " + xPosition +" y: " + yPosition); 
		mapController.ActionEndedInSquare();
		mapController.OnMouseClicked = false; 


	}
	void OnMouseEnter(){
		if (mapController.OnMouseClicked) {
			mapController.SelectSquare (xPosition, yPosition); 

		}
	}



	public void SetNormalMaterial(){
		rend.material = normalMaterial; 
	}

	public void SetSelectableMaterial(){
		rend.material = selectableMaterial; 
	}
		
	public int XPosition {
		get {
			return xPosition;
		}
		set{
			this.xPosition = value; 
		}
	}

	public int YPosition {
		get {
			return yPosition;
		}
		set{
			this.yPosition = value; 
		}
	}

	public override string ToString ()
	{
		return string.Format ("[Square: XPosition={0}, YPosition={1}]", XPosition, YPosition);
	}
}
