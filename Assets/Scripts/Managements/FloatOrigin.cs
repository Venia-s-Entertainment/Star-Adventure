using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloatOrigin : MonoBehaviour
{
    [SerializeField] float Threshold = 1000;

    [Header("Debug")]
    [SerializeField] Console con;

    void LateUpdate()
    {
        Vector3 cameraPosition = transform.position;

        if (cameraPosition.magnitude > Threshold)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                foreach (GameObject root in SceneManager.GetSceneAt(i).GetRootGameObjects())
                {
                    root.transform.position -= cameraPosition;
                }
            }

            con.Debug($"Point origin updated, new point at {cameraPosition}", "white");
        }
    }
}
