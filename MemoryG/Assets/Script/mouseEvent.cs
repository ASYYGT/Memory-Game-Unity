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
        //mose equator oyun  nesnesinin colliderýna temas ettiðinde conrtol sýnýfýndan countCntrl fonksiyonunu çaðýrýyoruz...
        Debug.Log("nesne sýfýrlandý...");
        cnt.countCntrl(0);
    }

}
