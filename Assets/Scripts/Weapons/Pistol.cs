using UnityEngine;

public class Pistol : Weapon
{
    private const string ANIM_IDLE = "Idle-Pistol";
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
