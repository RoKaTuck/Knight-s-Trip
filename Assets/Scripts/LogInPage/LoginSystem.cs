using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoginSystem : MonoBehaviour
{
    [SerializeField, Header("LoginSystem Attribute")]
    TMP_InputField _email;
    [SerializeField]
    TMP_InputField _password;    
    public TMP_Text _errorText;

    //public Text _outPutText;
    
    void Start()
    {
        FirebaseAuthManager.Instance._logInState += OnChangedState;
        FirebaseAuthManager.Instance.Init();
    }
    
    private void OnChangedState(bool sign)
    {
        //_errorText.text = sign ? "로그인 : " : "로그아웃 : ";
        //_errorText.text += FirbaseAuthManager.Instance._UserId;
    }

    //  회원가입
    public void Create() 
    {
        string e = _email.text;
        string p = _password.text;

        FirebaseAuthManager.Instance.Create(e, p);
    }

    //  로그인
    public void LogIn()
    {
        FirebaseAuthManager.Instance.LogIn(_email, _password, _errorText);        
    }

    // 로그아웃
    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }
}
