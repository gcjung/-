using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StarCount : MonoBehaviour
{
    public int stageNumber;
    Image[] childStars;

    public GameObject NextStageLock;

    private void Awake()
    {
        //그니까 저기 숫자1에다가 스테이지 깻을때 값을 가져오면댐 1~3
        PlayerPrefs.SetInt("StarStage"+ stageNumber, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        childStars = transform.GetComponentsInChildren<Image>(true);
        for(int i=0;i<PlayerPrefs.GetInt("StarStage"+stageNumber);i++)
        {
            childStars[i].gameObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetInt("StarStage" + stageNumber, GameManager.instance.Star);
        for (int i = 0; i < PlayerPrefs.GetInt("StarStage" + stageNumber); i++)
        {
            childStars[i].gameObject.SetActive(true);
        }
        if (childStars[0].gameObject.activeSelf)
            NextStageLock.SetActive(false);
    }
}
