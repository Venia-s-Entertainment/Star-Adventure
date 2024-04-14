using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunObserver : MonoBehaviour
{
    [SerializeField] Transform sun;
    // Update is called once per frame
    void Update()
    {
        transform.LookAt(sun);
    }
}
