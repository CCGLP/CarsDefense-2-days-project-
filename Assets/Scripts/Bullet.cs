using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {
    [SerializeField]
    private float speed = 10;
    [SerializeField]
    private float range = 30; 
    private Transform target;
    Vector3 direction;
   
	// Use this for initialization
	void Start () {
        SpawnHandler handler = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnHandler>();


        target = handler.NearestCarInRange(this.transform.position, range);
        
        if (target == null)
        {
            Destroy(this.gameObject);
            direction = Vector3.zero;
        }
        else
        {
            direction = target.transform.position - this.transform.position;
            direction.Normalize();
        }
    }
	
	// Update is called once per frame
	void Update () {
       
       this.transform.position += direction * speed * Time.deltaTime;
        
       
	}

#if UNITY_EDITOR
    void OnDrawGizmos()
    {
       // Gizmos.DrawSphere(this.transform.position, range);
    }
#endif
}
