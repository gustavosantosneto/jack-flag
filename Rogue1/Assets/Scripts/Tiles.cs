using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tiles : MonoBehaviour {

    // Atributos dos Tiles
    public bool caminhavel = true;
    public bool atual = false;
    public bool alvo = false;
    public bool selecionavel = false;

    public List<Tiles> adjacencias = new List<Tiles>();

    public bool visitado = false;
    public Tiles pai = null;
    public int distancia = 0;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
