using GameSave;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitializationGame : MonoBehaviour
{
    private void Awake() {
        Game.Action();
    }
}
