using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawn : MonoBehaviour
{
    public GameObject spawn1;
    public GameObject spawn2;
    public GameObject spawn3;
    public GameObject spawn4;

    void Start()
    {
       StartCoroutine(SelectSpawn());       
    }

    IEnumerator SelectSpawn()
    {
        while (true)
        {
            int range = Random.Range(30, 51);

            yield return new WaitForSeconds(range);

            int spwanpoint = Random.Range(1, 5);

            if (spwanpoint == 1)
            {

                spawn1.GetComponent<UfoSpawnerSub>().SpawnUfo();
                yield return new WaitForSeconds(5);
            }
            else if (spwanpoint == 2)
            {
                spawn2.GetComponent<UfoSpawnerSub>().SpawnUfo();
                yield return new WaitForSeconds(5);
            }
            else if (spwanpoint == 3)
            {
                spawn3.GetComponent<UfoSpawnerSub>().SpawnUfo();
                yield return new WaitForSeconds(5);
            }
            else if (spwanpoint == 4)
            {
                spawn4.GetComponent<UfoSpawnerSub>().SpawnUfo();
                yield return new WaitForSeconds(5);
            }
        }

    }
}
