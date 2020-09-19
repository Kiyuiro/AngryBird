using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour{
    public List<Bird> birds;
    public List<Pig> pigs;
    public List<Wood> woods;
    public List<Stone> stones;
    public List<Glass> glasses;
    public static GameManager _instance;
    private Vector3 pos;

    public GameObject win;
    public GameObject lose;
    public GameObject pause;

    public AudioClip BGM;

    // 存储数据
    private int startNum;

    private void Initialized() {
        for (int i = 0; i < birds.Count; i++) {
            if (i == 0) {
                birds[i].transform.position = pos;
                birds[i].enabled = true;
                birds[i].SetIsReady(true);
                birds[i].sp.enabled = true;
                birds[i].right.enabled = true;
                birds[i].left.enabled = true;
            } else {
                birds[i].enabled = false;
                birds[i].sp.enabled = false;
            }
        }
    }

    public void NextBird() {
        if(pigs.Count > 0) {
            if (birds.Count > 0) {
                Initialized();
            } else {
                // lost
                lose.SetActive(true);
            }
        } else {
            // win
            win.SetActive(true);
        }
    }
    
    public void Replay() {      // 重新开始
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

    public void Home() {        // 返回目录
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }

    public void Pause() {
        Time.timeScale = 0;
        pause.GetComponent<Animator>().SetBool("isPause", true);
    }

    public void PauseDis() {
        Time.timeScale = 1;
        pause.GetComponent<Animator>().SetBool("isPause", false);
    }

    private void Start() {
        Initialized();
    }

    private void Awake() {
        _instance = this;
        pause.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
        if(birds.Count > 0) {
            // 初始化鸟的位子
            pos = birds[0].transform.position;
        }
    }
}
