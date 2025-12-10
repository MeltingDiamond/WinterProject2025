using System;
using UnityEngine;

namespace Core.Player
{
    public class PlayerHealth : MonoBehaviour
    {
        public int PlayerCurrentHealth { get; private set; }
        public bool PlayerIsDead { get; private set; }
        
        [SerializeField] private int playerMaxHealth = 100;

        private void Awake()
        {
            PlayerIsDead = false;
            PlayerCurrentHealth = playerMaxHealth;
        }

        public void TakeDamage(int damage)
        {
            PlayerCurrentHealth -= damage;

            if (PlayerCurrentHealth <= 0f) PlayerIsDead = true;
        }

        public void ResetHealth()
        {
            PlayerCurrentHealth = playerMaxHealth;
        }

        public void HealFromDamage(int healing)
        {
            PlayerCurrentHealth += healing;
            
            if (PlayerCurrentHealth > 100) PlayerCurrentHealth = playerMaxHealth;
        }
    }
}