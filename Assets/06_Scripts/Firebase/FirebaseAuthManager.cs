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
                Debug.Log("로그아웃");
                _logInState?.Invoke(false);
            }

            _user = _auth.CurrentUser;

            if(signed)
            {
                Debug.Log("로그인");
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
                Debug.LogError("회원가입 취소");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("회원가입 실패");
                return;
            }

            Debug.LogError("회원가입 완료");
        });        
    }

    public void LogIn(TMPro.TMP_InputField email, TMPro.TMP_InputField password, TMPro.TMP_Text errorText)
    {        
        
        _auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWithOnMainThread(task =>
        {
            if (task.IsCanceled)
            {                
                Debug.LogError("로그인 취소");
                return;
            }
            if (task.IsFaulted)
            {
                errorText.text = "잘못된 이메일 혹은 패스워드를 입력하셨습니다.";
                email.text    = string.Empty;
                password.text = string.Empty;
                Debug.LogError("로그인 실패");
                return;
            }
            
            Debug.Log("로그인 완료");
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
        Debug.Log("로그아웃");
    }
}
