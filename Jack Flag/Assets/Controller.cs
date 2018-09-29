using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Controller : MonoBehaviour
{
    private Rigidbody rb;
    private const int moveStepLength = 1;
    private Vector3 moveStepX = new Vector3(moveStepLength, 0, 0);
    private Vector3 moveStepZ = new Vector3(0, 0, moveStepLength);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
            GotoClickedTile(rb);

        //if (Input.GetKeyDown(KeyCode.Space))
        //{
        //    rigidbody.velocity += jumpSpeed * Vector3.up;
        //}
    }

    private void GotoClickedTile(Rigidbody rb)
    {
        if (Camera.allCameras[0] == null)
            return;

        Ray ray = Camera.allCameras[0].ScreenPointToRay(Input.mousePosition);

        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100))
        {
            var currentGameObject = hit.transform.gameObject;

            var destinePosition = currentGameObject.transform.position;

            rb.MovePosition(destinePosition);
        }
    }
}
