//Adapted by Alex Coulombe @ibrews from @fariazz's script described here: https://forum.unity.com/threads/any-example-of-the-new-2019-1-xr-input-system.629824/

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using Unity.XR.Oculus;
using DilmerGamesLogger = DilmerGames.Core.Logger;

public class ButtonController : MonoBehaviour
{
    static readonly Dictionary<string, InputFeatureUsage<bool>> availableButtons = new Dictionary<string, InputFeatureUsage<bool>>
    {
        {"triggerButton", CommonUsages.triggerButton },
        {"thumbrest", OculusUsages.thumbrest },
        {"primary2DAxisClick", CommonUsages.primary2DAxisClick },
        {"primary2DAxisTouch", CommonUsages.primary2DAxisTouch },
        {"menuButton", CommonUsages.menuButton },
        {"gripButton", CommonUsages.gripButton },
        {"secondaryButton", CommonUsages.secondaryButton },
        {"secondaryTouch", CommonUsages.secondaryTouch },
        {"primaryButton", CommonUsages.primaryButton },
        {"primaryTouch", CommonUsages.primaryTouch },
    };

    public enum ButtonOption
    {
        triggerButton,
        thumbrest,
        primary2DAxisClick,
        primary2DAxisTouch,
        menuButton,
        gripButton,
        secondaryButton,
        secondaryTouch,
        primaryButton,
        primaryTouch
    };

    [Tooltip("Input device role (left or right controller)")]
    //currently will only work with one controller-- don't choose left and right in the dropdown
    public InputDeviceCharacteristics deviceCharacteristic;

    [Tooltip("Select the button")]
    public ButtonOption button;

    [Tooltip("Event when the button starts being pressed")]
    public UnityEvent OnPress;

    [Tooltip("Event when the button is released")]
    public UnityEvent OnRelease;

    // to check whether it's being pressed
    public bool IsPressed { get; private set; }

    // to obtain input devices
    List<InputDevice> inputDevices;
    bool inputValue;

    InputFeatureUsage<bool> inputFeature;

    void Awake()
    {
        // get label selected by the user
        string featureLabel = Enum.GetName(typeof(ButtonOption), button);

        // find dictionary entry
        availableButtons.TryGetValue(featureLabel, out inputFeature);

        // init list
        inputDevices = new List<InputDevice>();

        // was playing with using the code from https://docs.unity3d.com/2019.3/Documentation/ScriptReference/XR.InputDevices.GetDevicesWithCharacteristics.html instead of getting this info in Update
        /*
        InputDeviceCharacteristics leftTrackedControllerFilter = InputDeviceCharacteristics.Controller | InputDeviceCharacteristics.TrackedDevice | InputDeviceCharacteristics.Left, leftHandedControllers;

        List<InputDevice> foundControllers = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(leftTrackedControllerFilter, inputDevices);
        */
    }

    void Update()
    {
        InputDevices.GetDevicesWithCharacteristics(deviceCharacteristic, inputDevices);
        
        for (int i = 0; i < inputDevices.Count; i++)
        {
            if (inputDevices[i].TryGetFeatureValue(inputFeature,
                out inputValue) && inputValue)
            {
                // if start pressing, trigger event
                if (!IsPressed)
                {
                    IsPressed = true;
                    OnPress.Invoke();
                    DilmerGamesLogger.Instance.LogInfo($"Pressed {deviceCharacteristic} Controller {button}");
                }
            }

            // check for button release
            else if (IsPressed)
            {
                IsPressed = false;
                OnRelease.Invoke();
                DilmerGamesLogger.Instance.LogInfo($"Released {deviceCharacteristic} Controller {button}");
            }
            //was logging this to see exactly what was called -- want to try to allow for one instance of the script to allow for both controllers
            //DilmerGamesLogger.Instance.LogInfo($"Device Characteristic is {deviceCharacteristic} and Input Devices are {inputDevices}");
        }
    }
}