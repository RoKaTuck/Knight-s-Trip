using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl : MonoBehaviour
{
    [SerializeField, Header("Player Attriubte")]
    private float _playerSpeed = 5f;
    [SerializeField]
    private float _playerRunSpeed;    
    private float _applySpeed;
    [SerializeField]
    private float _rollSpeed;

    [SerializeField]
    private float _jumpForce;

    // 상태 변수    
    public bool _isGround = true;
    private bool _isRoll = false;

    // 땅 착지 여부
    private CapsuleCollider _capsuleCollider;

    [Header("Camera Attribute")]    
    [SerializeField]
    private float _cameraRotationLimit;
    private float _currentCameraRotationX = 0f; // 카메라 한계
    [SerializeField]
    private float _lookSensitivity; // 카메라 민감도

    // 필요한 컴포넌트
    [SerializeField]
    private Camera _theCamera;
    [SerializeField]
    private StatusCtrl _statusCtrl;
    private Rigidbody _rigid;
    private PlayerAnimCtrl _animCtrl;

    public bool _isMove = true;
    public bool _Move { get { return _isMove; } set { _isMove = value; } }

    private void Start()
    {
        Cursor.visible   = false;
        Cursor.lockState = CursorLockMode.Locked;

        _applySpeed = _playerSpeed;
        _rigid      = GetComponent<Rigidbody>();
        _animCtrl   = GetComponent<PlayerAnimCtrl>();
        _capsuleCollider = GetComponent<CapsuleCollider>();        
    }       

    #region Rigid(O) Move

    public void MoveState(int x, int z)
    {
        _animCtrl.MoveAnim(x, z);
    }

    public void OnRollEnd()
    {
        _isRoll = false;
        _applySpeed = _playerSpeed;
        _rigid.velocity = Vector3.zero;
    }

    public void IsGround()
    {
        Vector3 myTr = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        _isGround = Physics.Raycast(myTr, Vector3.down, _capsuleCollider.bounds.extents.y + 0.1f);
        Debug.DrawRay(transform.position, Vector3.down * (_capsuleCollider.bounds.extents.y + 0.1f), Color.red);

        if (_isGround == false)
            _animCtrl.JumpAnim(_isGround);
    }

    public void TryRoll(Vector3 moveDir)
    {
        if (Input.GetKeyDown(KeyCode.LeftControl))
            Roll(moveDir);        
    }

    public void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isGround == true)
            Jump();
    }

    private void Jump()
    {
        if (_isRoll == false)
        {
            _rigid.velocity = transform.up * _jumpForce;
            _animCtrl.JumpAnim(_isGround);
        }
    }

    private void Roll(Vector3 moveDir)
    {        
        _isRoll = true;
        _applySpeed = _rollSpeed;
        //_rigid.AddForce(moveDir * _applySpeed, ForceMode.Impulse);
        _rigid.velocity = moveDir * _applySpeed;
        _animCtrl.RollAnim();
    }

    public void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift) && _statusCtrl._Sp > 0)
            Running();
        if(Input.GetKeyUp(KeyCode.LeftShift) || _statusCtrl._Sp <= 0)
            RunningCancel();
    }

    private void Running()
    {        
        _applySpeed = _playerRunSpeed;
        _statusCtrl.DecreaseStamina(10);        
    }

    private void RunningCancel()
    {        
        _applySpeed = _playerSpeed;        
    }

    public void CharacterRotation()
    {
        // 좌우 캐릭터 회전
        float rotationY            = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, rotationY, 0f) * _lookSensitivity;

        _rigid.MoveRotation(_rigid.rotation * Quaternion.Euler(characterRotationY));
    }

    public void RigidMove()
    {
        if (_isRoll == false)
        {
            float inputX = Input.GetAxisRaw("Horizontal");
            float inputZ = Input.GetAxisRaw("Vertical");

            Vector3 moveHoriziontal = transform.right * inputX;
            Vector3 moveVertical = transform.forward * inputZ;

            Vector3 velocity = (moveHoriziontal + moveVertical).normalized * _applySpeed;

            if (velocity != Vector3.zero && _isGround == true)
            {
                TryRoll(velocity);
            }

            _rigid.MovePosition(transform.position + velocity * Time.deltaTime);

            if ((inputX <= 0.1f && inputX > 0) || (inputX >= -0.01 && inputX < 0))
                inputX = 0f;

            if ((inputZ <= 0.1f && inputZ > 0) || (inputZ >= -0.02 && inputZ < 0))
                inputZ = 0f;

            _animCtrl.MoveAnim(inputX, inputZ);
        }
    }

    public void CameraRotation()
    {
        // 상하 카메라 회전
        float rotationX       = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = rotationX * _lookSensitivity;

        _currentCameraRotationX -= cameraRotationX;
        _currentCameraRotationX  = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit);

        _theCamera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
    }
    #endregion

    public void Death()
    {
        _animCtrl.DeathAnim();
    }
}
