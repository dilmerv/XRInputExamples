//Created by Alex Coulombe @ibrews based on all the other scripts out there trying to do something similar
//Go to https://docs.unity3d.com/2019.3/Documentation/Manual/xr_input.html which devices support which input mappings

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.InteractionSubsystems;
using DilmerGamesLogger = DilmerGames.Core.Logger;

public class XRControllerInput : MonoBehaviour
{
    [SerializeField]
    [Tooltip("Left or Right Hand. This script does not need to be on the actual controller object.")]
    private XRNode XRController = XRNode.LeftHand;

    private List<InputDevice> devices = new List<InputDevice>();

    private InputDevice device;

    [Tooltip("When enbaled, disables controller input.")]
    public bool keyboardDebug = false;
    [Tooltip("How much should each keyboard press of float values change by?")]
    public float debugAxisValueIncrement = 0.1f;

    [Tooltip("Minimum value that needs to be read of the axes to register. If you're getting input without touching anything, increase this value")]
    public float minAxisValue = 0.15f;

    // private versions of the controller input variables before passed to public versions
    bool _triggerButton = false;
    float _triggerValue = 0.0f;
    bool _gripButton = false;
    float _gripValue = 0.0f;
    bool _primary2DAxisButton = false;
    Vector2 _primary2DAxisValue = Vector2.zero;
    bool _secondary2DAxisButton = false;
    Vector2 _secondary2DAxisValue = Vector2.zero;
    bool _primaryButton = false;
    bool _secondaryButton = false;
    bool _menuButton = false;

    // public controller input variables
    public bool triggerButton = false;
    [Range(0, 1)]
    public float triggerValue = 0.0f;
    public bool gripButton = false;
    [Range(0, 1)]
    public float gripValue = 0.0f;
    public bool primary2DAxisButton = false;
    [HideInInspector]
    public Vector2 primary2DAxisValue = Vector2.zero;
    [Range(-1, 1)]
    public float primary2DAxisXValue = 0.0f;
    [Range(-1, 1)]
    public float primary2DAxisYValue = 0.0f;
    public bool secondary2DAxisButton = false;
    [HideInInspector]
    public Vector2 secondary2DAxisValue = Vector2.zero;
    [Range(-1, 1)]
    public float secondary2DAxisXValue = 0.0f;
    [Range(-1, 1)]
    public float secondary2DAxisYValue = 0.0f;
    public bool primaryButton = false;
    public bool secondaryButton = false;
    public bool menuButton = false;

    // Events
    [Tooltip("Event when the Trigger starts being pressed")]
    public UnityEvent OnTriggerPress;
    [Tooltip("Event when the Trigger is released")]
    public UnityEvent OnTriggerRelease;

    [Tooltip("Event when the Grip starts being pressed")]
    public UnityEvent OnGripPress;
    [Tooltip("Event when the Grip is released")]
    public UnityEvent OnGripRelease;

    [Tooltip("Event when the Primary 2D Axis Click starts being pressed")]
    public UnityEvent OnPrimary2DAxisPress;
    [Tooltip("Event when the Primary 2D Axis Click is released")]
    public UnityEvent OnPrimary2DAxisRelease;

    [Tooltip("Event when the Secondary 2D Axis Click starts being pressed")]
    public UnityEvent OnSecondary2DAxisPress;
    [Tooltip("Event when the Secondary 2D Axis Click is released")]
    public UnityEvent OnSecondary2DAxisRelease;

    [Tooltip("Event when the Primary Button starts being pressed")]
    public UnityEvent OnPrimaryButtonPress;
    [Tooltip("Event when the Primary Button is released")]
    public UnityEvent OnPrimaryButtonRelease;

    [Tooltip("Event when the Secondary Button starts being pressed")]
    public UnityEvent OnSecondaryButtonPress;
    [Tooltip("Event when the Secondary Button is released")]
    public UnityEvent OnSecondaryButtonRelease;

    [Tooltip("Event when the Menu Button starts being pressed")]
    public UnityEvent OnMenuButtonPress;
    [Tooltip("Event when the Menu Button is released")]
    public UnityEvent OnMenuButtonRelease;


    void GetDevice()
    {
        InputDevices.GetDevicesAtXRNode(XRController, devices);
        device = devices.FirstOrDefault();
    }

    void OnEnable()
    {
        if (!device.isValid)
        {
            GetDevice();
        }
    }

