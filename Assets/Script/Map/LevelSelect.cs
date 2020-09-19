using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelSelect : MonoBehaviour{

    public void GameStart() {
        PlayerPrefs.SetString("mapName", transform.parent.parent.parent.name);
        PlayerPrefs.SetString("levelName", transform.parent.name);
        SceneManager.LoadScene(2);
    }

    private void Awake() {
        transform.Find("num").gameObject.GetComponent<Text>().text = transform.parent.name;        
    }

}
