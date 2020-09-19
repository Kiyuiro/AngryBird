using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;
using System;

public class LevelOpen : MonoBehaviour
{

    private bool select = false;

    private void starDisplay() {
        int map = Convert.ToInt32(transform.parent.parent.gameObject.name.Split('_')[1]);
        int level = Convert.ToInt32(transform.name);
        int score = Game.GetLevelStar(map, level);
        transform.Find("score").GetChild(score + 1).gameObject.SetActive(true);
    }

    private void Start() {
        string mapID = transform.parent.parent.name.Split('_')[1];
        if ((Convert.ToInt32(transform.name)) <= Game.GetLevel(Convert.ToInt32(mapID))) {
            select = true;
        } // 判断关卡是否开启

        if (select) {
            transform.Find("score").gameObject.SetActive(true);
            transform.Find("lock").gameObject.SetActive(false);
            starDisplay();
        }
    }

}
