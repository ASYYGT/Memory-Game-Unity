using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class control : MonoBehaviour
{
    private GameObject objClick1, objClick2;
    public int count = 0;// s�ra ile t�klanan nesneleri alabilmek i�in bir de�i�ken tan�mlad�k....
    public int healt = 5;
    public int foundobj;
    public GameObject equator;
    public float time ;
    private bool lossScrean = false;
    private MeshCollider col1, col2;
    private int angle = 0;



    void Update()
    {
        time = time - Time.deltaTime;
        
        if (Input.GetMouseButtonDown(0)&& time < 0)
        {

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.name == "equator")
                {
                    // e�er left click ile se�ilen objenin ismi equator ise hatali se�im yapt���n� belirtiyoruz...
                    Debug.Log("Yanl�� nesne se�ildi...");
                }
                else if( count == 0)
                {
                    /**
                     * ilk se�ilen oyun nesnesini objClick de�i�kenine at�yoruz. Sonra atac���m oyun nesnesinin colllider compannentini
                     * ka�at�yoruzki oyuncu ayn� nesneyi pe� pe�e iki defa se�emesin...
                     * */
                    objClick1 = hit.transform.gameObject;
                    col1 = objClick1.GetComponent<MeshCollider>();
                    col1.enabled = !col1.enabled;   
                    count++;
                }

                else if (count == 1)
                {
                    objClick2 = hit.transform.gameObject;
                    col2 = objClick2.GetComponent<MeshCollider>();
                    col2.enabled = !col2.enabled;
                    query();
                    
                }
            }
        }
    }
    public void countCntrl(int newCount)
    {
        //equator objesinin �st�nden ge�ti�inde bu fonksiyorunu �a��r�yoruz
        /**
         * e�er mouse equator objesinin �st�nden ge�erse count de�i�kenini 0 yaparak ilk nesneyi se�ilmemi� say�yoruz ve se�ilen
         * ilk nesneinn collider �n� tekrar True hale getiriyoruz...
         * 
         */
        col1.enabled = true;
        count = newCount;
        Debug.Log(count);
    }

    public void query()
    {
        if(objClick1.name == objClick2.name)
        {
            /**e�er se�ilen 2 nesnenin de isimleri ayn� ise e�le�meyi ba�ar�l� olarak say�yoruz. se�imin ba�ar�l� olmas�
             * sonucunda se�ilen objeleri tekrardan d�z hale gtiriyoruz...
             */
            Debug.Log("E�le�me ba�ar�l�");
            objClick1.transform.rotation = Quaternion.Euler(0, 0, 0);
            objClick2.transform.rotation = Quaternion.Euler(0, 0, 0);
            foundobj--;
            objClick1 = null;
            objClick2 = null;

        }

        else
        {
            //e�er se�ilen objeler uyu�maz ise bu objelerin colliderlar�n� tekrardan aktif ediyoruzz ve oyuncunun can de�erini azalt�yoruz...
            Debug.Log("E�le�me ba�ar�s�z");
            col1.enabled = true;
            col2.enabled = true;
            healt--;
        }
        if(healt <= 0)
        {
            lossScrean = true;
        }
        count = 0;
    }

    private void OnGUI()
    {
        GUI.Box(new Rect(580, 20, 200, 25), "Can: " + healt);
        

        if(GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height - 50, 50, 50), "Sa�"))
        {
            //butona bas�ld���nda equator objesinin z eksenindeki a��s�n� 10 derece artt�racak...
            angle = angle + 10;
            equator.transform.rotation = Quaternion.Euler(90, 0, angle);
        }
        if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height - 50, 50, 50), "Sol"))
        {
            angle = angle - 10;
            equator.transform.rotation = Quaternion.Euler(90, 0, angle);
        }
        
        if (time >= 0)
        {
            GUI.Box(new Rect(50, 20, 200, 25), "" + (int)time);
        }
        
        if (foundobj == 0)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
            Screen.height / 2 - 50, 200, 100), "Kazand�n"))
            {
                RestartGame();
            }
        }

        if(lossScrean == true)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
            Screen.height / 2 - 50, 200, 100), "KAYBET�N"))
            {
                RestartGame();
            }
        }


    }
     public void RestartGame()// sahneyi tekrar y�kleyerek oyunu yeniden ba�lat�r...
    {

        SceneManager.LoadScene(0);
    }

}
