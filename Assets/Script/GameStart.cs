using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour {

    private void Awake() {
        //Debug.Log(mapName+ " " + levelName);
        Instantiate(Resources.Load(PlayerPrefs.GetString("mapName") + "/" + PlayerPrefs.GetString("levelName")) as GameObject);
    }
}    

