﻿using Data.Level;
using Events;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ManagersScripts
{
    public class LevelSelectionManager : MonoBehaviour
    {
        [SerializeField] private LevelData levelData;

        public void TimerChoice(float timeChoice)
        {
            levelData.timerChoice = timeChoice * 60;
        }

        public void LevelChoice(string levelChoice)
        {
            levelData.levelChoice = levelChoice;
        }
        
        public void PlayerTurnChoice(string playerName) // Starts the game with Player 1's turn 
        {
            PlayerTurnManager.Instance.playerInTurnName = playerName;
        }
    }
}