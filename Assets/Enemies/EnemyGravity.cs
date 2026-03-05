using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class EnemyGravity : MonoBehaviour
{
    private CharacterController _controller;
    private Animator _animator;
    private float _verticalVelocity;

    private static readonly int _animGrounded = Animator.StringToHash("Grounded");
    private static readonly int _animFreeFall = Animator.StringToHash("FreeFall");
    private static readonly int _animMotionSpeed = Animator.StringToHash("MotionSpeed");

    void Start()
    {
        _controller = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
        _animator?.SetBool(_animGrounded, true);
        _animator?.SetBool(_animFreeFall, false);
        _animator?.SetFloat(_animMotionSpeed, 1f);
    }

    void Update()
    {
        if (_controller.isGrounded)
        {
            _verticalVelocity = -2f;
            _animator?.SetBool(_animGrounded, true);
            _animator?.SetBool(_animFreeFall, false);
            _animator?.SetFloat(_animMotionSpeed, 1f);
        }
        else
        {
            _verticalVelocity += Physics.gravity.y * Time.deltaTime;
            _animator?.SetBool(_animGrounded, false);
            _animator?.SetBool(_animFreeFall, true);
        }

        _controller.Move(new Vector3(0, _verticalVelocity * Time.deltaTime, 0));
    }

    private void OnLand(AnimationEvent animationEvent) { }
}
