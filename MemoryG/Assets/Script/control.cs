using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class control : MonoBehaviour
{
    private GameObject objClick1, objClick2;
    public int count = 0;// sýra ile týklanan nesneleri alabilmek için bir deðiþken tanýmladýk....
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
                    // eðer left click ile seçilen objenin ismi equator ise hatali seçim yaptýðýný belirtiyoruz...
                    Debug.Log("Yanlýþ nesne seçildi...");
                }
                else if( count == 0)
                {
                    /**
                     * ilk seçilen oyun nesnesini objClick deðiþkenine atýyoruz. Sonra atacýðým oyun nesnesinin colllider compannentini
                     * kaðatýyoruzki oyuncu ayný nesneyi peþ peþe iki defa seçemesin...
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
        //equator objesinin üstünden geçtiðinde bu fonksiyorunu çaðýrýyoruz
        /**
         * eðer mouse equator objesinin üstünden geçerse count deðiþkenini 0 yaparak ilk nesneyi seçilmemiþ sayýyoruz ve seçilen
         * ilk nesneinn collider ýný tekrar True hale getiriyoruz...
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
            /**eðer seçilen 2 nesnenin de isimleri ayný ise eþleþmeyi baþarýlý olarak sayýyoruz. seçimin baþarýlý olmasý
             * sonucunda seçilen objeleri tekrardan düz hale gtiriyoruz...
             */
            Debug.Log("Eþleþme baþarýlý");
            objClick1.transform.rotation = Quaternion.Euler(0, 0, 0);
            objClick2.transform.rotation = Quaternion.Euler(0, 0, 0);
            foundobj--;
            objClick1 = null;
            objClick2 = null;

        }

        else
        {
            //eðer seçilen objeler uyuþmaz ise bu objelerin colliderlarýný tekrardan aktif ediyoruzz ve oyuncunun can deðerini azaltýyoruz...
            Debug.Log("Eþleþme baþarýsýz");
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
        

        if(GUI.Button(new Rect(Screen.width / 2 + 50, Screen.height - 50, 50, 50), "Sað"))
        {
            //butona basýldýðýnda equator objesinin z eksenindeki açýsýný 10 derece arttýracak...
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
            Screen.height / 2 - 50, 200, 100), "Kazandýn"))
            {
                RestartGame();
            }
        }

        if(lossScrean == true)
        {
            if (GUI.Button(new Rect(Screen.width / 2 - 100,
            Screen.height / 2 - 50, 200, 100), "KAYBETÝN"))
            {
                RestartGame();
            }
        }


    }
     public void RestartGame()// sahneyi tekrar yükleyerek oyunu yeniden baþlatýr...
    {

        SceneManager.LoadScene(0);
    }

}
