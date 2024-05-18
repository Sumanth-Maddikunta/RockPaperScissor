using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using CoreGameOptions;

public class InputHandler : MonoBehaviour
{

    public Button rockButton;
    public Button paperButton;
    public Button scissorButton;
    public Button lizardButton;
    public Button spockButton;

    private GameManager gameManager;

    private bool canTakeInput = false;

    // Start is called before the first frame update
    void Start()
    {
        rockButton.onClick.AddListener(()=>UpdatePlayerInput(Weapon.ROCK));
        paperButton.onClick.AddListener(() => UpdatePlayerInput(Weapon.PAPER));
        scissorButton.onClick.AddListener(() => UpdatePlayerInput(Weapon.SCISSORS));
        lizardButton.onClick.AddListener(() => UpdatePlayerInput(Weapon.LIZARD));
        spockButton.onClick.AddListener(() => UpdatePlayerInput(Weapon.SPOCK));

       
    }

    public void UpdateGameManager(GameManager gm)
    {
        gameManager = gm;
    }

    public void SetCanTakeInput(bool value)
    {
        canTakeInput = value;
    }

    void UpdatePlayerInput(Weapon weapon)
    {
        if(canTakeInput)
        {
            gameManager.SetPlayerInput(weapon);
        }
    }
    
}
