using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUi : MonoBehaviour
{    
    private CanvasGroup _cg;
    public float _fadeTime = 1f; // 페이드 타임 
    private float _accumTime = 0f;
    private Coroutine _fadeCor;

    // 필요한 컴포넌트
    [SerializeField]
    private GameObject _content;

    public void OnRestartBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void OnGoTownBtn(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public void StartFadeIn() // 호출 함수 Fade In을 시작
    {
        _cg = GetComponent<CanvasGroup>();
        _content.SetActive(true);

        if (_fadeCor != null)
        {
            StopAllCoroutines();
            _fadeCor = null;
        }
        _fadeCor = StartCoroutine(FadeIn());
    }

    private IEnumerator FadeIn() // 코루틴을 통해 페이드 인 시간 조절
    {
        yield return new WaitForSeconds(0.2f);
        _accumTime = 0f;
        while (_accumTime < _fadeTime)
        {
            _cg.alpha = Mathf.Lerp(0f, 1f, _accumTime / _fadeTime);
            yield return 0;
            _accumTime += Time.deltaTime;
        }
        _cg.alpha = 1f;        
    }   
}
