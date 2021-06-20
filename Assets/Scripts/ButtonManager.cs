using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private EventTrigger buttonClickedEventTrigger;

    public void ButtonClickTrigger()
    {
        buttonClickedEventTrigger.Trigger();
    }
}
