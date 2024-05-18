using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CoreGameOptions;
using TMPro;

public class UIHandler : MonoBehaviour
{
    public Button nextRoundButton;

    public TMP_Text opponentWeaponText;

    private void Start()
    {
        GameManager.UpdateOpponentTextEvent += UpdateOpponentText;
        nextRoundButton.onClick.AddListener(NextRound);
    }

    public void NextRound()
    {
        GameManager.StartNextRoundEvent.Invoke();
    }

    public void UpdateOpponentText(Weapon weapon)
    {
        opponentWeaponText.text = weapon.ToString();
    }

}
