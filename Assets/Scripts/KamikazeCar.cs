using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class KamikazeCar : MonoBehaviour {


   
    private NavMeshAgent agent;
   

    [SerializeField]
    private GameObject explosion;
    [SerializeField]
    private float life = 3;
    [SerializeField]
    private int damage = 2;
    private Transform target;
    private SpawnHandler handler; 
    private Vector3 destination;
    // Use this for initialization
    void Start()
    {
        handler = GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnHandler>();
       target =  GameObject.FindGameObjectWithTag("Edificio").transform;
        agent = GetComponent<NavMeshAgent>();
        destination = target.position;
        agent.SetDestination(destination);
    }


   

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            if (GameObject.FindGameObjectWithTag("Edificio") != null)
            {
                target = GameObject.FindGameObjectWithTag("Edificio").transform;
                destination = target.position;
                agent.SetDestination(destination);
            }
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
