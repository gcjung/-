using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NoMoneyText : MonoBehaviour
{
    private Text noMoneyText;
    private RectTransform rectTransform;
    private IEnumerator coroutine;
    private void Awake()
    {
        noMoneyText = GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        coroutine = FadeAway();
    }

    public void OnNoMoneyText()
    {
        StopCoroutine(coroutine);

        coroutine = FadeAway();
        noMoneyText.color = Color.red;
        
        StartCoroutine(coroutine);
    }

    void FixedUpdate()
    {
        if (gameObject.activeSelf == true)
        {
            transform.Translate(Vector2.up * 2f);
        }
    }

    private IEnumerator FadeAway()
    {
        yield return new WaitForSeconds(0.5f);

        while (noMoneyText.color.a > 0)
        {
            var color = noMoneyText.color;
            color.a -= (0.85f * Time.deltaTime);        //0.01 사라지는 속도
            noMoneyText.color = color;

            yield return null;
        }

        gameObject.SetActive(false);
    }

}
