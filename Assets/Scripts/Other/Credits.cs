using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] float speed = 1;
    // Update is called once per frame
    IEnumerator MoveCredits()
    {
        while (true)
        {
            transform.position += Vector3.up * speed * Time.deltaTime;

            yield return new WaitForSeconds(0);
        }
    }
    public void StarCredits()
    {
        StartCoroutine(MoveCredits());
    }
    public void EndCredits()
    {
        StopAllCoroutines();

        transform.localPosition = Vector3.zero;
    }
}
