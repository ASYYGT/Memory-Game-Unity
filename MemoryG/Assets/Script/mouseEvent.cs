using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseEvent : MonoBehaviour
{
    private control cnt;

    private void Start()
    {
        cnt = GameObject.Find("Main Camera").GetComponent<control>();
    }
    void OnMouseEnter()
    {
        //mose equator oyun  nesnesinin collider�na temas etti�inde conrtol s�n�f�ndan countCntrl fonksiyonunu �a��r�yoruz...
        Debug.Log("nesne s�f�rland�...");
        cnt.countCntrl(0);
    }

}
