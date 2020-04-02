//Adapted by Alex Coulombe @ibrews from @icave_user's script described here: https://forum.unity.com/threads/any-example-of-the-new-2019-1-xr-input-system.629824/

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;


public class ButtonEventSystem : MonoBehaviour
{
    public GameObject LeftAnchor;
    public GameObject RightAnchor;

    private HandController _leftController;
    private HandController _rightController;

    private InputDevice _leftDevice;
    private InputDevice _rightDevice;

    // Start is called before the first frame update
    void Start()
    {
        //base.Start();

        SetDevices();

        //Initialize Hands
        _leftController = LeftAnchor.AddComponent<HandController>();
        _rightController = RightAnchor.AddComponent<HandController>();

    }

    // Update is called once per frame
    void Update()
    {
        //base.Update();

        //Set Tracked Devices
        SetDevicePosAndRot(XRNode.LeftHand, LeftAnchor);
        SetDevicePosAndRot(XRNode.RightHand, RightAnchor);

        //Set Buttons
        UpdateButtonState(_leftDevice, CommonUsages.gripButton, _leftController.GripEvent);
        UpdateButtonState(_rightDevice, CommonUsages.gripButton, _rightController.GripEvent);

        UpdateButtonState(_leftDevice, CommonUsages.primary2DAxisClick, _leftController.ClickEvent);
        UpdateButtonState(_rightDevice, CommonUsages.primary2DAxisClick, _rightController.ClickEvent);

        UpdateButtonState(_leftDevice, CommonUsages.triggerButton, _leftController.TriggerEvent);
        UpdateButtonState(_rightDevice, CommonUsages.triggerButton, _rightController.TriggerEvent);

        UpdateButtonState(_leftDevice, CommonUsages.menuButton, _leftController.MenuEvent);
        UpdateButtonState(_rightDevice, CommonUsages.menuButton, _rightController.MenuEvent);
    }

    private static void SetDevicePosAndRot(XRNode trackedDevice, GameObject anchor)
    {
        anchor.transform.localPosition = InputTracking.GetLocalPosition(trackedDevice);
        anchor.transform.localRotation = InputTracking.GetLocalRotation(trackedDevice);
    }

    private static InputDevice GetCurrentDevice(XRNode node)
    {
        var device = new InputDevice();
        var devices = new List<InputDevice>();
        InputDevices.GetDevicesAtXRNode(node,
            devices);
        if (devices.Count == 1)
        {
            device = devices[0];
            //Debug.Log($"Device name '{device.name}' with role '{device.role.ToString()}'");
        }
        else if (devices.Count > 1)
        {
            Debug.Log($"Found more than one '{device.role.ToString()}'!");
            device = devices[0];
        }

        return device;
    }

    private void UpdateButtonState(InputDevice device, InputFeatureUsage<bool> button,
        ButtonEvent buttonPressEvent)
    {
        bool tempState;
        bool invalidDeviceFound = false;
        bool buttonState = false;

        tempState = device.isValid // the device is still valid
                    && device.TryGetFeatureValue(button, out buttonState) // did get a value
                    && buttonState; // the value we got

        if (!device.isValid)
            invalidDeviceFound = true;

        if (tempState != buttonPressEvent.Value) // Button state changed since last frame
        {
            buttonPressEvent.Invoke(tempState);
            buttonPressEvent.Value = tempState;
        }

        if (invalidDeviceFound) // refresh device lists
            SetDevices();
    }

    private void SetDevices()
    {
        //Set Controller Devices
        _leftDevice = GetCurrentDevice(XRNode.LeftHand);
        _rightDevice = GetCurrentDevice(XRNode.RightHand);
    }

    private void ShowCurrentlyAvailableXRDevices()
    {
        var inputDevices = new List<InputDevice>();
        UnityEngine.XR.InputDevices.GetDevices(inputDevices);
        foreach (var device in inputDevices)
        {
            Debug.Log($"Device found with name '{device.name}' and role '{device.role.ToString()}'");
        }
    }
}
