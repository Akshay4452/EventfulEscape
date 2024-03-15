using System;
using System.Collections.Generic;
using UnityEditor.MPE;
using UnityEngine;

public class LightSwitchView : MonoBehaviour, IInteractable
{
    [SerializeField] private List<Light> lightsources = new List<Light>();
    private SwitchState currentState;

    private void OnEnable()
    {
        EventService.Instance.OnLightSwitchToggled.AddListener(onLightSwitch);
        EventService.Instance.OnLightsOffByGhostEvent.AddListener(onLightsOffByGhost);
    }

    private void OnDisable()
    {
        EventService.Instance.OnLightSwitchToggled.RemoveListener(onLightSwitch);
        EventService.Instance.OnLightsOffByGhostEvent.RemoveListener(onLightsOffByGhost);
    }

    private void Start() => currentState = SwitchState.Off;

    public void Interact() => EventService.Instance.OnLightSwitchToggled.InvokeEvent();

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

    private void onLightSwitch()
    {
        toggleLights();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
        GameService.Instance.GetInstructionView().HideInstruction();
    }

    private void turnOffLights()
    {
        foreach (Light lightSource in lightsources)
        {
            lightSource.enabled = false;
        }
    }

    private void onLightsOffByGhost()
    {
        if (currentState == SwitchState.On)
        {
            turnOffLights();
            GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SwitchSound);
            GameService.Instance.GetInstructionView().HideInstruction();
            currentState = SwitchState.Off;
        }
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.SpookyGiggle);
    }
}
