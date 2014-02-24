using UnityEngine;
using System.Collections;

public class MoveCamera : MonoBehaviour {

    
    Vector3 abajo = new Vector3(0, -0.25F, 0);
    Vector3 arriba = new Vector3(0, 0.25F, 0);

    float maximo = -0.8817074F;
    float salto = 1.1F;

    int primera = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (Input.GetKey(KeyCode.UpArrow))
        {
            camarriba();
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            camabajo();
        }            
	}

    public void camarriba()
    {
        transform.position += arriba;        
    }

    public void camabajo()
    {
        transform.position += abajo;      
    }
}
