using System.Collections.Generic;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    public delegate void LightSwitchDelegate(); // signature of delegate
    public static LightSwitchDelegate lightSwitchDelegate; // instance of delegate (public static variable is not recommended)

    private void Start() => currentState = SwitchState.Off;

    private void OnEnable() => lightSwitchDelegate += OnLightSwitchToggled; // subscribe

    private void OnDisable() => lightSwitchDelegate -= OnLightSwitchToggled; // unsubscribe

    // 1. Always append the listeners using +=
    // 2. Always unsubscribe to any delegate/event
    // 3. Always have null check before invoking any delegate using ?.

    public void Interact()
    {
        lightSwitchDelegate?.Invoke();
    }
    private void toggleLights()
    {
        bool lights = false;

        switch (currentState)
        {
            case SwitchState.On:
                currentState = SwitchState.Off;
                lights = false;
                break;
            case SwitchState.Off:
                currentState = SwitchState.On;
                lights = true;
                break;
            case SwitchState.Unresponsive:
                break;
        }
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = lights;
        }
    }

    private void OnLightSwitchToggled()
    {
        toggleLights();
        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
    }
}
