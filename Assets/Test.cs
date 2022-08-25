using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Test : MonoBehaviour
{
    
    private void Start()
    {

        GameManager.instance.SetTexts();
        SkillManager.instance.SetTextsSkill();
        gameObject.SetActive(false);

        
    }

    
}
