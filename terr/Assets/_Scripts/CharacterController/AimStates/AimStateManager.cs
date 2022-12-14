using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class AimStateManager : MonoBehaviour
{
    #region Animatio Options
    private Animator anim;
    private AimBaseState currentState;
    public HipFireState Hip { get; set; } = new HipFireState();
    public AimState Aim { get; set; } = new AimState();
    public Animator Anim { get=>anim; private set=>anim=value; }
    #endregion
    #region Camera Options
    [Header("Set in Inspector: aimState options")]
    [SerializeField] private float mouseSense = 1;
    [SerializeField] private Transform camFollowPos;
    [SerializeField] private CinemachineVirtualCamera vCam;
    private float _xAxis, _yAxis;

    [SerializeField] private float adsFov = 40;
    [SerializeField] private float hipFov;
    [SerializeField] private float currentFov;
    [SerializeField] private float fovSmoothSpeed = 10;
    public float AdsFov { get => adsFov; set => adsFov = value; }
    public float HipFov { get => hipFov; set => hipFov = value; }
    public float CurrentFov { get => currentFov; set => currentFov = value; }
    public float FovSmoothSpeed { get => fovSmoothSpeed; set => fovSmoothSpeed = value; }
    public CinemachineVirtualCamera VCam { get => vCam; private set => vCam = value; }

    #endregion

    #region Aiming options
    [SerializeField] private Transform aimPos;
    [SerializeField] private float aimSmoothSpeed;
    [SerializeField] LayerMask aimMask;
    public Transform AimPos { get => aimPos; private set => aimPos = value; }
    public Vector3 HitPoint { get; set; }
    #endregion

    void Awake()
    {
        vCam = GetComponentInChildren<CinemachineVirtualCamera>();
        hipFov = vCam.m_Lens.FieldOfView;
        anim = GetComponent<Animator>();
        SwitchState(Hip);
    }

    void Update()
    {
        try
        {
            _xAxis += Input.GetAxisRaw("Mouse X") * mouseSense;
            _yAxis -= Input.GetAxisRaw("Mouse Y") * mouseSense;
            _yAxis = Mathf.Clamp(_yAxis, -80, 80);

            vCam.m_Lens.FieldOfView = Mathf.Lerp(vCam.m_Lens.FieldOfView, currentFov, fovSmoothSpeed * Time.deltaTime);

            Vector2 screenCeneter = new Vector2(Screen.width / 2, Screen.height / 2);
            Ray ray = Camera.main.ScreenPointToRay(screenCeneter);

            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, aimMask))
            {
                HitPoint = hit.point;
                aimPos.position = Vector3.Lerp(aimPos.position, hit.point, aimSmoothSpeed * Time.deltaTime);
            }

            currentState.UpdateState(this);
        }
        catch (System.NullReferenceException)
        {
            Debug.LogWarning("Check Camera connection");
        }
    }

    private void LateUpdate()
    {
        try { 
        camFollowPos.localEulerAngles = new Vector3(_yAxis, camFollowPos.localEulerAngles.y, camFollowPos.localEulerAngles.z);
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, _xAxis, transform.eulerAngles.z);
        }
        catch (System.NullReferenceException)
        {
            Debug.LogWarning("Check Camera connection");
        }
    }

    public void SwitchState(AimBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }
}
