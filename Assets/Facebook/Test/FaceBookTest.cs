using UnityEngine;
using LitJson;
using System.Collections;

[ExecuteInEditMode]
public class FaceBookTest : MonoBehaviour {
	
	public GUIStyle foto;
    public GameObject obj;
    public GUIText text1;
    public GUIText text2;
    public GUIText text3;
    public GUIText text4;
    public GUIText text5;
	public GUIText text6;

    public enum tipoGui { step1, step2, step3, step4 }
    public tipoGui tipo = tipoGui.step1;
    JsonData friendsinfo;
    float space = 8F;

    #region TEST
    // Use this for initialization
	void Start () {
        Facebook.onLogin += delegate(bool result, JsonData reply) 
		{
            if (result)
            {
                tipo = tipoGui.step3;
                //get url My Picture  (you can use 'me' or your Facebook id 
                //you can learn at https://developers.facebook.com/tools/explorer
                string ruta = Facebook.getInfo("/me/picture?type=normal");

                //Get My Picture
				StartCoroutine(getImage(ruta));
			}			
		};
		
        Facebook.onGraphRequest += delegate(bool result, JsonData reply) 
		{
            if (result)
            {
                text1.text = reply["name"].ToString();
                text2.text = reply["username"].ToString();
                text3.text = reply["id"].ToString();
                text4.text = reply["religion"].ToString();
            }
		};
		
		Facebook.onGetLoginStatusdos += delegate(bool result, string status) 
		{            
            if (result)
            {
                text6.text = "Status+: " + status;
                //text6.text = "Status+: " + reply[0].ToString() + "\n" + reply[1]["accessToken"].ToString() + "\n" + reply[1]["expiresIn"].ToString() + "\n" + reply[1]["signedRequest"].ToString() + "\n" + reply[1]["userID"].ToString();
            }
            else
            {				
                text6.text = "Status-: ";
            }
		}; 
	}

    public IEnumerator getImage(string _imageURL)
    {
        var www = new WWW(_imageURL);
        yield return www;        
        foto.normal.background = www.texture;
    }


    public IEnumerator getInfoFriends(string texto)
    {        
        var www = new WWW(texto);
        yield return www;
        friendsinfo = JsonMapper.ToObject(www.text);
        buildfriends();
        
    }

    void buildfriends()
    {
        int pos = -14;
        int total = friendsinfo[0].Count;        
        text4.text = total + " friends found";
        text5.text = "Keyup / Key down to move camera";
        int i = 0;
        bool column = true;

        for (i = 0; i <= total; i++)
        {
            if (i > total / 2 & column) { pos = -3; space = 8F; column = false; }
            GameObject tr = (GameObject)Instantiate(obj, new Vector3(pos, space, 0), obj.transform.rotation);
            tr.transform.parent = transform;
            tr.transform.name = friendsinfo[0][i]["id"].ToString();

            tr.GetComponent<MiObjeto>().txtnombre = friendsinfo[0][i]["name"].ToString();
            tr.GetComponent<MiObjeto>().txtimagen = friendsinfo[0][i]["picture"][0][0].ToString();
            try
            {
                if (friendsinfo[0][i]["gender"].ToString() == "male")
                {
                    tr.GetComponent<MiObjeto>().txtgenero = "m";
                }
                else
                {
                    tr.GetComponent<MiObjeto>().txtgenero = "f";
                }
            }
            catch { }
            space -= 2F;            
        }        
    }

    void OnGUI()
	{
        switch (tipo)
        {
            case tipoGui.step1:                

                if (GUI.Button(new Rect(10, 10, Screen.width - 20, Screen.height - 20), "Enter"))
                {                    
                    Facebook.login("email,publish_actions,publish_stream");
                    tipo = tipoGui.step2;
                }                
                break;

            case tipoGui.step3:

                GUI.Label(new Rect(Screen.width * 0.82F, Screen.height * 0.12F, 125, 125), "", foto);

                if (GUI.Button(new Rect(Screen.width - 190, Screen.height / 2 - 100, 170, 30), "User Information"))
                {
                    Facebook.graphRequest("/me");
                }
			
			
				if (GUI.Button(new Rect(Screen.width - 190, Screen.height / 2 - 60, 170, 30), "Get Status"))
                {                    
                    Facebook.getLoginStatusdos();                    
                }
			

                if (GUI.Button(new Rect(770, 385, 170, 30), "Send invitations"))                
                {
                    Facebook.uiAppRequest("Title Request", "I invite you to try this application");
                }

                if (GUI.Button(new Rect(770, 385 + 35, 170, 30), "Post with Pop Up"))
                {
                    Facebook.uiFeedRequest("http://www.google.com", "http://www.google.com/logos/2012/tsiolkovsky12-hp.jpg", "Resaltado - ..........", "Facebook desde la Web", "Descripcion..... ... ..... .... ... ....... .");
                }                

                if (GUI.Button(new Rect(770, 385 + 70, 170, 30), "Post without Pop Up"))
                {	//string to, string message, string name, string description, string picture, string caption, string link			
					Facebook.postear("me", "Post Message", "Name...", "Des", "http://www.google.com/logos/2012/tsiolkovsky12-hp.jpg", "Facebook Web Cap", "http://www.google.com");					
                }
			
				if (GUI.Button(new Rect(770, 385 + 105, 170, 30), "Post Video"))
                {				
					Facebook.postearv("me", "Post Message", "Name...", "Des", "http://img.youtube.com/vi/9bZkp7q19f0/0.jpg", "Facebook Web Cap", "http://www.youtube.com/e/9bZkp7q19f0", "http://www.youtube.com/watch?v=9bZkp7q19f0");					
                }

                if (GUI.Button(new Rect(770, 385 + 140, 170, 30), "Friends"))
                {
                    StartCoroutine(getInfoFriends(Facebook.getInfo("/me/friends?fields=id,name,gender,picture")));
                }

                if (GUI.Button(new Rect(770, 384 + 175, 170, 30), "Post Photo"))
                {
                    Facebook.photo("Message...", "http://a.lyecorp.com/marcador.jpg");
                }                break;
        }		
	}
	#endregion
}
