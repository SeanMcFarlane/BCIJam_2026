using UnityEngine;

public class EnemyActor : MonoBehaviour
{
    private Animator _animator;
    private static readonly int _animHit = Animator.StringToHash("Hit");

    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void TakeHit()
    {
        _animator?.SetTrigger(_animHit);
    }
}
