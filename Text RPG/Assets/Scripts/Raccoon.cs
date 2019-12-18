using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG
{
    public class Raccoon : Enemy
    {
        // Start is called before the first frame update
        void Start()
        {
            Energy = 10;
            MaxEnergy = 10;
            Attack = 12;
            Defense = 3;
            Gold = 20;
            Inventory.Add("Bandit Mask");
            Description = "Raccoon";
        }

    }
}