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
        //_errorText.text = sign ? "�α��� : " : "�α׾ƿ� : ";
        //_errorText.text += FirbaseAuthManager.Instance._UserId;
    }

    //  ȸ������
    public void Create() 
    {
        string e = _email.text;
        string p = _password.text;

        FirebaseAuthManager.Instance.Create(e, p);
    }

    //  �α���
    public void LogIn()
    {
        FirebaseAuthManager.Instance.LogIn(_email, _password, _errorText);        
    }

    // �α׾ƿ�
    public void LogOut()
    {
        FirebaseAuthManager.Instance.LogOut();
    }
}
