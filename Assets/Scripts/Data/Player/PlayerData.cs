﻿using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "newPlayerData", menuName = "Data/Player Data/Base Data", order = 0)]
    public class PlayerData : ScriptableObject
    {
        public string playerName;
        public string enemyNpc;
    }
}