    // Events that are all Press/Release and can have their actions targeted directly in the editor (or here if you want) -- look for ///
    private bool TriggerButtonAction
    {
        get { return triggerButton; }
        set
        {
            if (value == triggerButton) return;
            triggerButton = value;

            ///do a thing in the editor, or here
            if (value == true) OnTriggerPress?.Invoke();
            else OnTriggerRelease?.Invoke();

            Debug.Log($"Trigger Press {triggerButton} on {XRController}");
            DilmerGamesLogger.Instance.LogInfo($"Trigger Press {triggerButton} on {XRController}");
        }
    }

    private bool GripButtonAction
    {
        get { return gripButton; }
        set
        {
            if (value == gripButton) return;
            gripButton = value;

            ///do a thing in the editor, or here
            if (value == true) OnGripPress?.Invoke();
            else OnGripRelease?.Invoke();

            Debug.Log($"Grip Press {gripButton} on {XRController}");
            DilmerGamesLogger.Instance.LogInfo($"Grip Press {gripButton} on {XRController}");
        }
    }

    private bool Primary2DAxisButtonAction
    {
        get { return primary2DAxisButton; }
        set
        {
            if (value == primary2DAxisButton) return;
            primary2DAxisButton = value;

            ///do a thing in the editor, or here
            if (value == true) OnPrimary2DAxisPress?.Invoke();
            else OnPrimary2DAxisRelease?.Invoke();

            Debug.Log($"Primary 2D Axis Button Press {primary2DAxisButton} on {XRController}");
            DilmerGamesLogger.Instance.LogInfo($"Primary 2D Axis Button Press {primary2DAxisButton} on {XRController}");
        }
    }

    private bool Secondary2DAxisButtonAction
    {
        get { return secondary2DAxisButton; }
        set
        {
            if (value == secondary2DAxisButton) return;
            secondary2DAxisButton = value;

            ///do a thing in the editor, or here
            if (value == true) OnSecondary2DAxisPress?.Invoke();
            else OnSecondary2DAxisRelease?.Invoke();

            Debug.Log($"Secondary 2D Axis Button Press {secondary2DAxisButton} on {XRController}");
            DilmerGamesLogger.Instance.LogInfo($"Secondary 2D Axis Button Press {secondary2DAxisButton} on {XRController}");
        }
    }

    private bool PrimaryButtonAction
    {
        get { return primaryButton; }
        set
        {
            if (value == primaryButton) return;
            primaryButton = value;

            ///do a thing in the editor, or here
            if (value == true) OnPrimaryButtonPress?.Invoke();
            else OnPrimaryButtonRelease?.Invoke();

            Debug.Log($"Primary Button Press {primaryButton} on {XRController}");
            DilmerGamesLogger.Instance.LogInfo($"Primary Button Press {primaryButton} on {XRController}");
        }
    }

    private bool SecondaryButtonAction
    {
        get { return secondaryButton; }
        set
        {
            if (value == secondaryButton) return;
            secondaryButton = value;

            ///do a thing in the editor, or here
            if (value == true) OnSecondaryButtonPress?.Invoke();
            else OnSecondaryButtonRelease?.Invoke();

            Debug.Log($"Secondary Button Press {secondaryButton} on {XRController}");
            DilmerGamesLogger.Instance.LogInfo($"Secondary Button Press {secondaryButton} on {XRController}");
        }
    }

    private bool MenuButtonAction
    {
        get { return menuButton; }
        set
        {
            if (value == menuButton) return;
            menuButton = value;

            ///do a thing in the editor, or here
            if (value == true) OnMenuButtonPress?.Invoke();
            else OnMenuButtonRelease?.Invoke();

            Debug.Log($"Menu Button Press {menuButton} on {XRController}");
            DilmerGamesLogger.Instance.LogInfo($"Menu Button Press {menuButton} on {XRController}");
        }
    }

