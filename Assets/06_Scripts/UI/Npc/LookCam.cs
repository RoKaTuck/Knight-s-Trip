using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookCam : MonoBehaviour
{
    public GameObject _cam;

    private void Update()
    {
        transform.rotation = _cam.transform.rotation;
    }
}
