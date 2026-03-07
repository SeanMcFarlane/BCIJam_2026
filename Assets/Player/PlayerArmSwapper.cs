using UnityEngine;

public class PlayerArmSwapper : MonoBehaviour
{
    public GameObject NormalArm;
    public GameObject LaserArm;
    public GameObject ExplosiveArm;

    void Start()
    {
        NormalArm.SetActive(false);
        LaserArm.SetActive(false);
        ExplosiveArm.SetActive(false);

        ArmType selectedArm = PlayerLoadout.Instance != null ? PlayerLoadout.Instance.SelectedArm : ArmType.NormalArm;

        switch (selectedArm)
        {
            case ArmType.NormalArm:
                NormalArm.SetActive(true);
                break;
            case ArmType.LaserArm:
                LaserArm.SetActive(true);
                break;
            case ArmType.ExplosiveArm:
                ExplosiveArm.SetActive(true);
                break;
        }
    }
}
