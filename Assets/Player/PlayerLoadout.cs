using UnityEngine;

public enum ArmType
{
    NormalArm,
    ExplosiveArm,
    LaserArm
}

public class PlayerLoadout : MonoBehaviour
{
    public static PlayerLoadout Instance { get; private set; }

    public ArmType SelectedArm = ArmType.NormalArm;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SelectArm(ArmType arm)
    {
        SelectedArm = arm;
        Debug.Log($"Selected arm: {arm}");
    }

    public void SelectNormalArm() => SelectArm(ArmType.NormalArm);
    public void SelectExplosiveArm() => SelectArm(ArmType.ExplosiveArm);
    public void SelectLaserArm() => SelectArm(ArmType.LaserArm);
}
