using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CoreGameOptions;

public class UIHandler : MonoBehaviour
{
    public Button nextRoundButton;

    private void Start()
    {
        nextRoundButton.onClick.AddListener(NextRound);
    }

    public void NextRound()
    {
        GameManager.StartNextRoundEvent.Invoke();
    }

}
