﻿using UnityEngine;
using UnityEngine.SceneManagement;

namespace ManagersScripts
{
    public class PlayerTurnManager : MonoBehaviour
    {
        #region Singleton

        private static PlayerTurnManager _instance;
        public static PlayerTurnManager Instance => _instance;

        private void Awake()
        {
            if (_instance == null)
            {
                DontDestroyOnLoad(gameObject);
                _instance = this;
            }
            else if (_instance != this)
            {
                Destroy(gameObject);
            }
        }
        #endregion

        public string playerInTurnName;
    
        public void EndTurn() // Ends player turn after projectile is destroyed
        {
            if (playerInTurnName == "Player1Ball")
            {
                playerInTurnName = "Player2Ball";
            }
        
            else if (playerInTurnName == "Player2Ball")
            {
                playerInTurnName = "Player1Ball";
            }
        }

        public void StartWithPlayer1() // Starts the game with Player 1's turn 
        {
            playerInTurnName = "Player1Ball";
            SceneManager.LoadScene("Stage 1");
        }
    
        public void StartWithPlayer2() // Starts the game with Player 2's turn 
        {
            playerInTurnName = "Player2Ball";
            SceneManager.LoadScene("Stage 1");
        }
    }
}