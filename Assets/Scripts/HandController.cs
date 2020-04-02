//Adapted by Alex Coulombe @ibrews from @icave_user's script described here: https://forum.unity.com/threads/any-example-of-the-new-2019-1-xr-input-system.629824/
//it's set up to animate hands so those lines are commented out

using UnityEngine;
using DilmerGamesLogger = DilmerGames.Core.Logger;

[RequireComponent(typeof(Animator))]
public class HandController : MonoBehaviour
{

    public bool IsGripPressed;
    public bool IsTriggerPressed;
    public bool IsMenuPressed;
    public bool IsClickPressed;

    // Button Events
    public ButtonEvent GripEvent { get; set; }
    public ButtonEvent TriggerEvent { get; set; }
    public ButtonEvent MenuEvent { get; set; }
    public ButtonEvent ClickEvent { get; set; }

    //private Animator _animator;

    void Start()
    {
        InitializeButtons();
        //_animator = GetComponent<Animator>();

    }

    private void InitializeButtons()
    {
        (GripEvent = new ButtonEvent()).Initialize(IsGripPressed, OnGripButtonEvent);
        (TriggerEvent = new ButtonEvent()).Initialize(IsTriggerPressed, OnTriggerButtonEvent);
        (MenuEvent = new ButtonEvent()).Initialize(IsMenuPressed, OnMenuButtonEvent);
        (ClickEvent = new ButtonEvent()).Initialize(IsClickPressed, OnClickButtonEvent);
    }

    // Button Functions
    private void OnGripButtonEvent(bool pressed)
    {
        IsGripPressed = pressed;
        //_animator.SetBool("GripAnimation", pressed);

        if (pressed)
        {
            Debug.Log("Grip Pressed");
            DilmerGamesLogger.Instance.LogInfo("Grip Pressed"); 
        }
        else
        {    
            Debug.Log("Grip Released");
            DilmerGamesLogger.Instance.LogInfo("Grip Released");
        }
    }

    private void OnTriggerButtonEvent(bool pressed)
    {
        IsTriggerPressed = pressed;
        //_animator.SetBool("TriggerAnimation", pressed);

        if (pressed)
        {

            Debug.Log("Trigger Pressed");
            DilmerGamesLogger.Instance.LogInfo("Trigger Pressed");
        }
        else
        {
            Debug.Log("Trigger Released");
            DilmerGamesLogger.Instance.LogInfo("Trigger Released");
        }
    }

    private void OnMenuButtonEvent(bool pressed)
    {
        IsMenuPressed = pressed;
        if (pressed)
        {
            Debug.Log("Menu Pressed");
            DilmerGamesLogger.Instance.LogInfo("Menu Pressed");
        }
    }

    private void OnClickButtonEvent(bool pressed)
    {
        IsClickPressed = pressed;
        //_animator.SetBool("ClickAnimation", pressed);

        if (pressed)
        {
            Debug.Log("Click Pressed");
            DilmerGamesLogger.Instance.LogInfo("Click Pressed");
        }
        else
        {
            Debug.Log("Click Released");
            DilmerGamesLogger.Instance.LogInfo("Click Released");
        }
    }
}