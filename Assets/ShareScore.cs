using UnityEngine;
using System.Collections;

public class ShareScore : MonoBehaviour
{
    void OnMouseDown()
    {
        Facebook.uiFeedRequest("https://www.facebook.com/FlappityDuck", "http://krabdev.com/FlappityDuck/1024x1024.jpg", "Flappity Duck!!", "My Personal High Score!", "I have " + PlayerPrefs.GetInt("Score").ToString() + " points in Flappity duck! Can u beat me?");
    }
}
