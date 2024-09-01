using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Analytics; // Unity Analytics namespace
using Unity.Services.Analytics;
using Unity.Services.Core;
using Unity.Services.Core.Environments; // Required for Dictionary

public class ButtonAnalyticsTracker : MonoBehaviour
{
    public Button myButton; // Reference to the UI Button
    public string eventName = "button_click"; // Default event name


    async void Start()
    {
        try
        {
            // Specify the environment options
            var options = new InitializationOptions();
            options.SetEnvironmentName("production");
            await UnityServices.InitializeAsync(options);
            Debug.Log("Analytics initialized successfully.");
            AnalyticsService.Instance.StartDataCollection();
        }
        catch (Exception ex)
        {
            Debug.LogError($"Failed to initialize analytics: {ex.Message}");
        }
    }

    void Awake()
    {
        // Ensure the button is assigned and add a click listener
        if (myButton != null)
        {
            myButton.onClick.AddListener(() => OnButtonClick(eventName));
        }
    }

    void OnButtonClick(string eventName)
    {
        if (UnityServices.State == ServicesInitializationState.Initialized)
        {
            try
            {
                CustomEvent e = new CustomEvent("userpressbutton");
                e.Add("whenPress", "Im pressing the click me on mobile class!!!");
                AnalyticsService.Instance.RecordEvent(e);
                AnalyticsService.Instance.Flush();
            }
            catch (Exception ex)
            {
                Debug.LogError($"Failed to send analytics event: {ex.Message}");
            }
        }
        else
        {
            Debug.Log("error initialized!");
        }

    }
}