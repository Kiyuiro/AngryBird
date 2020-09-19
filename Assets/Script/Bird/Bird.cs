using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour {

    private bool isClick = false;   // 是否点击
    private bool isFly = false;     // 是否飞出
    private bool isReady = false;   // 是否准备
    private float maxDis = 1.5f;       // 距离弹弓最大距离
    private float acceleration = 0.1f;   // 加速度时间
    private float smooth = 3;

    [HideInInspector]
    public SpringJoint2D sp;
    [HideInInspector]
    public Rigidbody2D rg;
    [HideInInspector]
    public SpriteRenderer render;

    public LineRenderer right;
    public Transform rightPos;
    public LineRenderer left;
    public Transform leftPos;
    public GameObject boom;            // 爆炸动画
    public GameObject shadow;          // 残影动画

    public AudioClip select;
    public AudioClip fly;
    public AudioClip collision;

    public void SetIsReady(bool b) {
        isReady = b;
    }

    private void OnMouseDown() {    // 鼠标按下时
        if (isReady && isFly == false) {
            isClick = true;
            rg.isKinematic = true;
            AudioPlay(select);
        }
    }

    private void OnMouseUp() {      // 鼠标抬起时
        if (isReady && isFly == false) {
            isClick = false;
            rg.isKinematic = false;
            Invoke("Fly", acceleration);
            right.enabled = false;
            left.enabled = false;
        }
    }

    void OnCollisionEnter2D(Collision2D collision) {    // 碰撞时
        // 碰撞速度
        float speed = collision.relativeVelocity.magnitude;
        AudioPlay(this.collision);
        if (isFly == true) {
            Invoke("Next", 3f);
        }
    }

    void Fly() {
        isFly = true;
        sp.enabled = false;
        Shadow();
        AudioPlay(fly);
    }

    void DrawLine() {   // 画出弹弓的线
        right.SetPosition(0, rightPos.position);
        right.SetPosition(1, transform.position);
        left.SetPosition(0, leftPos.position);
        left.SetPosition(1, transform.position);
    }

    void Next() {   
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom, transform.position, Quaternion.identity);
        GameManager._instance.NextBird();
    }

    void Shadow() {   // 残影
        if(isFly) {
            Invoke("Shadow", 0.1f);
            Instantiate(shadow, transform.position, transform.rotation);
        } 
    }

    public virtual void Skill() { }

    void AudioPlay(AudioClip clip) {
        AudioSource.PlayClipAtPoint(clip, new Vector3(0, 0, 0));
    }

    private void Awake() {
        sp = GetComponent<SpringJoint2D>();
        rg = GetComponent<Rigidbody2D>();
        sp.connectedAnchor = rightPos.transform.position;
        render = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        DrawLine();
        if (isClick) {
            // Input.mousePosition   --获取鼠标的坐标
            // Camera.main.ScreenToWorldPoint   --将鼠标的坐标转化为世界坐标
            transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position += new Vector3(0, 0, -Camera.main.transform.position.z);
            // 进行位置限定
            if (Vector3.Distance(transform.position, rightPos.position) > maxDis) {
                // 单位化向量
                Vector3 pos = (transform.position - rightPos.position).normalized;
                // 最大程度的向量
                pos *= maxDis;
                transform.position = pos + rightPos.position;
            }
        }
        float posX = transform.position.x;
        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, 
            new Vector3(Mathf.Clamp(posX, 0, 15), Camera.main.transform.position.y, 
            Camera.main.transform.position.z), smooth * Time.deltaTime);
        if (isFly == true && Input.GetMouseButtonDown(0)) {
            Skill();
        }
    }

}
