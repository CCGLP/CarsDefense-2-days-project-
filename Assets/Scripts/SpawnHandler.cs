using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnHandler : MonoBehaviour {
    private MapController mapController; 
    [SerializeField]
    private GameObject randomCarPrefab, kamikazeCarPrefab;
    [SerializeField]
    private AnimationCurve spawnByTimeRandomCar, spawnByTimeKamikaze;
    private float timerKami = 0; 
    private float timerRandom = 0;
    private float timer = 0;
    [SerializeField]
    private Transform[] spawns;
    List<Transform> cars; 
    public float Timer
    {
        get
        {
            return timer;
        }

       
    }

    // Use this for initialization
    void Start () {
        cars = new List<Transform>();
        mapController = this.GetComponent<MapController>();
	}
	

	// Update is called once per frame
	void Update () {
        timerRandom += Time.deltaTime;
        timer += Time.deltaTime;
        timerKami += Time.deltaTime; 
        if (timerRandom > spawnByTimeRandomCar.Evaluate(timer))
        {
            timerRandom = 0;
           cars.Add(((GameObject) Instantiate(randomCarPrefab, GetSpawnRandom(), randomCarPrefab.transform.rotation)).transform);
        }
        if (timerKami > spawnByTimeKamikaze.Evaluate(timer))
        {
            timerKami = 0;
            cars.Add(((GameObject)Instantiate(kamikazeCarPrefab,GetSpawnRandom(), kamikazeCarPrefab.transform.rotation)).transform);
        }
	}

   public void DestroyInCarList(Transform target)
    {
        cars.Remove(target);
    }
    public Transform NearestCarInRange(Vector3 position, float range)
    {
        for (int i = 0; i< cars.Count; i++)
        {
            if (Vector3.Distance(position, cars[i].position) <= range)
            {
                return cars[i];
            }

        }
        return null; 
    }

    Vector3 GetSpawnRandom()
    {
        return spawns[Random.Range(0, spawns.Length)].position;
    }
}
