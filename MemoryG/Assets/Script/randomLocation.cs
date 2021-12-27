using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class randomLocation : MonoBehaviour
{
    public Transform patrolRoute;
    public List<Transform> locations;
    public List<int> locId;
    public GameObject shape1, shape2, shape3, shape4;
    int createdObj = 4;//oyunda olu�turulacak 2 li obje say�s�
    int loc;
    int choose = 1;// olu�turulacak 2 li obje say�s�ndan hangisinin olu�turaca��n� se�mek i�in tan�mlanan de�i�ken...
    private float Countdown = 10.0f;//geri say�m s�ressi
    private control cnrt;
    
    private void Start()
    {
        cnrt = GameObject.Find("Main Camera").GetComponent<control>();//geri say�m s�resini control conpanentine e�itliyoruz..
        InitializePatrolRoute();
        cnrt.foundobj = createdObj;
        cnrt.time = Countdown;// gerisay�m s�resinni conrtrol s�n�f�ndaki sayac de�i�enine at�yoruz...
        while (createdObj > 0)
        {
            createObject();
            createdObj--;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {// control s�n�f�ndaki sayac de�i�leni 0 ile -2 aras�ndayken geri say�m� bitirip turn fonjsiyunu �a��r�p nesneleri ters �eviriyoruz
            if (cnrt.time < 0 && cnrt.time > -2) { 
            turn();
        }

    }
    public void InitializePatrolRoute()
    {
        int k = 0;
        // oyunda olu�turdu�umuz bo� loc nesnelerine yedi�den ad verip locations listesinde ekiloruz. 
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
         * olu�turaca��m objenin random bir lokasyonda olu�mas� i�in locations listesinden random bir indeks se�iyoruz.
         * bu indeksi locID listesine at�yoruz. sonradan bir obje olu�tururken e�er random se�ilen lokasyon locId listesinde
         * varsa yeniden bir random lokasyon se�iyoruz.
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
         * her objeden s�ra ile olu�turacak objeleri random olu�turdu�um 
         * zaman t�m olu�turulan objeleri ayn� olu�turabilir...
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
            //e�er oyun i�erisinde 2li olarak 4 ten fazla obje olu�turulacaksa 4. �ekli olu�turduktan sonra tekrar ilk nesneden ba�layacak...

        }

    }

    public void turn()
    {
        // listeye atanan loc de�erlerinin hepsii x ekesninde 180 derece �evirecek ve bu loc un i�inde �ekil objeside ters d�nm�� olacak .
        for(int j = 0; j < locations.Count; j++)
        {
            GameObject.Find(locations[j].name).transform.rotation = Quaternion.Euler(180, 0, 0);
        }
        
    }
}
