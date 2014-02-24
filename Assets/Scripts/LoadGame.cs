using UnityEngine;
using System.Collections;

public class LoadGame : MonoBehaviour 
{
    void OnMouseDown() 
    {
        Application.LoadLevel("Game");
    }   

    void Update()
    {
        if(Input.GetButtonDown("Jump"))
        {
            Application.LoadLevel("Game");
        }
    }
}
