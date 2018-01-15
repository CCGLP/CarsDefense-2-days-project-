using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement; 
public class RandomCars : MonoBehaviour {

	private Vector3 centerMap;
	private Vector3 sizeMap; 
	private NavMeshAgent agent; 
	[SerializeField]
	private float timeToChange = 5f; 
	private float timer = 0;
    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private float life = 2;
    [SerializeField]
    private int damage = 1;
    private SpawnHandler handler; 
	private Vector3 destination; 
	// Use this for initialization
	void Start () {
        handler = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnHandler>();

        centerMap = GameObject.Find ("Plano").transform.position; 
		sizeMap = GameObject.Find ("Plano").transform.localScale; 
		agent = GetComponent<NavMeshAgent> ();
		destination = GetRandomPos ();
		agent.SetDestination (destination);
	}


	private Vector3 GetRandomPos(){
		return new Vector3 (Random.Range(centerMap.x - sizeMap.x *0.5f, centerMap.x + sizeMap.x * 0.5f), 0, 
			Random.Range(centerMap.z - sizeMap.z * 0.5f, centerMap.z + sizeMap.z * 0.5f));	
	}

	// Update is called once per frame
	void Update () {
		timer += Time.deltaTime; 

		if (timer > timeToChange) {
			timer = 0; 
			destination = GetRandomPos ();
			agent.SetDestination (destination);

		}

		if (Vector3.Distance (this.transform.position, destination) < 0.6f) {
			timer = 0; 
			destination = GetRandomPos ();
			agent.SetDestination (destination);

		}
	}

    void OnTriggerEnter(Collider coll)
    {
        if (coll.gameObject.tag == "Edificio")
        {
            Instantiate(explosion, this.transform.position, explosion.transform.rotation);
            coll.gameObject.GetComponent<Edificio>().OneExplosion(damage);
            handler.DestroyInCarList(this.transform);

            Destroy(this.gameObject);
            

        }
        else if (coll.gameObject.tag == "Bullet")
        {
            Destroy(coll.gameObject);
            life -= 0.5f;
            if (life <= 0)
            {
                Instantiate(explosion, this.transform.position, explosion.transform.rotation);
                handler.DestroyInCarList(this.transform);
                Destroy(this.gameObject);

               
            }

        }
    }
}
