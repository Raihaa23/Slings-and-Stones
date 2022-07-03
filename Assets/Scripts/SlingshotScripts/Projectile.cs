﻿using Data.Ammo;
using Data.Player;
using Events;
using Interfaces;
using ManagersScripts;
using UnityEngine;

namespace SlingshotScripts
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] protected InputHandler inputHandler;
        [SerializeField] protected AmmoData ammoData;
        [SerializeField] protected PlayerData playerData;
        public int totalAmmo;
        private void OnCollisionEnter2D(Collision2D other) //projectile collision
        {
            if (PlayerTurnManager.Instance.isProjectileReleased != true) return;
            if (other.gameObject.CompareTag(playerData.enemyDestructible))
            {
                var impactForce = other.relativeVelocity.magnitude;
                var enemyGameObj = other.gameObject;
                var enemyScript = enemyGameObj.GetComponent<IDamageable>();
                enemyScript?.Damage(ammoData.damageMultiplier * impactForce);
            }
            MatchEvents.OnCountToEndMethod();
        }
        private void ReduceAmmo() // reduces the ammo count in this script which will be passed on the ammo manager later
        {
            totalAmmo--;
        }
        private void DestroyAmmo() //destroys projectile 
        {
            gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            AmmoEvents.OnDestroyAmmo += DestroyAmmo;
            AmmoEvents.OnReduceAmmo += ReduceAmmo;
            playerData.canDoAction = true;
        }

        private void OnDisable()
        {
            AmmoEvents.OnDestroyAmmo -= DestroyAmmo;
            AmmoEvents.OnReduceAmmo -= ReduceAmmo;
        }
    }
}