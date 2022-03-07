using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class UndergroundCollision : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!Game.isGameOver)
        {

       

        string tag = other.tag;

        if( tag.Equals("Object"))
        {
           // Debug.Log("this is object");
            Level.Instance.objectInScene--;
            UIManager.Instance.UpdateLevelProgress();
            Destroy(other.gameObject);

                if (Level.Instance.objectInScene == 0)
                {//no more object to collect
                 // Level.Instance.LoadNextLevel();
                    UIManager.Instance.ShowLevelCompletedUI();
                    Level.Instance.PlayWinFx();
                    Invoke("NextLevel", 2f);
                    Game.isGameOver = true; //level completed yazısından sonra hole un durması
                }
        }


        if (tag.Equals("Obstacles"))
        {
            Game.isGameOver = true;
            Camera.main.transform
                   .DOShakePosition(1f, .2f, 20, 90f)
                    .OnComplete(() =>
                     {
                        Level.Instance.RestartLevel();
                     });
                  

        }

    }
    }
    void NextLevel()
    {
        Level.Instance.LoadNextLevel();

    }
}
