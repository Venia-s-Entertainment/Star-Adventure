using UnityEngine;

public class StarfieldController : MonoBehaviour
{
    private void Start()
    {
        SetActive();
    }
    // Update is called once per frame
    void Update()
    { 
        transform.rotation = Quaternion.identity;
    }
    public void SetActive()
    {
        gameObject.SetActive(PlayerPrefs.GetInt("FirstRun") != 0 ? System.Convert.ToBoolean(PlayerPrefs.GetInt("Starfield")) : true);
    }
}
