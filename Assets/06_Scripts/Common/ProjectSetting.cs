using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ProjectSetting : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (!Directory.Exists("Assets/Prefabs"))
            Directory.CreateDirectory("Assets/Prefabs");

        if (!Directory.Exists("Assets/Scripts"))
            Directory.CreateDirectory("Assets/Scripts");

        if (!Directory.Exists("Assets/Sprites"))
            Directory.CreateDirectory("Assets/Sprites");

        if (!Directory.Exists("Assets/Animations"))
            Directory.CreateDirectory("Assets/Animations");

        if (!Directory.Exists("Assets/Physics Materials"))
            Directory.CreateDirectory("Assets/Physics Materials");

        if (!Directory.Exists("Assets/Fonts"))
            Directory.CreateDirectory("Assets/Fonts");

        if (!Directory.Exists("Assets/Audio"))
            Directory.CreateDirectory("Assets/Audio");

        if (!Directory.Exists("Assets/Resources"))
            Directory.CreateDirectory("Assets/Resources");

        if (!Directory.Exists("Assets/Plugins"))
            Directory.CreateDirectory("Assets/Plugins");
    }
}
