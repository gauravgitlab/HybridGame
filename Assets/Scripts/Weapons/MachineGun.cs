using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MachineGun : Weapon
{
    private const string ANIM_IDLE = "Idle_Mgun";
    private const string ANIM_SHOOT = "Shoot";
    
    protected override string GetIdleAnimation()
    {
        return ANIM_IDLE;
    }

    protected override string GetShootAnimation()
    {
        return ANIM_SHOOT;
    }
}
