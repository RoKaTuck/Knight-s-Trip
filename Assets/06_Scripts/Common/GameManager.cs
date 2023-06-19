using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int _gold;
    public int _Gold { get { return _gold; } set { _gold = value; } }

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
                instance = new GameManager();
            return instance;
        }
    }

    private void Awake()
    {
        if (null == instance)
        {            
            instance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {            
            Destroy(this.gameObject);
        }
    }
    
}
