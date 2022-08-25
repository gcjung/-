using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class DropImage : MonoBehaviour
{
    private float moveSpeed;
    private float alphaSpeed;
    private float destroyTime;
    Image image;
    Color alpha;
    void Start()
    {
        moveSpeed = 10.0f;
        alphaSpeed = 0.3f;
        destroyTime = 2.0f;

        image = GetComponent<Image>();
        alpha = image.color;
        Invoke("DestroyObject", destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, moveSpeed * Time.deltaTime, 0));

        alpha.a = Mathf.Lerp(alpha.a, 0, Time.deltaTime * alphaSpeed);
        image.color = alpha;
    }

    private void DestroyObject()
    {
        Destroy(gameObject);
    }
}
