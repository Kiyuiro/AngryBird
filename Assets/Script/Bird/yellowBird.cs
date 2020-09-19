using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class yellowBird : Bird
{
    private int skillUseCount = 1;
    public override void Skill() {
        if (skillUseCount > 0) {
            rg.velocity *= 2;
            //Physics.gravity = new Vector3(0, -1.0F, 0);
            skillUseCount--;
        }
    }
}
