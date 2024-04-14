using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorController : MonoBehaviour
{
    [SerializeField] float threshold = 25;
    [SerializeField] Transform player;
    private Animator a;
    void Start()
    {
        a = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        a.enabled = Vector3.Distance(player.position, transform.position) <= threshold;
    }
}
