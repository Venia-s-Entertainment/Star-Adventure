using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarpEngine_Test : MonoBehaviour
{
    [SerializeField] float maxWarpSpeed = 2500;
    [SerializeField] float distanceTreshold = 1000;

    public Transform target;
    public bool warpStarted;
    private float timer = 5;

    void FixedUpdate()
    {
        if (timer <= 0)
        {
            if (!(Vector3.Distance(transform.position, target.position) - target.GetComponent<CelestialInfo>().Radius < distanceTreshold))
            {
                transform.LookAt(target);
                GetComponent<Rigidbody>().velocity = transform.forward * maxWarpSpeed;
            }
            else
            {
                EndWarp();
            }
        }
    }
    IEnumerator RotateToTarget()
    {
        while (timer > 0 && warpStarted)
        {
            Vector3 relativePos = target.position - transform.position;

            var rotation = Quaternion.LookRotation(relativePos);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 1.5f * Time.deltaTime);

            timer -= Time.deltaTime;

            yield return new WaitForSeconds(0);
        }
    }
    public void SetTarget(Transform target)
    {
        if (!warpStarted)
        {
            this.target = target;
        }
    }
    public void StartWarp()
    {
        if (true)
        {
            if (PlayerStats.celestial != null)
            {
                return;
            }
            if (warpStarted)
            {
                EndWarp();
                return;
            }

            if (target != null)
            {
                enabled = true;
                warpStarted = true;
                timer = 5;
                StartCoroutine(RotateToTarget());
            }
        }
    }
    public void EndWarp()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        warpStarted = false;
        enabled = false;
    }
}
