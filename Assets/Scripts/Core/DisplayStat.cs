using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.Profiling;

public class DisplayStat : MonoBehaviour
{
    public TextMeshProUGUI fpsText; // Kéo Text UI vào Inspector
    private float deltaTime = 0.0f;
    private float ramUsageMB = 0.0f;
    private float updateInterval = 0.5f;
    private float timeSinceUpdate = 0.0f;

    void Update()
    {
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
        timeSinceUpdate += Time.unscaledDeltaTime;

        if (timeSinceUpdate >= updateInterval)
        {
            float fps = 1.0f / deltaTime;

            //Ram
            //long ramUsageBytes = Profiler.GetTotalAllocatedMemoryLong();
            //ramUsageMB = ramUsageBytes / (1024f * 1024f);

            //fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString() + "\nRAM: " + ramUsageMB.ToString("F2") + " MB";
            fpsText.text = "FPS: " + Mathf.Ceil(fps).ToString();
            timeSinceUpdate = 0.0f;
        }
    }
}
