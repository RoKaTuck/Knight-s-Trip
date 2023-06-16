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
    private float _playerRotateSpeed = 10f;

    // 달리는지 체크
    private bool _isRun = false;

    [Header("Camera Attribute")]    
    [SerializeField]
    private float _cameraRotationLimit;
    private float _currentCameraRotationX = 0f; // 카메라 한계
    [SerializeField]
    private float _lookSensitivity; // 카메라 민감도

    // 필요한 컴포넌트
    [SerializeField]
    private Camera _theCamera;
    private Rigidbody _rigid;
    private PlayerAnimCtrl _animCtrl;

    public bool _isMove = true;
    public bool _Move { set { _isMove = value; } }

    private void Start()
    {
        Cursor.visible   = false;
        Cursor.lockState = CursorLockMode.Locked;

        _applySpeed = _playerSpeed;
        _rigid      = GetComponent<Rigidbody>();
        _animCtrl   = GetComponent<PlayerAnimCtrl>();
    }
    
    // Update is called once per frame
    private void FixedUpdate()
    {
        if (_isMove == false)
            return;

        if (UiManager._isUiActivated == false && NpcCtrl._isInteracting == false)
            RigidMove();
        else
        {
            _animCtrl.MoveAnim(0, 0);
            _animCtrl.SprintAnim(false);
        }

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.Confined;

            return;
        }

        if (Inventory._inventoryActivated == false && NpcCtrl._isInteracting == false)
        {
            CameraRotation();
            CharacterRotation();
        }
    }

    private void Update()
    {
        if(UiManager._isUiActivated == false)
            TryRun();

        if (Input.GetKeyUp(KeyCode.LeftAlt))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    #region Rigid(O) Move

    private void TryRun()
    {
        if(Input.GetKey(KeyCode.LeftShift))
            Running();
        if(Input.GetKeyUp(KeyCode.LeftShift))
            RunningCancel();
    }

    private void Running()
    {
        _isRun      = true;
        _applySpeed = _playerRunSpeed;   
        
        _animCtrl.SprintAnim(_isRun);
    }

    private void RunningCancel()
    {
        _isRun      = false;
        _applySpeed = _playerSpeed;

        _animCtrl.SprintAnim(_isRun);
    }

    private void CharacterRotation()
    {
        // 좌우 캐릭터 회전
        float rotationY            = Input.GetAxisRaw("Mouse X");
        Vector3 characterRotationY = new Vector3(0f, rotationY, 0f) * _lookSensitivity;

        _rigid.MoveRotation(_rigid.rotation * Quaternion.Euler(characterRotationY));
    }

    private void RigidMove()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 moveHoriziontal = transform.right * inputX;
        Vector3 moveVertical    = transform.forward * inputZ;

        Vector3 velocity = (moveHoriziontal + moveVertical).normalized * _applySpeed;

        _rigid.MovePosition(transform.position + velocity * Time.deltaTime);

        if (_isRun == false)
        {
            if ((inputX <= 0.1f && inputX > 0) || (inputX >= -0.01 && inputX < 0))
                inputX = 0f;

            if ((inputZ <= 0.1f && inputZ > 0) || (inputZ >= -0.02 && inputZ < 0))
                inputZ = 0f;

            _animCtrl.MoveAnim(inputX, inputZ);
        }
    }

    private void CameraRotation()
    {
        // 상하 카메라 회전
        float rotationX       = Input.GetAxisRaw("Mouse Y");
        float cameraRotationX = rotationX * _lookSensitivity;

        _currentCameraRotationX -= cameraRotationX;
        _currentCameraRotationX  = Mathf.Clamp(_currentCameraRotationX, -_cameraRotationLimit, _cameraRotationLimit);

        _theCamera.transform.localEulerAngles = new Vector3(_currentCameraRotationX, 0f, 0f);
    }
    #endregion
    
}
