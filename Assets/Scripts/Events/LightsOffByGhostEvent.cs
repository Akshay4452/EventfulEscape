using UnityEngine;

public class LightsOffByGhostEvent : MonoBehaviour
{
    [SerializeField] private int keysRequiredToTrigger;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerView>() != null &&
            keysRequiredToTrigger == GameService.Instance.GetPlayerController().KeysEquipped)
        {
            EventService.Instance.OnLightsOffByGhostEvent.InvokeEvent();
            this.enabled = false;
        }
    }
}