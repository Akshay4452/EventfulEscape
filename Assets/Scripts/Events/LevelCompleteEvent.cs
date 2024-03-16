using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCompleteEvent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerView>() != null)
        {
            EventService.Instance.OnLevelComplete.InvokeEvent();
            this.enabled = false;
        }
    }
}
