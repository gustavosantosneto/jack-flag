using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CenarioCreator : MonoBehaviour
{
    private Transform _boardHolder;

    public static int columns = 32;
    public static int rows = 32;
    public static int tileScale = 1;

    public GameObject[] concretoTiles;
    public GameObject[] aguaTiles;

    public GameObject[] grassTiles;

    public Char[] playerTypes;
    public Text EnergyText;

    Char selectedPlayer;

    void Awake()
    {
        MakeFloor();
        EnergyText = GameObject.Find("EnergyText").GetComponent<Text>();
        CreatePlayers();

        selectedPlayer.setEnergy();
    }

    void CreatePlayers()
    {
        for (int i = 0, len = playerTypes.Length; i < len; i++)
        {
            var newPlayerinstanc = Instantiate(playerTypes[i], new Vector3((float)playerTypes[i].startX, (float)playerTypes[i].startY + 0.2f, 0), Quaternion.identity);
            if (selectedPlayer == null)
            {
                newPlayerinstanc.energy_text = EnergyText;
                selectedPlayer = newPlayerinstanc;
            }
        }
    }

    void MakeFloor()
    {
        _boardHolder = new GameObject("Board").transform;

        for (int x = 0; x < rows; x += tileScale)
        {
            for (int y = 0; y < columns; y += tileScale)
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

                GameObject instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;

                instance.transform.SetParent(_boardHolder);
            }
        }

    }

    bool InsideCircle(int[] center, int radius, int[] position)
    {
        int[] centro = new int[] { center[0], center[1] };
        int raio = radius;
        return Math.Round(Math.Sqrt(Math.Pow(centro[0] - position[0], 2) + Math.Pow(centro[1] - position[1], 2))) <= raio;
    }

    void Update()
    {
        ClickOperations();
    }

    void ClickOperations()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.allCameras[0] == null)
                return;

            var clickedGameObject = GetClickedGameObject();

            if (clickedGameObject == null)
                return;

            if (clickedGameObject.tag == "Floor")
            {
                _GotoClickedTile(clickedGameObject);
            }
            else if (clickedGameObject.tag == "Player")
            {
                selectedPlayer = playerTypes.First(w => w.GetComponent<GameObject>() == clickedGameObject);

                DrawSiblings(selectedPlayer.position);
            }
        }
    }

    private void DrawSiblings(Vector3Int centerPosition)
    {
        //var max = selectedPlayer.GetMaximumMoviment();
        var siblingVector3 = SiblingVector3(centerPosition);

        for (int i = 0, len = _boardHolder.childCount; i < len; i++)
        {
            var floor = _boardHolder.GetChild(i);

            //IGNORE UNWALCABLE OBJECTS
            if (floor.GetComponent<Collider>() == null)
                continue;

            var fPos = Vector3Int.RoundToInt(floor.transform.position);
            if (siblingVector3.Any(a => a == fPos))
            {
                ChangeFloorColor(floor.gameObject, new Color(0, 255, 0));
            }
            else
            {
                ChangeFloorColor(floor.gameObject, Color.white);
            }
        }
    }

    private void ChangeFloorColor(GameObject floor, Color color)
    {
        var spriteR = floor.GetComponent<SpriteRenderer>();
        spriteR.color = color;
    }

    private bool checkOuterLimits(Vector3Int refPosition)
    {
        return refPosition.x >= 0 && refPosition.y >= 0 && refPosition.x <= rows && refPosition.y <= columns;
    }

    private List<Vector3Int> SiblingVector3(Vector3Int refPosition, int factor = 1)
    {
        // Get X positions
        //[x][x][x]
        //[x][o][x]
        //[x][x][x]
        List<Vector3Int> siblings = new List<Vector3Int>();

        siblings.Add(new Vector3Int(refPosition.x + factor, refPosition.y, 0));
        siblings.Add(new Vector3Int(refPosition.x + factor, refPosition.y + factor, 0));
        siblings.Add(new Vector3Int(refPosition.x + factor, refPosition.y - factor, 0));
        siblings.Add(new Vector3Int(refPosition.x - factor, refPosition.y, 0));
        siblings.Add(new Vector3Int(refPosition.x - factor, refPosition.y + factor, 0));
        siblings.Add(new Vector3Int(refPosition.x - factor, refPosition.y - factor, 0));
        siblings.Add(new Vector3Int(refPosition.x, refPosition.y + factor, 0));
        siblings.Add(new Vector3Int(refPosition.x, refPosition.y - factor, 0));

        return siblings.Where(w => checkOuterLimits(w)).ToList();
    }

    GameObject GetClickedGameObject()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            return hit.transform.gameObject;
        }

        return null;
    }


    void _GotoClickedTile(GameObject tile)
    {
        var destinePosition = Vector3Int.RoundToInt(tile.transform.position);

        var moved = selectedPlayer.MoveTo(destinePosition);

        if (moved)
        {
            var floorPos = Vector3Int.RoundToInt(tile.transform.position);

            DrawSiblings(floorPos);
        }
    }
}