using UnityEngine;
public class SlowMo : MonoBehaviour
{
    [Range(0.1f, 2f)]
    public float modifiedScale;
    void Update()
    {
        Time.timeScale = modifiedScale;
    }
}//class
