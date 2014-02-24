using UnityEngine;
using System.Collections;

public class CameraDeath : MonoBehaviour
{
    void CameraDeathNow()
    {
        Animator anim = gameObject.GetComponent<Animator>();
        anim.SetTrigger("Death");
    }
    
}
