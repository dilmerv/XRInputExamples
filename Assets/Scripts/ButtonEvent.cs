//Adapted by Alex Coulombe @ibrews from @icave_user's script described here: https://forum.unity.com/threads/any-example-of-the-new-2019-1-xr-input-system.629824/

using UnityEngine.Events;

[System.Serializable] // Generic Event holding button value
public class ButtonEvent : UnityEvent<bool>
{
    public bool Value { get; set; }

    public void Initialize(bool value, UnityAction<bool> method)
    {
        Value = value;
        AddListener(method);
    }
}