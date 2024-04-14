using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class StarSystemManager : MonoBehaviour
{
    [Header("Management")]
    [SerializeField] Light sun;
    [SerializeField] Transform star;
    [SerializeField] Color color;
    [SerializeField] GameObject[] celestials;

    [Header("Events")]
    [SerializeField] UnityEvent OnExitingSystem;
    [SerializeField] UnityEvent OnEnteringSystem;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject celestial in celestials)
            {
                celestial.SetActive(true);
            }

            sun.transform.position = transform.position;
            sun.transform.parent = star;
            sun.gameObject.SetActive(true);
            sun.color = color;
            OnEnteringSystem.Invoke();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (GameObject celestial in celestials)
            {
                celestial.SetActive(false);
            }

            sun.transform.parent = null;
            sun.gameObject.SetActive(false);
            OnExitingSystem.Invoke();
        }
    }
}
