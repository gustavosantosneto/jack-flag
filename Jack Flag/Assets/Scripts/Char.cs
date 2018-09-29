using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char : MonoBehaviour
{
    public float startX = 1;
    public float startY = 1;

    private Rigidbody _rb;
    private bool _isSelected = false;

    private const int _moveStepLength = 1;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (Camera.allCameras[0] == null)
                return;

            Ray ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                var clickedGameObject = hit.transform.gameObject;

                if (clickedGameObject.tag == "Floor")
                    _GotoClickedTile(clickedGameObject);
                else if (clickedGameObject.tag == "Player")
                {

                    _isSelected = true;
                }
            }

        }

    }

    private void _GotoClickedTile(GameObject tile)
    {
        var destinePosition = tile.transform.position;

        _rb.MovePosition(destinePosition);
    }
}
