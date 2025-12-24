using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    public float updateInterval = 0.5f;

    float accum = 0;
    int frames = 0;
    float timeleft;

    float fps;

    void Start()
    {
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;

        if (timeleft <= 0.0)
        {
            fps = accum / frames;
            timeleft = updateInterval;
            accum = 0.0f;
            frames = 0;
        }
    }

    void OnGUI()
    {
        // Bottom-left, safe area aware
        float padding = 10f;

        Rect safe = Screen.safeArea;

        float labelWidth = 180f;
        float labelHeight = 40f;

        float x = safe.xMin + padding;
        float y = safe.yMax - labelHeight - padding;

        GUI.color = Color.white;
        GUI.Label(new Rect(x, y, labelWidth, labelHeight), $"FPS: {fps:F1}");
    }

}