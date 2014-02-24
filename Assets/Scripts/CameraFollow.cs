using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    public GameObject player;

    void Start()
    {
        
    }
    
    void Update()
    {
        transform.position = new Vector3(player.transform.position.x, transform.position.y, -10);
    }
}
