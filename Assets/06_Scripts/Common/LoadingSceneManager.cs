using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingSceneManager : MonoBehaviour
{
    [SerializeField, Header("LoadingSceneManager Attribute")]
    private Slider _slider;
    [SerializeField]
    private string _sceneName;
    [SerializeField]
    private float _limitTime; // 프로그레스 바의 최대 대기 시간     

    private AsyncOperation _operation;

    private float _time;
    private bool _isDone = false;


    private void Start()
    {
        StopAllCoroutines();
        StartCoroutine(CRT_LoadAsynScene(_sceneName));
    }

    private void Update()
    {
        _time        += Time.deltaTime;
        _slider.value = _time / _limitTime;

        if(_time >= _limitTime)
            _operation.allowSceneActivation = true;
    }

    IEnumerator CRT_LoadAsynScene(string sceneName)
    {
        _operation = SceneManager.LoadSceneAsync(sceneName);
        _operation.allowSceneActivation = false;

        if(_isDone == false)
        {
            _isDone = true;

            while(_operation.progress < 0.9f)
            {
                _slider.value = _operation.progress;                
                yield return true;
            }
        }
    }   
}
