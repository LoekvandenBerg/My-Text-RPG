using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public interface IBaddie
    {
        void Cry();
    }
    public class Enemy : Character, IBaddie
    {
        public string Description { get; set; }

        public override void TakeDamage(int amount)
        {
            
            base.TakeDamage(amount);
            UIController.OnEnemyUpdate(this); 
            Debug.Log("this is only on the enemy!");
        }

        public void Cry()
        {

        }

        public override void Die()
        {
            Encounter.OnEnemyDie();
            Energy = MaxEnergy;
            Debug.Log("Character died was enemy");
        }
    }
}
