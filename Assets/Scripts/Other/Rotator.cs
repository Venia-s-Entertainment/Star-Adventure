using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] Vector3 axis;
    // Update is called once per frame
    void Update()
    {
        transform.eulerAngles += axis * 0.5f;
    }
}
