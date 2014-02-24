using UnityEngine;
using System.Collections;
using LitJson;
using Parse;
using System.Threading.Tasks;

public class Initializer : MonoBehaviour
{

    public Font font;


    void CreateUser()
    {
        var query = ParseObject.GetQuery("GameScore")
            .WhereEqualTo("Username", PlayerPrefs.GetString("Username"));
        query.FirstOrDefaultAsync().ContinueWith(t =>
                                                 {
            ParseObject result = t.Result;
            if(result == null)
            {
                ParseObject gameScore = new ParseObject("GameScore");
                gameScore ["Username"] = PlayerPrefs.GetString("Username");
                gameScore ["Points"] = 0;
                Task saveTask = gameScore.SaveAsync();
            }
            else{

                PlayerPrefs.SetInt("Score", result.Get<int>("Points"));
            }
        });
    }

    void Start()
    {
        //AudioController.PlayMusic("Menu");
        CreateUser();
    }

    void Awake()
    {
        Time.timeScale = 1;
    }


    void OnGUI()
    {
        GUIStyle myStyle = new GUIStyle();
        myStyle.font = font;
        myStyle.normal.textColor = Color.black;
        myStyle.fontSize = 30;
     //   GUI.Label(new Rect(Screen.width - 230, Screen.height - 120, 160, 50), "High Score: " + PlayerPrefs.GetInt("Score").ToString(), myStyle); 
    }
}
