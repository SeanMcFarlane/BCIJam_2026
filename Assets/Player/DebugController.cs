using UnityEngine;

public class DebugController : MonoBehaviour
{
    public BattleActor BattleActor;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
            BattleActor.PerformAttack();

        if (Input.GetKeyDown(KeyCode.Alpha1))
            PlayerLoadout.Instance?.SelectNormalArm();

        if (Input.GetKeyDown(KeyCode.Alpha2))
            PlayerLoadout.Instance?.SelectExplosiveArm();

        if (Input.GetKeyDown(KeyCode.Alpha3))
            PlayerLoadout.Instance?.SelectLaserArm();
    }
}
