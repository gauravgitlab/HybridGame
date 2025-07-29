public class Launcher : Weapon
{
    private const string ANIM_IDLE = "LauncherIdle";
    private const string ANIM_SHOOT = "LauncherShoot";

    protected override string GetIdleAnimation()
    {
        return ANIM_IDLE;
    }

    protected override string GetShootAnimation()
    {
        return ANIM_SHOOT;
    }
}