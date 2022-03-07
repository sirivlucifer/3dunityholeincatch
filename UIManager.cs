using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UIManager : MonoBehaviour
{

    #region Singleton class: UIManager

    public static UIManager Instance;

     void Awake()
    {
        if (Instance == null)
        {
            Instance=this;
        }
    }

    #endregion


    [Header("Level Progress UI")]
    [SerializeField] int sceneOffset;
    [SerializeField] TMP_Text nextLevelText;
    [SerializeField] TMP_Text currentLevelText;
    [SerializeField] Image progressFillImage;

    [Space]
    [SerializeField] TMP_Text levelCompletedText;

    [Space]
    [SerializeField] Image fadePanel;





    void Start()
    {
        FateAtStart();
        progressFillImage.fillAmount = 0f;
        SetLevelProgressText();
    }

    void SetLevelProgressText()
    {
        int level = SceneManager.GetActiveScene().buildIndex + sceneOffset;
        nextLevelText.text = (level + 1).ToString();
        currentLevelText.text = level .ToString();
    }

    
    public void UpdateLevelProgress()
    {
        
        float val = 1f - ((float)Level.Instance.objectInScene / Level.Instance.totalObjects);
        //progressFillImage.fillAmount = val;
        progressFillImage.DOFillAmount(val, .4f);


    }

    public void ShowLevelCompletedUI()
    {
        levelCompletedText.DOFade(1f, .6f);
    }

    public void FateAtStart()
    {
        fadePanel.DOFade(0f, 1.3f).From(1f);
    }
}

