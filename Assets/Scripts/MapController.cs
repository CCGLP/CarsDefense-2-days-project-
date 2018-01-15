using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class MapController : MonoBehaviour {
    [SerializeField]
    private Text moneyText; 
	private Vector3 centerMap; 
	private Vector3 sizeMap; 

	private int squareX = 3;
	private int squareY = 6;
    [SerializeField]
    private GameObject chabola, pisos, restaurante, hospital, ayuntamiento, bunker, ataque; 

	[SerializeField]
	private GameObject squarePrefab;

    private GameObject selectPrefab; 
	private bool onMouseClicked = false; 
	private Square currentSelectableSquare; 
	private Square[,] squares;
    private bool colocable = false; 
	// Use this for initialization
	void Start () {
        StaticVariables.buildings = 1; 
		StaticVariables.money = 100; 
		centerMap = GameObject.Find ("Plano").transform.position; 
		sizeMap = GameObject.Find ("Plano").transform.localScale; 
		squares = new Square[(int)sizeMap.x,(int)sizeMap.z]; 
		float x = centerMap.x - sizeMap.x /2; 
		float z = centerMap.z + sizeMap.z /2; 

		for (int i = 0; i < sizeMap.x; i++) {
			for (int j = 0; j < sizeMap.z; j++) {
				squares[i,j] = (((GameObject)Instantiate (squarePrefab, new Vector3 (x, 0.55f, z), squarePrefab.transform.rotation)).GetComponent<Square>());
				squares [i,j].XPosition = i;
				squares [i,j].YPosition = j; 
				z -= 1; 
			}
			z = centerMap.z + sizeMap.z /2; 
			x += 1; 
		}



	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public Vector3 GetRandomPos()
    {
        return new Vector3(Random.Range(centerMap.x - sizeMap.x * 0.5f, centerMap.x + sizeMap.x * 0.5f), 0,
            Random.Range(centerMap.z - sizeMap.z * 0.5f, centerMap.z + sizeMap.z * 0.5f));
    }
    public void SelectSquare(int x, int y){
        if (selectPrefab != null)
        {
            squareX = (int)selectPrefab.transform.localScale.x;
            squareY = (int)selectPrefab.transform.localScale.z; 
            if (currentSelectableSquare != null)
            {
                for (int i = 0; i < squares.GetLength(0); i++)
                {
                    for (int j = 0; j < squares.GetLength(1); j++)
                    {
                        squares[i, j].SetNormalMaterial();
                    }
                }
            }

            if (x + squareX > squares.GetLength(0) )
            {
                colocable = false;
                return;
            }
            else if (y+ squareY > squares.GetLength(1) )
            {
                colocable = false;
                return; 
            }
            for (int i = x; i < x + squareX; i++)
            {
                for (int j = y; j < y + squareY; j++)
                {
                    colocable = squares[i, j].CheckIfColocable();
                    if (!colocable)
                        break;
                }
                if (!colocable)
                    break;
            }

            for (int i = x; i < x + squareX; i++)
            {
                for (int j = y; j < y + squareY; j++)
                {
                    if (colocable)
                        squares[i, j].SetSelectableMaterial();
                    else
                        squares[i, j].SetBadMaterial();
                }
            }
            currentSelectableSquare = squares[x, y];
        }
	}


	public void ActionEndedInSquare(){
        if (selectPrefab != null && colocable)
        {
           UpdateMoney(-selectPrefab.GetComponent<Edificio>().CostMoney);
          
            Vector3 centerPosition = new Vector3(currentSelectableSquare.transform.position.x - currentSelectableSquare.transform.localScale.x / 2 + (float)squareX / 2,
                selectPrefab.transform.localScale.y *0.5f + currentSelectableSquare.transform.localScale.y + currentSelectableSquare.transform.position.y,
                currentSelectableSquare.transform.position.z + currentSelectableSquare.transform.localScale.z /2  - (float)squareY / 2);
            StaticVariables.buildings++;
            Instantiate(selectPrefab, centerPosition, selectPrefab.transform.rotation);
            currentSelectableSquare.SetNormalMaterial();
            currentSelectableSquare = null;
            if (selectPrefab.GetComponent<Edificio>().CostMoney > StaticVariables.money)
                selectPrefab = null; 
        }
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
                squares[i, j].SetNormalMaterial();
            }
        }
        colocable = false;
	}

    public void UpdateMoney(float money)
    {
        StaticVariables.money += money;
        moneyText.text = ((int) StaticVariables.money).ToString();
    }


    public void OnAyuntamientoClicked()
    {
        if (StaticVariables.money >= ayuntamiento.GetComponent<Edificio>().CostMoney)
            selectPrefab = ayuntamiento;
        else
            selectPrefab = null; 
    }

    public void OnChabolaClicked()
    {
        if (StaticVariables.money >= chabola.GetComponent<Edificio>().CostMoney)
            selectPrefab = chabola;
        else
            selectPrefab = null;
    }

    public void OnPisosClicked()
    {
        if (StaticVariables.money >= pisos.GetComponent<Edificio>().CostMoney)
            selectPrefab = pisos;
        else
            selectPrefab = null;
    }

    public void OnRestauranteClicked()
    {
        if (StaticVariables.money >= restaurante.GetComponent<Edificio>().CostMoney)
            selectPrefab = restaurante;
        else
            selectPrefab = null;
    }

    public void OnHospitalClicked()
    {
        if (StaticVariables.money >= hospital.GetComponent<Edificio>().CostMoney)
            selectPrefab = hospital;
        else
            selectPrefab = null;
    }

    public void OnBunkerClicked()
    {
        if (StaticVariables.money >= bunker.GetComponent<Edificio>().CostMoney)
            selectPrefab = bunker;
        else
            selectPrefab = null;
    }

    public void OnAtaqueClicked()
    {
        if (StaticVariables.money >= ataque.GetComponent<Edificio>().CostMoney)
            selectPrefab = ataque;
        else
            selectPrefab = null;
    }

    public void ResetSpawn()
    {
        selectPrefab = null; 
    }

	public bool OnMouseClicked {
		get {
			return onMouseClicked;
		}
		set {
			onMouseClicked = value;
		}
	}





    
}
