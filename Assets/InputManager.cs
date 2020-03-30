using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR;
using DilmerGamesLogger = DilmerGames.Core.Logger;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private XRNode xrNode = XRNode.LeftHand;

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;

    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(xrNode, devices);
        device = devices.FirstOrDefault();
    }

    void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }

    void Update()
    {
        if (!device.isValid)
        {
            GetDevice();
        }

        /* First scenario is going to get us all features
        List<InputFeatureUsage> features = new List<InputFeatureUsage>();
        device.TryGetFeatureUsages(features);

        foreach (var feature in features)
        {
            if (feature.type == typeof(bool))
            {
               // DilmerGamesLogger.Instance.LogInfo($"Feature {feature.name} type {feature.type}");
            }
        }
        */

        // capturing trigger button
        bool triggerButtonAction = false;
        if (device.TryGetFeatureValue(CommonUsages.triggerButton, out triggerButtonAction) && triggerButtonAction)
        {
            DilmerGamesLogger.Instance.LogInfo($"TriggerButton activated {triggerButtonAction}");
        }

        // capturing primary button
        bool primaryButtonValue = false;
        InputFeatureUsage<bool> primaryButtonUsage = CommonUsages.primaryButton;

        if (device.TryGetFeatureValue(primaryButtonUsage, out primaryButtonValue) && primaryButtonValue)
        {
            DilmerGamesLogger.Instance.LogInfo($"PrimaryButton activated {primaryButtonValue}");
        }

        // capturing primary 2D Axis
        Vector2 primary2DAxisValue = Vector2.zero;
        InputFeatureUsage<Vector2> primary2DAxisUsage = CommonUsages.primary2DAxis;

        if (device.TryGetFeatureValue(primary2DAxisUsage, out primary2DAxisValue) && primary2DAxisValue != Vector2.zero)
        {
            DilmerGamesLogger.Instance.LogInfo($"Primary2DAxis value {primary2DAxisValue}");
        }

        // capturing grip value
        float gripValue = 0;
        InputFeatureUsage<float> gripUsage = CommonUsages.grip;

        if (device.TryGetFeatureValue(gripUsage, out gripValue))
        {
            DilmerGamesLogger.Instance.LogInfo($"Grip value {gripValue}");
        }
    }
}
