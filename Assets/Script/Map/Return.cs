using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Return : MonoBehaviour
{
    public void returnButton() {
        transform.parent.gameObject.SetActive(false);
        transform.parent.parent.Find("map").gameObject.SetActive(true);
    }
}
