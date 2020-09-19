using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameSave;
using UnityEngine.UI;

public class MapStars : MonoBehaviour
{
    private void Awake() {
        transform.GetComponent<Text>().text = Game.GetMapStar(1) + "/" + 3*24;
    }
}
