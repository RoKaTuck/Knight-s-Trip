using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathUi : MonoBehaviour
{    
    private CanvasGroup _cg;
    public float _fadeTime = 1f; // ���̵� Ÿ�� 
    private float _accumTime = 0f;
    private Coroutine _fadeCor;

    // �ʿ��� ������Ʈ
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

    public void StartFadeIn() // ȣ�� �Լ� Fade In�� ����
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

    private IEnumerator FadeIn() // �ڷ�ƾ�� ���� ���̵� �� �ð� ����
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
