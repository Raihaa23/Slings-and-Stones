using System.Collections.Generic;
using Data.Player;
using Events;
using SlingshotScripts;
using TMPro;
using UnityEngine;

namespace ManagersScripts
{
    public class AmmoManager : MonoBehaviour
    {
        [SerializeField] private GameObject currentAmmo;
        [SerializeField] private Transform origin;
        [SerializeField] private Rigidbody2D originRigidBody2D;
        [SerializeField] private PlayerData playerData;
        [SerializeField] private int ammoIndex;
        [SerializeField] private List<GameObject> ammoList = new List<GameObject>();
        [SerializeField] private GameObject ammoButtons;

        [SerializeField] private TextMeshProUGUI ammoToText;

        [SerializeField] private int ammoLeft = 0;

        private void Start()
        {
            LoadAmmo();
            ToggleAmmoButtons();
            ToggleAmmoText();
        }
        
        private void LoadAmmo() //instantiates a ball and sets its origin
        {
            if (playerData.playerName != PlayerTurnManager.Instance.playerInTurnName) return;
            currentAmmo = ammoList[ammoIndex];
            var slingshot = currentAmmo.GetComponent<Slingshot>();
            ammoLeft = slingshot.totalAmmo;
            ammoToText.text = "Ammo: " + ammoLeft;
            if (ammoLeft <= -1)
            {
                ammoToText.text = "Ammo: Infinite";
            }
            currentAmmo.transform.position = origin.position;
            currentAmmo.GetComponent<SpringJoint2D>().connectedBody = originRigidBody2D;
            playerData.equippedAmmo = currentAmmo.tag;
            currentAmmo.transform.eulerAngles = new Vector3(0,0,0);
            currentAmmo.SetActive(true);
            PlayerTurnManager.Instance.isProjectileReleased = false;
        }

        private void ToggleAmmoButtons()
        {
            if (playerData.playerName != PlayerTurnManager.Instance.playerInTurnName) return;
            switch (ammoButtons.activeSelf)
            {
                case true:
                    ammoButtons.SetActive(false);
                    break;
                case false:
                    ammoButtons.SetActive(true);
                    break;
            }
        }

        public void LoadNextAmmo()
        {
            GameEvents.OnDestroyAmmoMethod();
            if (ammoIndex >= ammoList.Count - 1)
            {
                ammoIndex = 0;
            }
            else
            {
                ammoIndex += 1;
            }
            LoadAmmo();
        }

        public void LoadPreviousAmmo()
        {
            GameEvents.OnDestroyAmmoMethod();
            if (ammoIndex <= 0)
            {
                ammoIndex = ammoList.Count - 1;
                
            }
            else
            {
                ammoIndex -= 1;
                
            }
            LoadAmmo();
        }
        
        private void ReduceAmmo()
        {
            if (playerData.playerName != PlayerTurnManager.Instance.playerInTurnName) return;
            if (ammoLeft == -1) return;
            ammoLeft--;
            if (ammoLeft == 0)
            {
                ammoList.Remove(currentAmmo);
            }
            ammoIndex = 0;
        }

        private void ToggleAmmoText()
        {
            if (playerData.playerName != PlayerTurnManager.Instance.playerInTurnName) return;
            switch (ammoToText.enabled)
            {
                case true:
                    ammoToText.enabled = false;
                    break;
                case false:
                    ammoToText.enabled = true;
                    break;
            }
        }
        
        private void OnEnable()
        {
            GameEvents.OnLoadAmmo += LoadAmmo;
            GameEvents.OnToggleAmmoButton += ToggleAmmoButtons;
            GameEvents.OnReduceAmmo += ReduceAmmo;
            GameEvents.OnToggleAmmoText += ToggleAmmoText;
        }

        private void OnDisable()
        {
            GameEvents.OnLoadAmmo -= LoadAmmo;
            GameEvents.OnToggleAmmoButton -= ToggleAmmoButtons;
            GameEvents.OnReduceAmmo -= ReduceAmmo;
            GameEvents.OnToggleAmmoText -= ToggleAmmoText;
        }
    }
}
