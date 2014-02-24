using UnityEngine;
using System.Collections;

public class ShareFB : MonoBehaviour {

    void OnMouseDown() 
    {
        Facebook.uiAppRequest("Flappity Duck!!", "I invite you to try this application");
    } 
}
