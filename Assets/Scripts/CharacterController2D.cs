using UnityEngine;
using System.Collections;
using Parse;

public class CharacterController2D : MonoBehaviour
{
    public int Points = 0;
    public Texture2D overlay;
    public Texture2D okButton;
    public Texture2D gameOverTexture;
    public Font font;
    public Texture2D playButton;
    public Texture2D pauseButton;
    public Texture2D pausePlayButton;
    public Texture2D scoreTexture;
    public Texture2D shareTexture;

    public enum GameState
    {
        Playing,
        Pause,
        Dead
    }

    public GameState gameState = GameState.Pause;
    public float gravityPower = 20.0f;
    public float power = 50.0f;
    public float speed = 5.0f;
    public Vector3 _newPosition;
    public bool moving = false;
    private Animator anim;
    public Vector3 startPosition;
    public float liftSpeed = 5.0f;
    public float weight = 0f;
    public float _tempGravityPower = 0.1f;
    public float jumpHeight = 0.5f;
    private float _gravityPower = 0.1f;
    public Texture2D muteTexture;
    public Texture2D unmuteTexture;
    private bool playSound = false;

    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }
    
    void Update()
    {


        if (gameState == GameState.Pause)
        {
            Time.timeScale = 0;
            if (Input.GetButtonDown("Jump"))
            {
                gameState = GameState.Playing;
            }

        } else if (gameState == GameState.Dead)
        {
            Time.timeScale = 0;
            if (Input.GetButtonDown("Jump"))
            {
                Application.LoadLevel("Start");
            }
        } else if (gameState == GameState.Playing)
        {
            transform.position += Vector3.right * speed * Time.deltaTime;



//            if (!moving)
//            {
            Time.timeScale = 1;
        
               
            if (Input.GetButtonDown("Jump"))
            {
                StopCoroutine("MoveUp");
                if (playSound)
                {
                    AudioController.StopMusic();
                    AudioController.PlayMusic("Jump");
                }
                _gravityPower = 0.1f;
                anim.SetTrigger("Jump");
                _tempGravityPower = 0.1f;
                startPosition = transform.position; //Set the start
                _newPosition = startPosition + new Vector3(0, jumpHeight, 0);
                StartCoroutine("MoveUp");
                moving = true; //set flag     
            } else
            {
                Gravity();
            }
//            }

           
            anim.SetBool("Jumping", false);
            if (moving)
            {
                //     if (Vector3.Distance(transform.position, _newPosition) <= 0.1f)
                //         moving = false;

                anim.SetBool("Jumping", true);
                // weight += Time.deltaTime * liftSpeed; //amount
                // transform.position = Vector3.Lerp(startPosition, _newPosition, weight);
                //     StartCoroutine("MoveUp");
            }
        }
          
    }

    IEnumerator MoveUp()
    {

        float duration = 0.35f;
//        for (float i = 0.0f; i < duration; i+=Time.deltaTime)
//        {   
//            startPosition += Vector3.right * speed * Time.deltaTime;
//            _newPosition += Vector3.right * speed * Time.deltaTime;
//            transform.position = Vector3.Lerp(startPosition, _newPosition, Mathf.SmoothStep(0.0f, 1.0f, i));
//            //transform.position = Vector3.Lerp(startPosition, _newPosition, i / duration);
//            _tempGravityPower = 0;
//            yield return new WaitForSeconds(0.0001f); 
//            }


        var t = 0.0f;
        
        while (t <= 1.0f)
        {
            startPosition += Vector3.right * speed * Time.deltaTime;
            _newPosition += Vector3.right * speed * Time.deltaTime;
            t += Time.deltaTime / duration;
            
            transform.position = Vector3.Lerp(startPosition, _newPosition, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, t)));
            
            yield return new WaitForEndOfFrame();

        }
       

        moving = false;
        //yield return null;

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (playSound)
        {
            AudioController.StopMusic();
            AudioController.PlayMusic("la");
        }
        Points ++;
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "GameOver")
        {
            SaveUserData.SaveData(PlayerPrefs.GetString("Username"), Points);

            int maxPoints = PlayerPrefs.GetInt("Score");
            if (maxPoints > Points)
            {
                // Facebook.uiFeedRequest("https://www.facebook.com/FlappityDuck", "http://www.google.com/logos/2012/tsiolkovsky12-hp.jpg", "Flappity Duck!", "Flappity Duck Proud player!!!", "Completed one more run with " + Points.ToString() + " points!");
            
            } else
            {
                PlayerPrefs.SetInt("Score", Points);
                //    Facebook.postear("me", "Post Message", "Name...", "Des", "http://www.google.com/logos/2012/tsiolkovsky12-hp.jpg", "Facebook Web Cap", "http://www.google.com");                 
                Facebook.postear("me", "New Personal High Score!", "Flappity Duck!", "I just made a new score on Flappity Duck, can you beat me?", "http://krabdev.com/FlappityDuck/1024x1024.jpg", "Just made a new high score", "https://www.facebook.com/FlappityDuck");                 

            }
            if (playSound)
            {
                AudioController.PlayMusic("GameOver");
            }
            gameState = GameState.Dead;
        }
    }

    void OnGUI()
    {
       
        if (gameState == GameState.Pause)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlay); 
            if (GUI.Button(new Rect(Screen.width / 2 - 197, Screen.height / 2 - 55, 395, 114), pausePlayButton, GUIStyle.none))
            {
                gameState = GameState.Playing;
            }
        }

        if (gameState == GameState.Dead)
        {
            GUIStyle mystyle = new GUIStyle();
            mystyle.font = font;
            mystyle.fontSize = 32;
            mystyle.normal.textColor = Color.white;
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), overlay); 
            GUI.DrawTexture(new Rect(Screen.width / 2 - 128, Screen.height - 250, 256, 64), gameOverTexture);
            GUI.DrawTexture(new Rect(Screen.width / 2 - 157, 130, 314, 214), scoreTexture);
            GUI.Label(new Rect(Screen.width / 2 + 50, 170, 80, 30), Points.ToString(), mystyle);
            GUI.Label(new Rect(Screen.width / 2 + 50, 270, 80, 30), PlayerPrefs.GetInt("Score").ToString(), mystyle);

            if (GUI.Button(new Rect(Screen.width / 2 - 40, Screen.height - 160, 79, 59), okButton, GUIStyle.none))
            {
                Time.timeScale = 1;
                Application.LoadLevel("Start");
            }
            if (GUI.Button(new Rect(Screen.width / 2 - 90, Screen.height - 75, 180, 61), shareTexture, GUIStyle.none))
            {
                Facebook.uiFeedRequest("https://www.facebook.com/FlappityDuck", "http://krabdev.com/FlappityDuck/1024x1024.jpg", "Flappity Duck!!", "My Personal High Score!", "I have " + PlayerPrefs.GetInt("Score").ToString() + " points in Flappity duck! Can u beat me?");
            }
        }

        if (gameState == GameState.Playing)
        {
            GUIStyle mystyle = new GUIStyle();
            mystyle.font = font;
            mystyle.fontSize = 32;
            mystyle.normal.textColor = Color.black;
            GUI.Label(new Rect(Screen.width - 50, 40, 100, 40), Points.ToString(), mystyle);

            if (GUI.Button(new Rect(10, 10, 50, 50), pauseButton, GUIStyle.none))
            {
                gameState = GameState.Pause;
            }
        }
       

        if(playSound)
        {
            if(GUI.Button(new Rect(Screen.width - 100, Screen.height - 100, 76, 60), muteTexture, GUIStyle.none))
            {
                playSound = false;
            }
        }
        else
        {
            if(GUI.Button(new Rect(Screen.width - 100, Screen.height - 100, 76, 60), unmuteTexture, GUIStyle.none))
            {
                playSound = true;
            }
        }
    }

    void Gravity()
    {

        if (_tempGravityPower < gravityPower)
        {
            _tempGravityPower += _gravityPower;
            _gravityPower += 0.0005f;
        }
        if (_tempGravityPower > (gravityPower / 2))
            anim.SetBool("Diving", true);
        else
            anim.SetBool("Diving", false);

        transform.position += -Vector3.up * _tempGravityPower * Time.deltaTime;
    }
}
