using UnityEngine;
using TMPro;

public class FpsCounter : MonoBehaviour
{
    private TMP_Text text;
    private float timer;

    [SerializeField] private float refreshRate;
    private void Start()
    {
        text = GetComponent<TMP_Text>();
    }

    void Update()
    {
        if (Time.unscaledTime > timer)
        {
            int fps = (int)(1f / Time.unscaledDeltaTime);
            text.text = "FPS: " + fps;
            timer = Time.unscaledTime + refreshRate;
        }
    }
}
