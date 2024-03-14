using UnityEngine;

public class KeyView : MonoBehaviour, IInteractable
{
    //private void OnEnable() => EventService.Instance.OnKeyPickedUp.AddListener(onKeyPickUp);
    //private void OnDisable() => EventService.Instance.OnKeyPickedUp.RemoveListener(onKeyPickUp);
    public void Interact()
    {
        int currentKeys = GameService.Instance.GetPlayerController().KeysEquipped;

        GameService.Instance.GetInstructionView().HideInstruction();
        GameService.Instance.GetSoundView().PlaySoundEffects(SoundType.KeyPickUp);

        currentKeys++;
        // Invoke the key pick up event after incrementing the keys
        GameService.Instance.GetGameUI().UpdateKeyText();

        gameObject.SetActive(false);
    }
}
