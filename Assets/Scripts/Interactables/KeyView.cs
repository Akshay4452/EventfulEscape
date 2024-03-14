using UnityEngine;

public class KeyView : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        int currentKeys = GameService.Instance.GetPlayerController().KeysEquipped;

        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.KeyPickUp);

        currentKeys++;
        // Invoke the key pick up event after incrementing the keys
        // -> Key is the publisher
        // -> Player and UI are subscribers
        EventService.Instance.OnKeyPickedUp.InvokeEvent(currentKeys);

        gameObject.SetActive(false);
    }
}
