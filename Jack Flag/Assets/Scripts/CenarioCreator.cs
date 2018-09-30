using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class CenarioCreator : MonoBehaviour
{
    private Transform _boardHolder;

    public int columns = 32;
    public int rows = 32;
    public static int tileScale = 1;
    public int turn = 1;

    public GameObject[] concretoTiles;
    public GameObject[] aguaTiles;
    public GameObject[] grassTiles;
    public GameObject[] grass2Tiles;
    public GameObject[] moitaTiles;
    public GameObject[] quinaTile;
    public GameObject[] blocoxTile;
    public GameObject[] blocoyTile;
    public GameObject[] flag_black;
    public GameObject[] flag_white;
    public GameObject[] rochaTiles;
    public GameObject[] team1Base;
    public GameObject[] team2Base;
    public GameObject[] maca;

    public Char[] playerTypes;
    public Text EnergyText;
    public Text TurnText;

    private Char player1;
    private Vector3 player1LastMove;
    private GameObject flagBlack;
    private bool flagBlackClicked = false;
    private GameObject flagWhite;
    private bool flagWhiteClicked = false;

    void Awake()
    {
        MakeFloor();
        EnergyText = GameObject.Find("EnergyText").GetComponent<Text>();
        TurnText = GameObject.Find("TurnText").GetComponent<Text>();
        CreatePlayers();
    }

    public void SetTurn(int turnValue = 100)
    {
        turn = turnValue;
        TurnText.text = turn.ToString();
    }

    void CreatePlayers()
    {
        for (int i = 0, len = playerTypes.Length; i < len; i++)
        {
            var newPlayerinstanc = Instantiate(playerTypes[i], new Vector3((float)playerTypes[i].startX, (float)playerTypes[i].startY + 0.2f, playerTypes[i].z), Quaternion.identity);
            if (player1 == null)
            {
                newPlayerinstanc.energy_text = EnergyText;
                player1 = newPlayerinstanc;
            }
        }
    }

    void MakeFloor()
    {
        _boardHolder = new GameObject("Board").transform;
        GameObject instance = null;
        GameObject toInstantiate = null;

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

        for (int x = 0; x < rows; x++)
        {
            for (int y = 0; y < columns; y++)
            {
                int[] pos = new int[] { x, y };

                if ((x + y < aux) || ((rows - x - 1) + (rows - y - 1) < aux))
                {
                    toInstantiate = concretoTiles[0];

                    instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(_boardHolder);
                    instance.transform.SetParent(_boardHolder);
                }
                else if (InsideCircle(centroAgua1, raioAgua1, pos) || (InsideCircle(centroAgua2, raioAgua2, pos)))
                {
                    toInstantiate = aguaTiles[0];

                    instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(_boardHolder);
                }
                else if ((InsideCircle(centroArvore1, raioArvore1, pos) && !InsideCircle(centroArvore11, raioArvore11, pos)))
                {
                    // instancia grass
                    toInstantiate = grass2Tiles[0];
                    instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(_boardHolder);
                    // instancia moita
                    toInstantiate = moitaTiles[0];
                    instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(_boardHolder);
                }
                else if (InsideCircle(centroArvore2, raioArvore2, pos) && !InsideCircle(centroArvore22, raioArvore22, pos))
                {
                    toInstantiate = grass2Tiles[0];

                    instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(_boardHolder);

                    toInstantiate = rochaTiles[0];

                    instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(_boardHolder);
                }
                else
                {
                    toInstantiate = grassTiles[0];

                    instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;
                    instance.transform.SetParent(_boardHolder);

                    float chance_maca = UnityEngine.Random.value;
                    if (chance_maca < 0.01)
                    {
                        instance = Instantiate(maca[0], new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;
                        instance.transform.SetParent(_boardHolder);
                    }
                }

                // Bordas são prioridade
                if (x == 0 || x == rows - 1)
                {
                    toInstantiate = blocoyTile[0];
                }
                if (y == 0 || y == rows - 1)
                {
                    toInstantiate = blocoxTile[0];
                }
                if (x == 0 && y == 0 || x == rows - 1 && y == rows - 1 || x == 0 && y == rows - 1 || x == rows - 1 && y == 0)
                {
                    toInstantiate = quinaTile[0];
                }
                instance = Instantiate(toInstantiate, new Vector3Int(x, y, 0), Quaternion.identity) as GameObject;

                instance.transform.SetParent(_boardHolder);
            }

        }

        instance = Instantiate(team1Base[0], new Vector3Int(2, 2, 0), Quaternion.identity) as GameObject;
        instance.transform.SetParent(_boardHolder);

        flagBlack = Instantiate(flag_black[0], new Vector3Int(2, 2, 0), Quaternion.identity) as GameObject;
        flagBlack.transform.SetParent(_boardHolder);

        instance = Instantiate(team2Base[0], new Vector3Int(29, 29, 0), Quaternion.identity) as GameObject;
        instance.transform.SetParent(_boardHolder);

        instance = Instantiate(flag_white[0], new Vector3Int(29, 29, 0), Quaternion.identity) as GameObject;
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

            //Flag click
            if (clickedGameObject.transform.position.x == 2 && clickedGameObject.transform.position.y == 2)
            {
                flagBlackClicked = true;
            }
            else if (clickedGameObject.transform.position.x == 29 && clickedGameObject.transform.position.y == 29)
            {
                flagWhiteClicked = true;
            }
            else if (clickedGameObject.tag == "Floor")
            {
                _GotoClickedTile(clickedGameObject);
                SetTurn(turn + 1);
            }
            else if (clickedGameObject.tag == "Player")
            {
                player1 = playerTypes.First(w => w.GetComponent<GameObject>() == clickedGameObject);

                DrawSiblings(player1.position);
                SetTurn(turn + 1);
            }
            else if (clickedGameObject.tag == "Obstacle")
            {
                SetTurn(turn + 1);
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
            if (floor.GetComponent<Collider>() == null || floor.tag != "Floor")
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
        var destinePosition = new Vector3(tile.transform.position.x, tile.transform.position.y, player1.z);

        player1LastMove = player1.position;

        var moved = player1.MoveTo(destinePosition);

        if (moved)
        {
            //Flag follow char, if clicked
            if (flagBlackClicked)
            {
                flagBlack.GetComponent<Rigidbody>().MovePosition(player1LastMove);
            }

            var floorPos = Vector3Int.RoundToInt(tile.transform.position);

            DrawSiblings(floorPos);
        }
    }
}