using UnityEngine;
using System.Collections;
using Parse;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

public class SaveUserData : MonoBehaviour
{
  

   

    public static void SaveData(string username, int points)
    {

        var query = ParseObject.GetQuery("GameScore")
                .WhereEqualTo("Username", username);
        query.FindAsync().ContinueWith(t =>
        {
            IEnumerable<ParseObject> results = t.Result;
            if (results != null)
            {
                if (results.FirstOrDefault().Get<int>("Points") < points)
                {
                    ParseObject bla = new ParseObject("GameScore");
                    bla = results.FirstOrDefault();
                        
                    bla.SaveAsync().ContinueWith(
                            g => 
                    {
                        bla ["Points"] = points;
                        bla.SaveAsync();
                        PlayerPrefs.SetInt("Score", points);
                    });
                }
                    
            }
                
        });
    }
    
}
