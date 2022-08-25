using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UfoSpawnerSub : MonoBehaviour
{
    public GameObject ufo;
    BoxCollider2D rangeCollider;
    // Start is called before the first frame update
    void Awake()
    {
        rangeCollider = GetComponent<BoxCollider2D>();
    }

    public void SpawnUfo()
    {
        float range_X = rangeCollider.bounds.size.x;
        float range_Y = rangeCollider.bounds.size.y;

        range_X = Random.Range((range_X / 2) * -1, range_X / 2);
        range_Y = Random.Range((range_Y / 2) * -1, range_Y / 2);

        GameObject clone = (GameObject)Instantiate(ufo, new Vector3(transform.position.x + range_X, transform.position.y + range_Y, 0f), Quaternion.identity);
    }
}
