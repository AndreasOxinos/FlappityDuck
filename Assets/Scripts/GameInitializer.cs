using UnityEngine;
using System.Collections;
using LitJson;
using Parse;
using System.Threading.Tasks;

public class GameInitializer : MonoBehaviour
{
    bool loaded = false;
    public enum tipoGui
    {
        step1,
        step2,
        step3,
        step4
    }
    public tipoGui tipo = tipoGui.step1;
    public string FBName;

    void Start()
    {

        DontDestroyOnLoad(this);

       

        Facebook.onLogin += delegate(bool result, JsonData reply)
        {
            if (result)
            {
                Facebook.graphRequest("/me");
                tipo = tipoGui.step3;
                //get url My Picture  (you can use 'me' or your Facebook id 
                //you can learn at https://developers.facebook.com/tools/explorer
                string ruta = Facebook.getInfo("/me/picture?type=normal");


                
            }           
        };
        
        Facebook.onGraphRequest += delegate(bool result, JsonData reply)
        {
            if (result)
            {
                FBName = reply ["name"].ToString();
                if (!string.IsNullOrEmpty(FBName))
                {
                    PlayerPrefs.SetString("Username", FBName);

                }
            }
        };
        
        StartCoroutine("MakeLoginProccess");

       
    }

    IEnumerator MakeLoginProccess()
    {
        yield return new WaitForSeconds(1f);
        Debug.Log("OMG IM MAKING IT THROUGH");
        Facebook.login("email,publish_actions,publish_stream");
        Facebook.graphRequest("/me");
        tipo = tipoGui.step2;
        yield return new WaitForSeconds(1f);
    }

    void Update()
    {
        if(tipo == tipoGui.step3)
        {
            if(!loaded)
            {
                loaded = true;
                LoadScene();
            }
        }
    }

    void LoadScene()
    {
       
        Application.LoadLevel("Start");
    }

   
}
