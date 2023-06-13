using System;
using UnityEngine;
using Firebase.Auth;
using Firebase.Extensions;
using System.Threading.Tasks;

public class FirebaseAuthManager
{
    private static FirebaseAuthManager instance = null;

    public static FirebaseAuthManager Instance
    {
        get
        {
            if(instance == null)
                instance = new FirebaseAuthManager();
            return instance;
        }
    }    

    private FirebaseAuth _auth;
    private FirebaseUser _user;

    public string _UserId => _user.UserId;

    public Action<bool> _logInState;

    private bool _isLogin;


    public void Init()
    {
        _auth = FirebaseAuth.DefaultInstance;

        if(_auth.CurrentUser != null)
        {
            LogOut();
        }
        
        _auth.StateChanged += OnChanged;
    }

    private void OnChanged(object sender, EventArgs e)
    {
        if(_auth.CurrentUser != _user)
        {
            bool signed = (_auth.CurrentUser != _user && _auth.CurrentUser != null);

            if(!signed && _user != null)
            {
                Debug.Log("�α׾ƿ�");
                _logInState?.Invoke(false);
            }

            _user = _auth.CurrentUser;

            if(signed)
            {
                Debug.Log("�α���");
                _logInState?.Invoke(true);
                
            }          
        }
    }

    public void Create(string email, string password)
    {        
        _auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("ȸ������ ���");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("ȸ������ ����");
                return;
            }

            Debug.LogError("ȸ������ �Ϸ�");
        });        
    }

    public void LogIn(TMPro.TMP_InputField email, TMPro.TMP_InputField password, TMPro.TMP_Text errorText)
    {        
        
        _auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {                
                Debug.LogError("�α��� ���");
                return;
            }
            if (task.IsFaulted)
            {
                errorText.text = "�߸��� �̸��� Ȥ�� �н����带 �Է��ϼ̽��ϴ�.";
                email.text    = string.Empty;
                password.text = string.Empty;
                Debug.LogError("�α��� ����");
                return;
            }
            
            Debug.Log("�α��� �Ϸ�");
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync("1_SelectPage");
            return;
        }); 
        
    }

    public bool LoginCheck(bool isLogin)
    {        
        return _isLogin;
    }

    public void LogOut()
    {
        _auth.SignOut();
        Debug.Log("�α׾ƿ�");
    }
}
