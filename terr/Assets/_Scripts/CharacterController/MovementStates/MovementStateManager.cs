using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementStateManager : MonoBehaviour
{
    [Header("Set in Inspecot: Character options")]
    #region Ground
    [SerializeField] private float groundYOffset;
    [SerializeField] LayerMask groundMask;
    private Vector3 spherePos;
    #endregion

    #region Gravity
    [SerializeField] private float gravity = -9.81f;
    private Vector3 velocity;
    #endregion

    #region Movement
    [SerializeField] private float currentMoveSpeed;
    [SerializeField] private float walkSpeed = 3, walkBackSpeed = 2;
    [SerializeField] private float runSpeed = 7, runBackSpeed = 5;
    [SerializeField] private float crouchSpeed = 2, crouchBackSpeed = 1;

    [SerializeField] private Vector3 dir;
    private float _hzInput, _vInput;
    private CharacterController _controller;

    public float CurrentMoveSpeed { get=> currentMoveSpeed; set=> currentMoveSpeed=value; }
    public float WalkSpeed { get => walkSpeed; private set => walkSpeed = value; }
    public float WalkBackSpeed { get => walkBackSpeed; private set => walkBackSpeed = value; }
    public float RunSpeed { get => runSpeed; private set => runSpeed = value; }
    public float RunBackSpeed { get => runBackSpeed; private set => runBackSpeed = value; }
    public float CrouchSpeed { get => crouchSpeed; private set => crouchSpeed = value; }
    public float CrouchBackSpeed { get => crouchBackSpeed; private set => crouchBackSpeed = value; }
    public Vector3 Dir { get => dir; private set => dir = value; }
    public float VInput { get => _vInput; }
    public float HZInput { get => _hzInput; }

    #endregion

    #region Animataion
    private MovementBaseState currentState;

    [SerializeField] private Animator anim;
    public IdleState Idle { get; set; } = new IdleState();
    public WalkState Walk { get; set; } = new WalkState();
    public CrouchState Crouch { get; set; } = new CrouchState();
    public RunState Run { get; set; } = new RunState();
    public Animator Anim { get => anim; private set => anim = value; }
    #endregion

    #region Sounds
    [SerializeField] private AudioSource audioWalking;
    [SerializeField] private AudioSource audioRunning;
    #endregion
    private void Awake()
    {
        anim = GetComponent<Animator>();
        _controller = GetComponent<CharacterController>();
        SwitchState(Idle);
    }

    private void Update()
    {
        GetDirectionAndMove();
        Gravity();
        anim.SetFloat("hzInput", _hzInput);
        anim.SetFloat("vInput", _vInput);
        currentState.UpdateState(this);
    }

    public void SwitchState(MovementBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    private void GetDirectionAndMove()
    {
        _hzInput = Input.GetAxis("Horizontal");
        _vInput = Input.GetAxis("Vertical");

        dir = transform.forward * _vInput + transform.right*_hzInput;

        _controller.Move(dir.normalized * currentMoveSpeed * Time.deltaTime);
    }

    private bool IsGrounded()
    {
        spherePos = new Vector3(transform.position.x, transform.position.y+groundYOffset, transform.position.z);
        if (Physics.CheckSphere(spherePos, _controller.radius - 0.05f, groundMask)) return true;
        return false;
    }

    private void Gravity()
    {
        if (!IsGrounded()) velocity.y += gravity * Time.deltaTime;
        else if (velocity.y < 0) velocity.y = -2;

        _controller.Move(velocity * Time.deltaTime);
    }

    public void SetAudioWalking(bool play)
    {
        audioWalking.gameObject.SetActive(play);
    }

    public void SetAudioRunning(bool play)
    {
        audioWalking.gameObject.SetActive(play);
    }
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(spherePos, _controller.radius - 0.05f);
    //}
}
