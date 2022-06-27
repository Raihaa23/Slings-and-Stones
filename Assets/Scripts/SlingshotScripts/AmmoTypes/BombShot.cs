﻿using System;
using Events;
using Interfaces;
using UnityEngine;
using ManagersScripts;

namespace SlingshotScripts.AmmoTypes
{
    public class BombShot : Slingshot
    {
        [SerializeField] private float fieldOfImpact;
        [SerializeField] private float force;

        [SerializeField] private LayerMask layerToHit;

        protected override void Update()
        {
            HandleAction();
            base.Update();
        }

        private void HandleAction()
        {
            if (!inputHandler.SpaceBarDown) return;
            if (PlayerTurnManager.Instance.isProjectileReleased && playerData.canDoAction)
            {
                Explode();
                playerData.canDoAction = false;
            }
        }

        private void Explode()
        {
           Collider2D[] objects = Physics2D.OverlapCircleAll(transform.position, fieldOfImpact, layerToHit);

           foreach (var obj in objects)
           {
               Vector2 direction = obj.transform.position - transform.position;
               obj.GetComponent<Rigidbody2D>().AddForce(direction * force);
               var enemyScript = obj.GetComponent<IDamageable>();
               enemyScript?.Damage(ammoData.specialDamage);
           }
           GameEvents.OnCountToEndMethod();
           GameEvents.OnDestroyAmmoMethod();
        }
        
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, fieldOfImpact);
        }
        
    }
}