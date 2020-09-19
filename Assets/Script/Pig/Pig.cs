using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pig : Collision {
    
    public GameObject score;
    public AudioClip deadAudio;

    public override void Display() {
        GameManager._instance.pigs.Remove(this);
        base.Display();
        // 创建分数出现效果
        Instantiate(score, transform.position, Quaternion.identity);
    }
    public override void OnCollisionEnter2D(Collision2D collision) {
        base.OnCollisionEnter2D(collision);
        if (base.life < 0) base.AudioPlay(deadAudio);
    }
}
