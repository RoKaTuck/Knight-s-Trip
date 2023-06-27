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

    // ���� ����    
    public bool _isGround = true;

    // �� ���� ����
    private CapsuleCollider _capsuleCollider;

    [Header("Camera Attribute")]    
    [SerializeField]
    private float _cameraRotationLimit;
    private float _currentCameraRotationX = 0f; // ī�޶� �Ѱ�
    [SerializeField]
    private float _lookSensitivity; // ī�޶� �ΰ���

    // �ʿ��� ������Ʈ
    [SerializeField]
    private Camera _theCamera;
    private Rigidbody _rigid;
    private PlayerAnimCtrl _animCtrl;
    private StatusCtrl _statusCtrl;

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
        _statusCtrl = FindObjectOfType<StatusCtrl>();
    }       

    #region Rigid(O) Move

    public void MoveState(int x, int z)
    {
        _animCtrl.MoveAnim(x, z);
    }

    public void IsGround()
    {
        Vector3 myTr = new Vector3(transform.position.x, transform.position.y + 0.5f, transform.position.z);
        _isGround = Physics.Raycast(myTr, Vector3.down, _capsuleCollider.bounds.extents.y + 0.1f);
        Debug.DrawRay(transform.position, Vector3.down * (_capsuleCollider.bounds.extents.y + 0.1f), Color.red);

        if (_isGround == false)
            _animCtrl.JumpAnim(_isGround);
    }

    public void TryRoll()
    {
        if (Input.GetKeyDown(KeyCode.LeftControl) && _Move == true)
            Roll();
    }

    public void TryJump()
    {
        if(Input.GetKeyDown(KeyCode.Space) && _isGround == true)
            Jump();
    }

    private void Jump()
    {
        _rigid.velocity = transform.up * _jumpForce;
        _animCtrl.JumpAnim(_isGround);
    }

    private void Roll()
    {
        _applySpeed = _rollSpeed;
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
        // �¿� ĳ���� ȸ��
        float rotationY            = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, rotationY, 0f) * _lookSensitivity;

        _rigid.MoveRotation(_rigid.rotation * Quaternion.Euler(characterRotationY));
    }

    public void RigidMove()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHoriziontal = transform.right * inputX;
        Vector3 moveVertical    = transform.forward * inputZ;

        Vector3 velocity = (moveHoriziontal + moveVertical).normalized * _applySpeed;

        _rigid.MovePosition(transform.position + velocity * Time.deltaTime);

        if ((inputX <= 0.1f && inputX > 0) || (inputX >= -0.01 && inputX < 0))
            inputX = 0f;

        if ((inputZ <= 0.1f && inputZ > 0) || (inputZ >= -0.02 && inputZ < 0))
            inputZ = 0f;

        _animCtrl.MoveAnim(inputX, inputZ);  
    }

    public void CameraRotation()
    {
        // ���� ī�޶� ȸ��
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
