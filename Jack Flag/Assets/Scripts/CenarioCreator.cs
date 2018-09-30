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
    public GameObject[] moitaTiles;
    public GameObject[] quinaTile;
    public GameObject[] blocoxTile;
    public GameObject[] blocoyTile;
    public GameObject[] flag_black;
    public GameObject[] flag_white;

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
        GameObject instance = null;

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {

                GameObject toInstantiate = grassTiles[0];
                int aux = 7;

                int[] centroAgua1 = new int[] { 23, 8 };
                int raioAgua1 = 5;

                int[] centroAgua2 = new int[] { 9, 24 };
                int raioAgua2 = 5;

                int[] centroArvore1 = new int[] { 10, 13 };
                int[] centroArvore11 = new int[] { 15, 19 };
                int raioArvore1 = 6;
                int raioArvore11 = 10;

                int[] centroArvore2 = new int[] { 22, 19 };
                int[] centroArvore22 = new int[] { 17, 13 };
                int raioArvore2 = 6;
                int raioArvore22 = 10;
                

                int[] pos = new int[] { x, y };

                if ((x + y < aux) || ((rows - x - 1) + (rows - y - 1) < aux)) {
                    toInstantiate = concretoTiles[0];
                }
                else if (InsideCircle(centroAgua1, raioAgua1, pos) || (InsideCircle(centroAgua2, raioAgua2, pos)))
                {
                    toInstantiate = aguaTiles[0];
                }
                else if ((InsideCircle(centroArvore1, raioArvore1, pos) && !InsideCircle(centroArvore11, raioArvore11, pos)) || (InsideCircle(centroArvore2, raioArvore2, pos) && !InsideCircle(centroArvore22, raioArvore22, pos)))
                {
                    // instancia grass
                    instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                    // instancia moita
                    toInstantiate = moitaTiles[0];
                }

                // Bordas são prioridade
                if (x == 0 || x==rows - 1){
                    toInstantiate = blocoyTile[0];
                }
                if (y == 0 || y==rows - 1){
                    toInstantiate = blocoxTile[0];
                }
                if (x == 0 && y==0 || x == rows-1 && y==rows-1 || x == 0 && y==rows-1 || x == rows-1 && y==0){
                    toInstantiate = quinaTile[0];
                }

                instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity) as GameObject;
                instance.transform.SetParent(_boardHolder);
            }
            
        }

        instance = Instantiate(flag_black[0], new Vector3(2, 2, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(_boardHolder);
        instance = Instantiate(flag_white[0], new Vector3(29, 29, 0f), Quaternion.identity) as GameObject;
        instance.transform.SetParent(_boardHolder);
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