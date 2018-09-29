using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;

public class BoardManager : MonoBehaviour {

    [Serializable]
    public class Count
    {
        public int min;
        public int max;

        public Count(int mi, int ma)
        {
            min = mi;
            max = ma;
        }

    }

    public int columns = 32;
    public int rows = 32;
    public Count wallCount = new Count(5, 9);
    public Count foodCount = new Count(1, 5);
    public GameObject exit;
    public GameObject[] concretoTiles;
    public GameObject[] aguaTiles;
    public GameObject[] foodTiles;
    public GameObject[] enemyTiles;
    public GameObject[] outerWallTiles;
    public GameObject[] grassTiles; //Array of grass prefabs.

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3> ();

    void InitializeList()
    {
        gridPositions.Clear();

        for(int x = 1; x < columns - 1; x++)
        {
            for (int y = 1; y < rows - 1; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                
                GameObject toInstantiate = grassTiles[0];
                int aux = 7;
                int[] centroAgua1 = new int[] {23, 8};
                int raioAgua1 = 5;

                int[] centroArvore1 = new int[] {10, 13};
                int[] centroArvore11 = new int[] {15, 19};
                int raioArvore1 = 6;
                int raioArvore11 = 10;

                int[] pos = new int[] {x, y};

                if((x + y < aux) || ((rows - x - 1) + (rows - y - 1) < aux)) {
                    toInstantiate = concretoTiles[0];
                } else if(InsideCircle(centroAgua1, raioAgua1, pos)) {
                    toInstantiate = aguaTiles[0];
                } else if(InsideCircle(centroArvore1, raioArvore1, pos) && ! InsideCircle(centroArvore11, raioArvore11, pos)){
                    toInstantiate = aguaTiles[0];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    bool InsideCircle(int[] center, int radius, int[] position) {
        float[] centro = new float[] {center[0], center[1]};
        float raio = radius;
        return Math.Round(Math.Sqrt(Math.Pow(centro[0] - position[0], 2) + Math.Pow(centro[1] - position[1], 2))) <= raio;
    }

    Vector3 RandomPosition()
    {
        int randomIndex = Random.Range(0, gridPositions.Count);
        Vector3 randomPosition = gridPositions[randomIndex];
        gridPositions.RemoveAt(randomIndex);
        return randomPosition;
    }

    void LayoutObjectAtRandom(GameObject[] tileArray, int minimum, int maximun)
    {
        int ObjectCount = Random.Range(minimum, maximun + 1);

        for (int i = 0; i < ObjectCount; i++)
        {
            Vector3 randomPosition = RandomPosition();
            GameObject tileChoice = tileArray[Random.Range(0, tileArray.Length)];
            Instantiate(tileChoice, randomPosition, Quaternion.identity);
        }
    }

    public void SetupScene(int level)
    {
        BoardSetup();
        InitializeList();
        //LayoutObjectAtRandom(wallTiles, wallCount.min, wallCount.max);
        //LayoutObjectAtRandom(foodTiles, foodCount.min, foodCount.max);
        //int enemyCount = (int)Math.Log(level, 2f);
        //LayoutObjectAtRandom(enemyTiles, enemyCount, enemyCount);
        Instantiate(exit, new Vector3(columns - 1, rows - 1, 0f), Quaternion.identity);
    }

    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
