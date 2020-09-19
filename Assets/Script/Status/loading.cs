using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GameSave;

public class loading : MonoBehaviour
{
    private void Awake() {
        Game.Action();
    }
    void Start()
    {
        Invoke("load", 3);
    }
    void load() {
        SceneManager.LoadScene(1);
    }
}
