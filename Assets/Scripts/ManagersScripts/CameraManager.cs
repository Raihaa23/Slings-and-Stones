﻿using System;
using Cinemachine;
using UnityEngine;
using Events;

namespace ManagersScripts
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] private CinemachineVirtualCamera cvCamera;
        [SerializeField] private Transform player1Origin;
        [SerializeField] private Transform player2Origin;

        private void Start()
        {
            ToggleCamera();
        }

        private void Update()
        {
            Debug.Log(cvCamera.Follow);
        }

        private void ToggleCamera()
        {
            if (PlayerTurnManager.Instance.playerInTurnName == "Player1")
            {
                cvCamera.Follow = player1Origin;
            }
            else if (PlayerTurnManager.Instance.playerInTurnName == "Player2")
            {
                cvCamera.Follow = player2Origin;
            }
        }
        private void SetCamera(GameObject projectile)
        {
            cvCamera.Follow = projectile.transform;

        }
        private void OnEnable()
        {
            GameEvents.OnSetCamera += SetCamera;
            GameEvents.OnToggleCamera += ToggleCamera;

        }

        private void OnDisable()
        {
            GameEvents.OnSetCamera -= SetCamera;
            GameEvents.OnToggleCamera -= ToggleCamera;

        }
    }
}