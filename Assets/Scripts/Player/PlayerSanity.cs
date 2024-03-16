using UnityEngine;

public class PlayerSanity : MonoBehaviour
{
    [SerializeField] private float sanityLevel = 100.0f;
    [SerializeField] private float sanityDropRate = 0.2f;
    [SerializeField] private float sanityDropAmountPerEvent = 10f;
    private float maxSanity;
    private PlayerController playerController;

    private void Start()
    {
        maxSanity = sanityLevel;
        playerController = GameService.Instance.GetPlayerController();
    }
    private void OnEnable()
    {
        EventService.Instance.OnRatRush.AddListener(onSupernaturalEvent);
        EventService.Instance.OnSkullDrop.AddListener(onSupernaturalEvent);
        EventService.Instance.OnPotionDrink.AddListener(onDrankPotion);
        
    }
    private void OnDisable()
    {
        EventService.Instance.OnRatRush.RemoveListener(onSupernaturalEvent);
        EventService.Instance.OnSkullDrop.RemoveListener(onSupernaturalEvent);
        EventService.Instance.OnPotionDrink.RemoveListener(onDrankPotion);

    }
    void Update()
    {
        if (playerController.PlayerState == PlayerState.Dead)
            return;

        float sanityDrop = updateSanity();

        increaseInsanity(sanityDrop);
    }

    private float updateSanity()
    {
        float sanityDrop = sanityDropRate * Time.deltaTime;
        if (playerController.PlayerState == PlayerState.InDark)
        {
            sanityDrop *= 10f;
        }
        return sanityDrop;
    }

    private void increaseInsanity(float amountToDecrease)
    {
        Mathf.Floor(sanityLevel -= amountToDecrease);
        if (sanityLevel <= 0)
        {
            sanityLevel = 0;
            GameService.Instance.GameOver();
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }

    private void decreaseInsanity(float amountToIncrease)
    {
        Mathf.Floor(sanityLevel += amountToIncrease);
        if (sanityLevel > 100)
        {
            sanityLevel = 100;
        }
        GameService.Instance.GetGameUI().UpdateInsanity(1f - sanityLevel / maxSanity);
    }
    private void onSupernaturalEvent()
    {
        increaseInsanity(sanityDropAmountPerEvent);
    }

    private void onDrankPotion(int potionEffect)
    {
        decreaseInsanity(potionEffect);
    }
}