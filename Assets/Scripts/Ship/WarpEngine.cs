using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WarpEngine : MonoBehaviour
{
    [SerializeField] WarpModule warpModule;
    [SerializeField] Text warpText;
    [SerializeField] ParticleSystem warpEffect;
    [SerializeField] float maxWarpSpeed = 2500;
    [SerializeField] float distanceTreshold = 1000;

    private Transform target;
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
                warpText.text = $"Distance to target: {Mathf.RoundToInt(Vector3.Distance(transform.position, target.position) - target.GetComponent<CelestialInfo>().Radius)}u";
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

            warpText.text = $"Prepairing... {Mathf.RoundToInt(timer)}s";
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                warpEffect.Play();
                warpModule.jumpRemains--;
            }

            yield return new WaitForSeconds(0);
        }
    }
    public void SetTarget(Transform target)
    {
        if (!warpStarted)
        {
            this.target = target;

            if (warpModule.hasItem)
                warpText.text = $"<color=cyan>Target selected! Press B key to warp</color>";
        }
    }
    public void StartWarp()
    {
        if (warpModule.hasItem)
        {
            if (PlayerStats.celestial != null)
            {
                warpText.text = $"<color=red>Too close to planet!</color>";
                return;
            }
            if (warpStarted)
            {
                EndWarp();
                return;
            }
            if (warpModule.jumpRemains == 0)
            {
                warpText.text = $"<color=red>Warp Module is out of energy!</color>";
                return;
            }

            if (target != null)
            {
                enabled = true;
                warpStarted = true;
                timer = 5;
                StartCoroutine(RotateToTarget());
            }
            else
            {
                warpText.text = $"<color=red>Target not selected!</color>";
            }
        }
    }
    public void EndWarp()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;

        warpText.text = null;
        warpEffect.Stop();
        warpStarted = false;
        enabled = false;
    }
}
