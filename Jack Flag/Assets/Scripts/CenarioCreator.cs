using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioCreator : MonoBehaviour
{
    private Transform _boardHolder;

    public int columns = 32;
    public int rows = 32;


    public GameObject[] concretoTiles;
    public GameObject[] aguaTiles;

    public GameObject[] grassTiles;

    public Char[] players;

    void Awake()
    {
        MakeFloor();
        CreatePlayers();
    }


    void CreatePlayers()
    {
        for (int i = 0, len = players.Length; i < len; i++)
        {

            Instantiate(players[i], new Vector3(players[i].startX, 0, players[i].startY), Quaternion.identity);
        }

    }

    void MakeFloor()
    {
        _boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {

                GameObject toInstantiate = grassTiles[0];
                int aux = 7;
                int[] centroAgua1 = new int[] { 23, 8 };
                int raioAgua1 = 5;

                int[] centroArvore1 = new int[] { 10, 13 };
                int[] centroArvore11 = new int[] { 15, 19 };
                int raioArvore1 = 6;
                int raioArvore11 = 10;

                int[] pos = new int[] { x, y };

                if ((x + y < aux) || ((rows - x - 1) + (rows - y - 1) < aux))
                {
                    toInstantiate = concretoTiles[0];
                }
                else if (InsideCircle(centroAgua1, raioAgua1, pos))
                {
                    toInstantiate = aguaTiles[0];
                }
                else if (InsideCircle(centroArvore1, raioArvore1, pos) && !InsideCircle(centroArvore11, raioArvore11, pos))
                {
                    toInstantiate = aguaTiles[0];
                }

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, 0f, y), Quaternion.identity) as GameObject;

                instance.transform.SetParent(_boardHolder);
            }
        }

    }

    bool InsideCircle(int[] center, int radius, int[] position)
    {
        float[] centro = new float[] { center[0], center[1] };
        float raio = radius;
        return Math.Round(Math.Sqrt(Math.Pow(centro[0] - position[0], 2) + Math.Pow(centro[1] - position[1], 2))) <= raio;
    }

    void Update()
    {

    }
}