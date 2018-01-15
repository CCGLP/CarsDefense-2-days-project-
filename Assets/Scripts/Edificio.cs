using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Edificio : MonoBehaviour {
    [SerializeField]
    private float moneyBySecond = 1;
    [SerializeField]
    private float timeMoney = 1; 
    private float timer;
    [SerializeField]
    private float costMoney = 10; 
    private MapController controller;
    [SerializeField]
    private int life = 2;

    [SerializeField]
    private GameObject explosion;

    private Renderer rend; 
    public float CostMoney
    {
        get
        {
            return costMoney;
        }

        
    }

    void Start()
    {
        controller = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapController>();
        rend = this.GetComponentInChildren<Renderer>();
    }
	
    public void OneExplosion(int damage)
    {
        life-= damage;
        rend.material.color = new Color(rend.material.color.r + 0.2f, 0, 0, 1);
        if (life <= 0)
        {
            Instantiate(explosion, this.transform.position, explosion.transform.rotation);
            Destroy(this.gameObject);
            StaticVariables.buildings--;
            if (StaticVariables.buildings <= 0)
            {
                PlayerPrefs.SetFloat("time", GameObject.FindGameObjectWithTag("GameController").GetComponent<SpawnHandler>().Timer);
                SceneManager.LoadScene(1);
            }
        }
    }

	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime; 
        if (timer > timeMoney)
        {
            timer = 0;
            controller.UpdateMoney(moneyBySecond);
        }
	}
}
