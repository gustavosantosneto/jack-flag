using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Char : MonoBehaviour
{

    public Vector3 destination;
    public double angleDirection;
    public Vector3 charFront;

    void Start()
    {
        _RigidBody = GetComponent<Rigidbody>();
        destination = transform.position;
        charFront = new Vector3(0f, 1f, 0f);
    }

    public int energy;
    public Text energy_text;
    public int height;
    public int orientation;
    public int startX;
    public int startY;
    public string type;
    public int width;
    public int weigth;
    public float z = -0.21f;

    public Vector3Int position { get { return Vector3Int.RoundToInt(gameObject.transform.position); } }

    private Rigidbody _RigidBody;

    public Char()
    {
        this.energy = 100;
        this.height = 1;
        this.orientation = 0;
        this.startX = 1;
        this.startY = 1;
        this.type = "Jumper";
        this.weigth = 1;
        this.width = 1;
    }

    public Char(Char c)
    {
        this.energy = c.energy;
        this.height = c.height;
        this.orientation = c.orientation;
        this.startX = c.startX;
        this.startY = c.startX;
        this.type = c.type;
        this.weigth = c.weigth;
        this.width = c.width;
    }

    public void SetEnergy(int energyValue = 100)
    {
        this.energy = energyValue;
        energy_text.text = energy.ToString();
    }

    public bool MoveTo(Vector3 destinePosition)
    {
        if (ValidateDestine(destinePosition) && energy > 0)
        {
           // destination eh necessario para o personagem caminhar
            destination = destinePosition;
            Vector3 vecDirection = destination - transform.position;
            float angle = Vector3.SignedAngle(charFront, vecDirection, new Vector3(0f,0f,1f));
            transform.Rotate(0, 0, angle);
            charFront = vecDirection;

            SetEnergy(energy - 1);
            return true;
        }
        else
            return false;
    }

    public bool Jumpe(Vector3Int destinePosition)
    {
        if (type != "Jumper")
            return false;

        _RigidBody.MovePosition(destinePosition);
        SetEnergy(energy - 5);
        return true;
    }

    public bool Push(Rigidbody Rigidbody, Vector3Int destinePosition)
    {
        if (type != "Pusher")
            return false;

        Rigidbody.MovePosition(destinePosition);
        SetEnergy(energy - 5);
        return true;
    }

    internal bool ValidateDestine(Vector3 destinePosition, int factor = 1)
    {
        //FACTOR = 1 CAN MOVE TO "O" TILES
        // [X] [X] [X] [X] [X]
        // [X] [O] [O] [O] [X]
        // [X] [O] [X] [O] [X]
        // [X] [O] [O] [O] [X]
        // [X] [X] [X] [X] [X]
        return true;
        //destinePosition != position &&
        //destinePosition.x <= position.x + factor &&
        //destinePosition.x >= position.x - factor &&
        //destinePosition.y <= position.y + factor &&
        //destinePosition.y >= position.y - factor;
    }

    internal object GetMaximumMoviment()
    {
        return MaxFibonacci(energy);
    }

    internal static int MaxFibonacci(int max)
    {
        int a = 1;
        int b = 1;
        int n = 0;

        while (b <= max)
        {
            int temp = a;
            a = b;
            b = temp + b;
            n++;
        }

        return n;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, destination, /*0.85f **/ Time.deltaTime);
    }
}
