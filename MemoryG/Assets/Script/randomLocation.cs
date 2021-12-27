using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomLocation : MonoBehaviour
{
    public Transform patrolRoute;
    public List<Transform> locations;
    public List<int> locId;
    public GameObject shape1, shape2, shape3, shape4;
    int createdObj = 4;//oyunda oluþturulacak 2 li obje sayýsý
    int loc;
    int choose = 1;// oluþturulacak 2 li obje sayýsýndan hangisinin oluþturacaðýný seçmek için tanýmlanan deðiþken...
    private float Countdown = 10.0f;//geri sayým süressi
    private control cnrt;
    
    private void Start()
    {
        cnrt = GameObject.Find("Main Camera").GetComponent<control>();//geri sayým süresini control conpanentine eþitliyoruz..
        InitializePatrolRoute();
        cnrt.foundobj = createdObj;
        cnrt.time = Countdown;// gerisayým süresinni conrtrol sýnýfýndaki sayac deðiþenine atýyoruz...
        while (createdObj > 0)
        {
            createObject();
            createdObj--;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {// control sýnýfýndaki sayac deðiþleni 0 ile -2 arasýndayken geri sayýmý bitirip turn fonjsiyunu çaðýrýp nesneleri ters çeviriyoruz
            if (cnrt.time < 0 && cnrt.time > -2) { 
            turn();
        }

    }
    public void InitializePatrolRoute()
    {
        int k = 0;
        // oyunda oluþturduðumuz boþ loc nesnelerine yediþden ad verip locations listesinde ekiloruz. 
        foreach (Transform child in patrolRoute)
        {
            child.name = "Loc" + k;
            locations.Add(child);
            k++;
        }
    }
    public void randLoc()
    {
        /**
         * oluþturacaðým objenin random bir lokasyonda oluþmasý için locations listesinden random bir indeks seçiyoruz.
         * bu indeksi locID listesine atýyoruz. sonradan bir obje oluþtururken eðer random seçilen lokasyon locId listesinde
         * varsa yeniden bir random lokasyon seçiyoruz.
         * */
        loc = Random.Range(0, locations.Count );
        for (int j = 0; j < locId.Count-1; j++)
        {
            if(loc == locId[j])
            {
                loc = Random.Range(0, locations.Count );
                j = 0;
            }

        }
        locId.Add(loc);
    }


   
    public void createObject()
    {

        
        /**
         * her objeden sýra ile oluþturacak objeleri random oluþturduðum 
         * zaman tüm oluþturulan objeleri ayný oluþturabilir...
         * */
        for ( int i = 0; i<2; i++)
        {
            randLoc();
            if (choose   == 1)
            {
                Instantiate(shape1, locations[loc].transform);
            }
            else if(choose == 2)
            {
                Instantiate(shape2, locations[loc].transform);
            }
            else if (choose == 3)
            {
                Instantiate(shape3, locations[loc].transform);
            }
            else if (choose == 4)
            {
                Instantiate(shape4, locations[loc].transform);
            }
            
        }
        choose++;
        if(choose == 5)
        {
            choose = 1;
            //eðer oyun içerisinde 2li olarak 4 ten fazla obje oluþturulacaksa 4. þekli oluþturduktan sonra tekrar ilk nesneden baþlayacak...

        }

    }

    public void turn()
    {
        // listeye atanan loc deðerlerinin hepsii x ekesninde 180 derece çevirecek ve bu loc un içinde þekil objeside ters dönmüþ olacak .
        for(int j = 0; j < locations.Count; j++)
        {
            GameObject.Find(locations[j].name).transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        
    }
}
