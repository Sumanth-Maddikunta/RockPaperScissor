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

    public delegate void EnableNextRoundButtonDelegate(bool value);
    public static EnableNextRoundButtonDelegate EnableNextRoundButtonDelegateEvent;

    private void Start()
    {
        GameManager.UpdateOpponentTextEvent += UpdateOpponentText;
        nextRoundButton.onClick.AddListener(NextRound);
        EnableNextRoundButtonDelegateEvent += EnableNextRoundButton;
    }

    public void NextRound()
    {
        GameManager.StartNextRoundEvent.Invoke();
        EnableNextRoundButton(false);
    }

    public void UpdateOpponentText(Weapon weapon)
    {
        opponentWeaponText.text = weapon.ToString();
    }

    public void EnableNextRoundButton(bool value)
    {
        nextRoundButton.gameObject.SetActive(value);
    }

    private void OnDestroy()
    {
        GameManager.UpdateOpponentTextEvent -= UpdateOpponentText;
        EnableNextRoundButtonDelegateEvent -= EnableNextRoundButton;
    }
}
