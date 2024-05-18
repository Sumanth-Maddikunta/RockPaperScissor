using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreGameOptions;

public class BattleDecider : MonoBehaviour
{
    //This method can be overloaded with an array of players when/if we plan to expand this and make a multiplayer game with n players
    public RoundData DecideBattle(List<PlayerRoundData> players, int roundCount)
    {
        RoundData roundData = new RoundData();
        int count = players.Count;

        for(int i = 0;i< count;i++)
        {
            PlayerRoundData currentPlayer = players[i];

            for (int j = 0; j < count;j++)
            {
                if( i != j)
                {
                    PlayerRoundData opponentPlayer = players[j];

                    bool didCurrentPlayerWin = false;

                    switch (currentPlayer.GetPlayerWeapon)
                    {
                        case CoreGameOptions.Weapon.NONE:
                            break;
                        case CoreGameOptions.Weapon.ROCK:
                            didCurrentPlayerWin = (opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.SCISSORS || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.LIZARD || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.NONE);
                            break;
                        case CoreGameOptions.Weapon.PAPER:
                            didCurrentPlayerWin = (opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.ROCK || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.SPOCK || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.NONE);
                            break;
                        case CoreGameOptions.Weapon.SCISSORS:
                            didCurrentPlayerWin = (opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.PAPER || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.LIZARD || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.NONE);
                            break;
                        case CoreGameOptions.Weapon.SPOCK:
                            didCurrentPlayerWin = (opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.ROCK || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.SCISSORS || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.NONE);
                            break;
                        case CoreGameOptions.Weapon.LIZARD:
                            didCurrentPlayerWin = (opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.PAPER || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.SPOCK || opponentPlayer.GetPlayerWeapon == CoreGameOptions.Weapon.NONE);
                            break;
                    }

                    if(didCurrentPlayerWin)
                    {
                        currentPlayer.IncreaseRoundScore();
                    }
                }
            }
        }

        roundData.SetRoundCount(roundCount);
        roundData.SetPlayerRoundsData(players);

        return roundData;

    }
}
