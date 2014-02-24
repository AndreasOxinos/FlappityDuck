using UnityEngine;
using System.Collections;

public class SelfDestroy : MonoBehaviour {

    public float destructionTime = 9f;

	void Start () 
    {
        Invoke("Destroy", destructionTime);
	}

    void Destroy()
    {
        Destroy(gameObject);
    }
	
	
}
