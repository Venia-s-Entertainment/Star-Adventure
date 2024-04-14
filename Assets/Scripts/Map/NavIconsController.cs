using UnityEngine;

public class NavIconsController : MonoBehaviour
{
    [SerializeField] Camera cam;
    public Transform Celestial;
    private Vector2 screenPos;

    // Update is called once per frame
    void Update()
    {
        float minX = gameObject.GetComponent<RectTransform>().rect.width / 2;
        float maxX = Screen.width - minX;

        float minY = gameObject.GetComponent<RectTransform>().rect.height / 2;
        float maxY = Screen.height - minY;

        screenPos = cam.WorldToScreenPoint(Celestial.position);
        
        if (Vector3.Dot((Celestial.position - cam.transform.position), cam.transform.forward) < 0)
        {
            if (screenPos.x < Screen.width / 2)
            {
                screenPos.x = maxX;
            }
            else
            {
                screenPos.y = minY;
            }
        }

        screenPos.x = Mathf.Clamp(screenPos.x, minX, maxX);
        screenPos.y = Mathf.Clamp(screenPos.y, minY, maxY);

        transform.position = screenPos;
    }
    public void ShowTarget(Transform target)
    {
        gameObject.SetActive(true);
        Celestial = target;
    }
    public void HideTarget()
    {
        gameObject.SetActive(false);
    }
}
