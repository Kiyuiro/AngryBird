using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;

public class MapOpen : MonoBehaviour
{

    private bool select = false;
    public int starCount;

    public GameObject unlock;
    public GameObject stars;
    public GameObject panel;
    public GameObject map;

    public void Selected() {
        if (select) {
            panel.SetActive(true);
            map.SetActive(false);
        } // 选择地图后的操作
    }

    private void Start() {   
        if (Game.GetStarCount() >= starCount) {
            select = true;
        } // 判断地图是否开启
        if (select) {
            unlock.SetActive(false);
            stars.SetActive(true);
        }
    }

}