    // Events that don't invoke events targetable from the editor because they're all ranged float values. Fill in your own actions at ///
    private float TriggerValueAction
    {
        get { return triggerValue; }
        set
        {
            if (value == triggerValue) return;
            triggerValue = value;

            ///Do something with the value

            Debug.Log($"Trigger Value {(Mathf.RoundToInt(triggerValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            DilmerGamesLogger.Instance.LogInfo($"Trigger Value {triggerValue} on {XRController}");
        }
    }

    private float GripValueAction
    {
        get { return gripValue; }
        set
        {
            if (value == gripValue) return;
            gripValue = value;

            ///Do something with the value

            Debug.Log($"Trigger Value {(Mathf.RoundToInt(gripValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            DilmerGamesLogger.Instance.LogInfo($"Grip Value {Mathf.RoundToInt(gripValue * 100) / 100} on {XRController}");
        }
    }

    private Vector2 Primary2DAxisValueAction
    {
        get { return primary2DAxisValue;  }
        set
        {
            if (value == primary2DAxisValue) return;
            primary2DAxisValue = value;
            primary2DAxisXValue = primary2DAxisValue.x;
            primary2DAxisYValue = primary2DAxisValue.y;

            ///Do something with .x or .y values or both

            Debug.Log($"Primary2DAxis X value {(Mathf.RoundToInt(primary2DAxisXValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            Debug.Log($"Primary2DAxis Y value {(Mathf.RoundToInt(primary2DAxisYValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            DilmerGamesLogger.Instance.LogInfo($"Primary2DAxis value {primary2DAxisValue} on {XRController}");
        }
    }

    private Vector2 Secondary2DAxisValueAction
    {
        get { return secondary2DAxisValue; }
        set
        {
            if (value == secondary2DAxisValue) return;
            secondary2DAxisValue = value;
            secondary2DAxisXValue = secondary2DAxisValue.x;
            secondary2DAxisYValue = secondary2DAxisValue.y;
            
            ///Do something with .x or .y values or both

            Debug.Log($"Secondary2DAxis X value {(Mathf.RoundToInt(secondary2DAxisXValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            Debug.Log($"Secondary2DAxis Y value {(Mathf.RoundToInt(secondary2DAxisYValue * 10f) / 10f)} on {XRController}"); //helps to keep values collapsed in console log
            DilmerGamesLogger.Instance.LogInfo($"Secondary2DAxis value {secondary2DAxisValue} on {XRController}");
        }
    }

    void Update()
    {
        // Recenter XR player location with R
        if (Input.GetKeyDown(KeyCode.R))
        {
            UnityEngine.XR.InputTracking.Recenter();
            //XRInputSubsystem.TryRecenter();
        }

        // Keyboard Debug. Disables controller input. For clarity, recommend only turning on with left or right hand script at a time.
        if (keyboardDebug)
        {
            // trigger
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //you could also use GetKeyDown and GetKeyUp for Press (true) and Release (false)
                TriggerButtonAction = !TriggerButtonAction;
            }
            if (Input.GetKeyDown(KeyCode.PageUp) && triggerValue < 1)
            {
                TriggerValueAction += debugAxisValueIncrement;
            }
            if (Input.GetKeyDown(KeyCode.PageDown) && triggerValue > 0)
            {
                TriggerValueAction -= debugAxisValueIncrement;
            }

            // grip
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                GripButtonAction = !GripButtonAction;
            }
            if (Input.GetKeyDown(KeyCode.KeypadPlus) && gripValue < 1)
            {
                GripValueAction += debugAxisValueIncrement;
            }
            if (Input.GetKeyDown(KeyCode.KeypadMinus) && gripValue > 0)
            {
                GripValueAction -= debugAxisValueIncrement;
            }

            // primary 2D axis
            if (Input.GetKeyDown(KeyCode.Return))
            {
                Primary2DAxisButtonAction = !Primary2DAxisButtonAction;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow) && primary2DAxisValue.y < 1)
            {
                _primary2DAxisValue += Vector2.up * debugAxisValueIncrement;
                Primary2DAxisValueAction = _primary2DAxisValue;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow) && primary2DAxisValue.y > -1)
            {
                _primary2DAxisValue -= Vector2.up * debugAxisValueIncrement;
                Primary2DAxisValueAction = _primary2DAxisValue;
            }
            if (Input.GetKeyDown(KeyCode.RightArrow) && primary2DAxisValue.x < 1)
            {
                _primary2DAxisValue += Vector2.right * debugAxisValueIncrement;
                Primary2DAxisValueAction = _primary2DAxisValue;
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && primary2DAxisValue.x > -1)
            {
                _primary2DAxisValue -= Vector2.right * debugAxisValueIncrement;
                Primary2DAxisValueAction = _primary2DAxisValue;
            }

            // secondary 2D axis
            if (Input.GetKeyDown(KeyCode.Keypad5))
            {
                Secondary2DAxisButtonAction = !Secondary2DAxisButtonAction;
            }
            if (Input.GetKeyDown(KeyCode.Keypad8) && secondary2DAxisValue.y < 1)
            {
                _secondary2DAxisValue += Vector2.up * debugAxisValueIncrement;
                Secondary2DAxisValueAction = _secondary2DAxisValue;
            }
            if (Input.GetKeyDown(KeyCode.Keypad2) && secondary2DAxisValue.y > -1)
            {
                _secondary2DAxisValue -= Vector2.up * debugAxisValueIncrement;
                Secondary2DAxisValueAction = _secondary2DAxisValue;
            }
            if (Input.GetKeyDown(KeyCode.Keypad6) && secondary2DAxisValue.x < 1)
            {
                _secondary2DAxisValue += Vector2.right * debugAxisValueIncrement;
                Secondary2DAxisValueAction = _secondary2DAxisValue;
            }
            if (Input.GetKeyDown(KeyCode.Keypad4) && secondary2DAxisValue.x > -1)
            {
                _secondary2DAxisValue -= Vector2.right * debugAxisValueIncrement;
                Secondary2DAxisValueAction = _secondary2DAxisValue;
            }

            // primary button
            if (Input.GetKeyDown(KeyCode.LeftControl))
            {
                PrimaryButtonAction = !PrimaryButtonAction;
            }

            // secondary button
            if (Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SecondaryButtonAction = !SecondaryButtonAction;
            }

            // menu button
            if (Input.GetKeyDown(KeyCode.BackQuote))
            {
                MenuButtonAction = !MenuButtonAction;
            }
        }

        if (!keyboardDebug)
        {
            if (!device.isValid)
            {
                GetDevice();
            }

            // These ranged, non-boolean inputs invoke the events above that are not targetable from the editor

            // Capture trigger value
            if (device.TryGetFeatureValue(CommonUsages.trigger, out _triggerValue))
            {
                if (_triggerValue > minAxisValue) TriggerValueAction = _triggerValue;
                else TriggerValueAction = 0f;
            }
            // Capture grip value
            if (device.TryGetFeatureValue(CommonUsages.grip, out _gripValue))
            {
                if (_gripValue > minAxisValue) GripValueAction = _gripValue;
                else GripValueAction = 0f;
            }
            //don't forget to use an absolute value for the axes

            // Capture primary 2D Axis
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out _primary2DAxisValue))
            {
                if (Mathf.Abs(_primary2DAxisValue.x) > minAxisValue || Mathf.Abs(_primary2DAxisValue.y) > minAxisValue) Primary2DAxisValueAction = _primary2DAxisValue;
                else Primary2DAxisValueAction = Vector2.zero;
            }

            // Capture secondary 2D Axis
            if (device.TryGetFeatureValue(CommonUsages.secondary2DAxis, out _secondary2DAxisValue))
            {
                if (Mathf.Abs(_secondary2DAxisValue.x) > minAxisValue || Mathf.Abs(_secondary2DAxisValue.y) > minAxisValue) Secondary2DAxisValueAction = _secondary2DAxisValue;
                else Secondary2DAxisValueAction = Vector2.zero;
            }


            // These press/release inputs invoke the public, editor-definable events above

            // Capture trigger button      
            if (device.TryGetFeatureValue(CommonUsages.triggerButton, out _triggerButton))
            {
                if (_triggerButton) TriggerButtonAction = true;
                else TriggerButtonAction = false;
            }

            // Capture grip button
            if (device.TryGetFeatureValue(CommonUsages.gripButton, out _gripButton))
            {
                if (_gripButton) GripButtonAction = true;
                else GripButtonAction = false;
            }

            // Capture primary 2d axis button
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxisClick, out _primary2DAxisButton))
            {
                if (_primary2DAxisButton) Primary2DAxisButtonAction = true;
                else Primary2DAxisButtonAction = false;
            }

            // Capture secondary 2d axis button
            if (device.TryGetFeatureValue(CommonUsages.secondary2DAxisClick, out _secondary2DAxisButton))
            {
                if (_secondary2DAxisButton) Secondary2DAxisButtonAction = true;
                else Secondary2DAxisButtonAction = false;
            }

            // Capture primary button
            if (device.TryGetFeatureValue(CommonUsages.primaryButton, out _primaryButton))
            {
                if (_primaryButton) PrimaryButtonAction = true;
                else PrimaryButtonAction = false;
            }

            // Capture secondary button
            if (device.TryGetFeatureValue(CommonUsages.secondaryButton, out _secondaryButton))
            {
                if (_secondaryButton) SecondaryButtonAction = true;
                else SecondaryButtonAction = false;
            }

            // Capture menu button
            if (device.TryGetFeatureValue(CommonUsages.menuButton, out _menuButton))
            {
                if (_menuButton) MenuButtonAction = true;
                else MenuButtonAction = false;
            }
        }    
    }
}
