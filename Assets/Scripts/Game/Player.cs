using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreGameOptions;

public class Player : MonoBehaviour
{
    private PlayerType playerType = PlayerType.REAL;
    private Weapon weaponOfChoice = Weapon.NONE;
    private string playerName;
    private string playerId;
    private int score = 0;

    public PlayerType GetPlayerType{ get { return playerType;}}
    public Weapon GetPlayerWeapon { get { return weaponOfChoice; } }
    public string GetPlayerId { get { return playerId; } }

    public void SetInitialData(PlayerType playerType,string playerName, string playerId)
    {
        this.playerType = playerType;
        this.playerName = playerName;
        this.playerId = playerId;
    }

    public void UpdateWeaponChoice(Weapon weapon)
    {
        this.weaponOfChoice = weapon;
    }

    public void SetRandomWeapon()
    {
        weaponOfChoice =(Weapon)Random.Range(1, 5);
    }

    public void ResetWeapon()
    {
        weaponOfChoice = Weapon.NONE;
    }
}
