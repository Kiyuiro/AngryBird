using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class greenBird : Bird
{
    public Sprite skillImage;
    private int skillUseCount = 1;
    public override void Skill() {
        if (skillUseCount > 0) {
            Vector3 speed = rg.velocity;
            speed.x *= -1;
            rg.velocity = speed;
            render.sprite = skillImage;
            skillUseCount--;
        }
    }
}
