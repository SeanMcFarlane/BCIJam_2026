using System.Collections;
using UnityEngine;
using StarterAssets;

public class BattleActor : MonoBehaviour
{
    public Transform StartPoint;
    public Transform EndPoint;
    public BattleActor Target;
    public float MoveSpeed = 5f;
    public float AnimationSpeed = 1f;
    public float PunchWaitTime = 0.5f;
    public float TurnSpeed = 5f;
    public Animator CharacterAnimator;
    public AudioClip NormalArmSFX;
    public AudioClip LaserArmSFX;
    public AudioClip ExplosiveArmSFX;

    private CharacterController _characterController;
    private ThirdPersonController _thirdPersonController;
    private AudioSource _audioSource;

    private static readonly int _animHit = Animator.StringToHash("Hit");

    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _thirdPersonController = GetComponent<ThirdPersonController>();
        _audioSource = GetComponent<AudioSource>();

        transform.position = StartPoint.position;
        transform.rotation = Quaternion.LookRotation(EndPoint.position - StartPoint.position);
    }

    public void PerformAttack()
    {
        StartCoroutine(AttackRoutine());
    }

    public IEnumerator AttackRoutine()
    {
        _thirdPersonController.enabled = false;
        _characterController.enabled = false;

        // Teleport to start and face the end point
        transform.position = StartPoint.position;
        yield return StartCoroutine(RotateToFace(EndPoint.position));

        // Move to EndPoint
        CharacterAnimator.SetFloat("Speed", AnimationSpeed);
        yield return StartCoroutine(MoveToTarget(EndPoint.position));
        CharacterAnimator.SetFloat("Speed", 0f);

        // Fire punch animation and notify target
        CharacterAnimator.SetTrigger("Punching");
        PlayPunchSFX();
        Target?.TakeHit();
        yield return new WaitForSeconds(PunchWaitTime);

        // Turn to face StartPoint before running back
        yield return StartCoroutine(RotateToFace(StartPoint.position));

        // Lerp back to StartPoint
        CharacterAnimator.SetFloat("Speed", AnimationSpeed);
        yield return StartCoroutine(MoveToTarget(StartPoint.position));
        CharacterAnimator.SetFloat("Speed", 0f);

        // Face EndPoint again
        yield return StartCoroutine(RotateToFace(EndPoint.position));

        _characterController.enabled = true;
        _thirdPersonController.enabled = true;
    }

    public void TakeHit() {
        CharacterAnimator.SetTrigger(_animHit);
    }

    private IEnumerator RotateToFace(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        dir.y = 0f;
        if (dir == Vector3.zero) yield break;

        Quaternion targetRotation = Quaternion.LookRotation(dir);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.5f)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, TurnSpeed * 100f * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRotation;
    }

    private IEnumerator MoveToTarget(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, MoveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = target;
    }

    private void PlayPunchSFX()
    {
        if (_audioSource == null) return;

        ArmType arm = PlayerLoadout.Instance != null ? PlayerLoadout.Instance.SelectedArm : ArmType.NormalArm;
        AudioClip clip = arm switch
        {
            ArmType.LaserArm => LaserArmSFX,
            ArmType.ExplosiveArm => ExplosiveArmSFX,
            _ => NormalArmSFX
        };

        if (clip != null)
            _audioSource.PlayOneShot(clip);
    }

    //void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.K))
    //    {
    //        PerformAttack();
    //    }
    //}
}
