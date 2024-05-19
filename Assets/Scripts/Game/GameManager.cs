using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreGameOptions;
using UnityEngine.UI;
using System;

namespace CoreGameOptions
{
    public delegate void StartNextRoundDelegate();
    public delegate void UpdateOpponentTextDelegate(Weapon weapon);

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

        public int GetRoundScore { get { return roundScore; } }
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

        public int GetScoreForPlayer(string playerId)
        {
            return playerRoundsData.Find(x => x.GetPlayerId == playerId).GetRoundScore;
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

    public static StartNextRoundDelegate StartNextRoundEvent;
    public static UpdateOpponentTextDelegate UpdateOpponentTextEvent;

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

        StartNextRoundEvent += StartRound;
    }

    void StartGame()
    {
        currentRoundCount = 1;
        StartRound();
    }

    void StartRound()
    {
        Debug.LogError("Start round");
        ResetPlayerWeapons();
        timeHandler.ResetTimer();
        inputHandler.SetCanTakeInput(true);
        UIHandler.EnableNextRoundButtonDelegateEvent(false);


        //Update Scores
        //Update UI
        //Reset Scores
        //StartNext round
    }

    private void ResetPlayerWeapons()
    {
        for(int i = 0; i< players.Count; i++)
        {
            players[i].ResetWeapon();
        }

        UpdateOpponentTextEvent.Invoke(Weapon.NONE);
    }

    public void TimeUp()
    {
        inputHandler.SetCanTakeInput(false);
        FetchInputs();
        CreateRoundDataAndDecideBattle();
        UIHandler.EnableNextRoundButtonDelegateEvent(true);
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

        for (int i = 0; i < players.Count; i++)
        {
            players[i].AddScore(roundData.GetScoreForPlayer(players[i].GetPlayerId));

            Debug.LogError("PLAYER id "+players[i].GetPlayerId+ " Score : "  + players[i].GetPlayerScore);
        }


        if(roundData.GetScoreForPlayer("2") > 0)
        {
            GameEnd();
        }
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
                        UpdateOpponentTextEvent.Invoke(players[i].GetPlayerWeapon);
                    }
                    else
                    {
                        if(players[i].GetPlayerWeapon == Weapon.NONE)
                        {
                            GameEnd();
                        }
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

    public void GameEnd()
    {
        Debug.LogError("GAME END");
        if(PlayerPrefs.HasKey("HS"))
        {
            int hs = PlayerPrefs.GetInt("HS");

            if(player.GetPlayerScore > hs)
            {
                PlayerPrefs.SetInt("HS", player.GetPlayerScore);
            }
        }
        else
        {
            PlayerPrefs.SetInt("HS", player.GetPlayerScore);
        }

        Debug.LogError("High score " + PlayerPrefs.GetInt("HS"));

        SceneHandler.GetInstance.LoadMainMenu();
    }

    public void OnDestroy()
    {
        StartNextRoundEvent = null;
    }
}
