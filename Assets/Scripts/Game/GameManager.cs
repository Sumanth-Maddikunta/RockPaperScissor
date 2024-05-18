using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreGameOptions;
using UnityEngine.UI;
using System;

namespace CoreGameOptions
{
    public enum GameMode
    {
        BOT,
        PVE
    }

    public enum Weapon
    {
        NONE = 0,
        ROCK,
        PAPER,
        SCISSORS,
        SPOCK,
        LIZARD
    }

    public enum PlayerType
    {
        BOT,
        REAL
    }

    public class PlayerRoundData
    {
        private string playerId;
        private Weapon choiceOfWeapon;
        private int roundScore;

        public string GetPlayerId { get { return playerId; } }
        public Weapon GetPlayerWeapon { get { return choiceOfWeapon; } }

        public void UpdateRoundData(string playerId, Weapon weapon)
        {
            this.playerId = playerId;
            choiceOfWeapon = weapon;
        }

        public void IncreaseRoundScore()
        {
            roundScore++;
        }
    }

    public class RoundData
    {
        private int roundNumber;
        List<PlayerRoundData> playerRoundsData;

        public void SetPlayerRoundsData(List<PlayerRoundData> data)
        {
            playerRoundsData = data;
        }

        internal void SetRoundCount(int roundCount)
        {
            roundNumber = roundCount;
        }
    }

}


public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager getInstance{get{return instance;}}

    private GameMode gameMode;

    private bool canPlay = false;
    public bool canPlayGetter { get { return canPlay; } }

    public const float roundTime = 5f;
    public TimeHandler timeHandler;
    public InputHandler inputHandler;

    int currentRoundCount = 1;
    private List<RoundData> roundsData;
    private BattleDecider battleDecider;

    List<Player> players;
    Player player;

    void Awake()
    {
        if(instance == null)
        {
            instance = new GameManager();
        }
    }

    private void Start()
    {
        SetupGame();
        StartGame();
    }

    void SetupGame()
    {
        timeHandler.SetupTimeHandler(roundTime,this);
        inputHandler.UpdateGameManager(this);
        roundsData = new List<RoundData>();
        gameMode = GameMode.BOT;
        battleDecider = new BattleDecider();
        players = new List<Player>();

        if(gameMode == GameMode.BOT)
        {
            player = new Player();
            player.SetInitialData(PlayerType.REAL, "Player", "1");
            players.Add(player);

            Player bot = new Player();
            bot.SetInitialData(PlayerType.BOT, "BOT", "2");
            players.Add(bot);

        }
    }

    void StartGame()
    {
        currentRoundCount = 1;
        StartRound();
    }

    void StartRound()
    {
        Debug.LogError("Start round");
        inputHandler.SetCanTakeInput(true);
        timeHandler.ResetTimer();     
       
        //Update Scores
        //Update UI
        //Reset Scores
        //StartNext round
    }

    public void TimeUp()
    {
        inputHandler.SetCanTakeInput(false);
        FetchInputs();
        CreateRoundDataAndDecideBattle();
    }

    void CreateRoundDataAndDecideBattle()
    {
        List<PlayerRoundData> playerRoundDatas = new List<PlayerRoundData>();

        for (int i = 0; i< players.Count;i++)
        {
            PlayerRoundData playerRoundData = new PlayerRoundData();
            playerRoundData.UpdateRoundData(players[i].GetPlayerId, players[i].GetPlayerWeapon);
            playerRoundDatas.Add(playerRoundData);
        }

        RoundData roundData = battleDecider.DecideBattle(playerRoundDatas,currentRoundCount);
        roundsData.Add(roundData);
    }

    void FetchInputs()
    {
        switch (gameMode)
        {
            case GameMode.BOT:
                for(int i = 0;i< players.Count;i++)
                {
                    if(players[i].GetPlayerType == PlayerType.BOT)
                    {
                        players[i].SetRandomWeapon();
                    }
                }

                break;
            case GameMode.PVE:
                //Fetch other players inputs
                break;
        }
    }

    public void SetPlayerInput(Weapon weapon)
    {
        player.UpdateWeaponChoice(weapon);
    }

   [ContextMenu("ResetTimer")]
   public void ResetTimer()
    {
        timeHandler.ResetTimer();
    }
}
