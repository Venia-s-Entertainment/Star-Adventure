using UnityEngine;

public class OrbitDrawer : MonoBehaviour
{
    [SerializeField] MapController map;
    private LineRenderer lineRenderer;
    public int segments = 50;
    public float radius = 5f;
    public float maxWidth = 1500;
    public float minWidth = 100;

    void Start()
    {
        UpdateSegments();
    }
    private void UpdateSegments()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = segments + 1;
        lineRenderer.useWorldSpace = false;

        float angle = 0f;
        for (int i = 0; i < segments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle) * radius;
            float y = Mathf.Cos(Mathf.Deg2Rad * angle) * radius;

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));

            angle += 360f / segments;
        }
    }
    private void Update()
    {
        float width = map.zoomStrength / 10;
        width = Mathf.Clamp(width, minWidth, maxWidth);

        lineRenderer.startWidth = width;
        lineRenderer.endWidth = width;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            UpdateSegments();
        }
    }
}


