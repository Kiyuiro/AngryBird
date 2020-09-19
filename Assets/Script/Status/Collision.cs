using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collision : MonoBehaviour {

    private SpriteRenderer render;
    [HideInInspector]
    public float life = 12f;

    public Sprite[] lnjured = new Sprite[4];           // 受伤图片
    public GameObject boom;            // 爆炸动画

    public AudioClip displayAudio;
    public AudioClip lnjuredAudio;

    public virtual void OnCollisionEnter2D(Collision2D collision) {    // 碰撞时
        // 碰撞速度
        life -= collision.relativeVelocity.magnitude;
        // 改为受伤时的图片
        if (life < 10) render.sprite = lnjured[0];
        if (life < 7.5) render.sprite = lnjured[1];
        if (life < 5) render.sprite = lnjured[2];
        if (life < 2.5) render.sprite = lnjured[3];
        if (life < 0) { Display(); AudioPlay(displayAudio); }
        if (life > 0) { AudioPlay(lnjuredAudio); }
    }

    public virtual void Display() {
        Destroy(gameObject);
        // 创建爆炸效果
        Instantiate(boom, transform.position, Quaternion.identity);
    }

    public void AudioPlay(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, 0));
    }

    private void Awake() {
        render = GetComponent<SpriteRenderer>();
    }

}
