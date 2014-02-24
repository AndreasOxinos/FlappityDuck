using UnityEngine;
using System.Collections;

public class MoveController : MonoBehaviour
{
    public float speed = 1.0f;
    public float gravity = 10f;
    public float power = 10f;
    public float jumpTime = 1.0f;
    public State gameState = State.Pause;
    private float actualGravity = 1f;
    private bool resetGravity = true;
    private bool deadCat = false;
    public Font font;
    public int SCORE = 0;
    public Texture2D overLay;
    public Texture2D gameOverTexture;
    public Texture2D okButtonTexture;
    
    public enum State
    {
        Playing, 
        Pause,
        Dead
    }
    
    void Update()
    {
        
        if (!deadCat)
        {
            
            if (gameState == State.Pause)
            {
                Time.timeScale = 0;
            } else
            {
                Time.timeScale = 1;
                transform.position += Vector3.right * speed * Time.deltaTime;
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    transform.position += Vector3.up * power;
                    actualGravity = 1f;
                } else
                {
                    AddGravity();
                }
            }
        } else
        {
            Time.timeScale = 0;
        }
        
    }
    
    void AddGravity()
    {
        if (resetGravity)
        {
            if (actualGravity <= gravity)
                actualGravity += 0.2f;
            
            transform.position += -Vector3.up * actualGravity * Time.deltaTime;
        }
        
    }
    
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "SCORE")
        {
            SCORE++;
        }
    }
    
    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Ground")
        {
            Debug.Log("Death"); 
            DeadCat();
        }
        if (coll.gameObject.tag == "Pole")
        {
            Debug.Log("Death");
            DeadCat();
        }
    }
    
    void OnGUI()
    {
        
//        if (gameState == State.Pause)
//        {
//            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overLay);
//            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height - 50, 100, 50), okButtonTexture, GUIStyle.none))
//            {
//                gameState = State.Playing;
//            }
//        }
//        
//        GUIStyle mySkin = new GUIStyle();
//        mySkin.fontSize = 24;
//        mySkin.fontStyle = FontStyle.Bold;
//        mySkin.font = font;
//        mySkin.normal.textColor = Color.white;
//        
//        
//        GUI.Label(new Rect(Screen.width - 50, 20, 50, 20), SCORE.ToString(), mySkin);  
//        if (deadCat)
//        {
//            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overLay);
//            GUI.DrawTexture(new Rect(Screen.width / 2 - 125, Screen.height / 2 - 50, 250, 50), gameOverTexture);
//            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 50), okButtonTexture, GUIStyle.none))
//            {
//                Application.LoadLevel("Start");
//            }
//        }
    }
    
    void DeadCat()
    {
        deadCat = true;
        if (PlayerPrefs.HasKey("HighScore"))
        {
            int previusHighScore = PlayerPrefs.GetInt("HighScore");
            if (previusHighScore <= SCORE)
            {
                PlayerPrefs.SetInt("HighScore", SCORE);
            }
        } else
        {
            PlayerPrefs.SetInt("HighScore", SCORE);
        }
        
    }
}
