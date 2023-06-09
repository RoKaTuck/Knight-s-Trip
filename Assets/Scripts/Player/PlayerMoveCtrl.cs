using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveCtrl : MonoBehaviour
{
    [SerializeField, Header("Player Attriubte")]
    float _playerSpeed = 5f;
    [SerializeField]
    float _playerRotateSpeed = 10f;

    public bool _isMove = true;

    private PlayerAnimCtrl _animCtrl;

    public bool _Move { set { _isMove = value; } }

    private void Awake()
    {
        _animCtrl = GetComponent<PlayerAnimCtrl>();
    }
    

    // Update is called once per frame
    private void Update()
    {
        if (_isMove == false)
            return;

        PlayerMove();   
    }

    void PlayerMove()
    {        
        float inputX = Input.GetAxis("Horizontal");
        float inputZ = Input.GetAxis("Vertical");

        Vector3 moveDir = new Vector3(inputX, 0f, inputZ);

        if(!(moveDir.x == 0 && moveDir.z == 0))
        {
            _isMove = true;
            transform.position += moveDir * _playerSpeed * Time.deltaTime;
            transform.rotation = Quaternion.Lerp(transform.rotation,
                                 Quaternion.LookRotation(moveDir), _playerRotateSpeed * Time.deltaTime);

            if ((moveDir.x <= 0.1f && moveDir.x > 0) || (moveDir.x >= -0.01 && moveDir.x < 0))
                moveDir.x = 0f;

            if ((moveDir.z <= 0.1f && moveDir.z > 0) || (moveDir.z >= -0.02 && moveDir.z < 0))
                moveDir.z = 0f;

            _animCtrl.MoveAnim(moveDir.x, moveDir.z);
        }        
    }
}
