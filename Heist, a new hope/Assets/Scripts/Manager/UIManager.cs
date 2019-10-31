using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    UIManager Instance;

    [Header("AllUiSlots")]
    [SerializeField] Sc_Chara_Interface[] allCharaCards;
    [SerializeField] Image chaosBar;
    [SerializeField] Text timeElpased;

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        UpdateHostageStressBar();
    }

    void UpdateHostageStressBar()
    {
        foreach(Mb_IAHostage hostage in IAManager.Instance.IAList)
        {
            hostage.stressBar.fillAmount = hostage.stress;
        }
    }
}
