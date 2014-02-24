using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiObjeto : MonoBehaviour {

    public string txtimagen;
    public string txtnombre;
    public string txtgenero;

    public GameObject objfoto;
    public TextMesh objnombre;
    public GameObject objgenero;      

    public List<Texture2D> genero;

	// Use this for initialization
	void Start () {

        StartCoroutine(getImage(txtimagen));

        objnombre.text = txtnombre;
        
        objgenero.renderer.material.mainTexture = genero[2];
        if (txtgenero == "m") { objgenero.renderer.material.mainTexture = genero[0]; }
        if (txtgenero == "f") { objgenero.renderer.material.mainTexture = genero[1]; }
	}


    public IEnumerator getImage(string imageURL)
    {
        
        var www = new WWW(imageURL);
        yield return www;        
        Texture2D foto = www.texture;
        objfoto.renderer.material.mainTexture = foto;
    }

}
