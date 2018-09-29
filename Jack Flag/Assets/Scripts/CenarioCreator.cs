using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenarioCreator : MonoBehaviour
{

    public int columns = 32;
    public int rows = 32;
    public GameObject floorTile;
    private Transform boardHolder;

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
        boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                GameObject instance = Instantiate(floorTile, new Vector3(x, -0.1f, y), Quaternion.identity) as GameObject;

                instance.transform.SetParent(boardHolder);
            }
        }

    }


    void Update()
    {

    }
}