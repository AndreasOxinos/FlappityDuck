using UnityEngine;
using System.Collections;

public class ShowScore : MonoBehaviour
{
    public Font font;

    void OnGUI()
    {
        GUIStyle mystyle = new GUIStyle();
        mystyle.font = font;
        mystyle.fontSize = 37;
        mystyle.normal.textColor = Color.black;
        GUI.Label(new Rect(Screen.width / 2 - 50, 225, 100, 30), PlayerPrefs.GetInt("Score").ToString(), mystyle);
    }
}
