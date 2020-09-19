using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;
using System;

public class Win : MonoBehaviour
{
    public GameObject centerStar;
    public GameObject loseCenterStar;
    public GameObject rightStar;
    public GameObject loseRightStar;
    public GameObject leftRoot;
    public GameObject rightRoot;
    public GameObject centerRoot;

    private int starNum;
    public int getStarNum() { return starNum; }

    public void LevelStar() {
        int birdCount = GameManager._instance.birds.Count;
        if (birdCount == 0) { 
            centerStar.SetActive(false);
            rightStar.SetActive(false);
            loseCenterStar.SetActive(true);
            loseRightStar.SetActive(true);
            starNum = 1;
        }
        if (birdCount == 1) {
            rightStar.SetActive(false);
            loseRightStar.SetActive(true);
            starNum = 2;
        }
        if (birdCount > 1) {
            starNum = 3;
        }
        Game.changeFile(Convert.ToInt32(PlayerPrefs.GetString("mapName").Split('_')[1]), 
                        Convert.ToInt32(PlayerPrefs.GetString("levelName")), 
                        starNum);
    }

    void Left() { leftRoot.SetActive(true); }
    void Right() { if(rightStar.activeInHierarchy) rightRoot.SetActive(true); }
    void Center() { if(centerStar.activeInHierarchy) centerRoot.SetActive(true); }

}
