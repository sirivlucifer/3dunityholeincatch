using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Level : MonoBehaviour
{

    #region Singleton class: UIManager

    public static Level Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    [SerializeField] ParticleSystem winFx;

    [Space]
    public int objectInScene;
    public int totalObjects;

    [SerializeField] Transform objectsParent;

    [Space]
    [Header("Level Objects and Obstacles")]
    [SerializeField] Material groundMaterial;
    [SerializeField] Material objectMaterial;
    [SerializeField] Material obstacleMaterial;
    [SerializeField] SpriteRenderer groundBorderSprite;
    [SerializeField] SpriteRenderer groundSideSprite;
    [SerializeField] SpriteRenderer bgFadeSprite;
    [SerializeField] Image progressFillImage;

    [Space]
    [Header("Level Colors------")]
    [Header("Ground")]
    [SerializeField] Color groundColor;
    [SerializeField] Color bordersColor;
    [SerializeField] Color sideColor;
    [Header("Objects & Obstacles")]
    [SerializeField] Color objectColor;
    [SerializeField] Color obstacleColor;

    [Header("UI(progress")]
    [SerializeField] Color progressFillColor;

    [Header("Background")]
    [SerializeField] Color cameraColor;
    [SerializeField] Color fadeColor;






    void Start()
    {
        CountObject();
        UpdateLevelColors();
    }

   void CountObject ()
    {
        totalObjects = objectsParent.childCount;
        objectInScene = totalObjects;


    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex );
    }

    public void PlayWinFx()
    {
        winFx.Play();
    }

    public void UpdateLevelColors()
    {
        groundMaterial.color = groundColor;
        groundSideSprite.color = sideColor;
        groundBorderSprite.color = bordersColor;

        obstacleMaterial.color = obstacleColor;
        objectMaterial.color = objectColor;

        progressFillImage.color = progressFillColor;
        Camera.main.backgroundColor = cameraColor;
        bgFadeSprite.color = fadeColor;


    }
    void OnValidate()
    {
        UpdateLevelColors();
    }
}
