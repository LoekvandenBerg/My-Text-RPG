using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TextRPG {
    public class Player : Character
    {
        public int Floor { get; set; }
        public Room Room { get; set; }
        [SerializeField] Encounter encounter;
        public World world;


        // Start is called before the first frame update
        void Start()
        {
            Floor = 0;
            Energy = 30;
            Attack = 10;
            Defense = 5;
            Gold = 0;
            Inventory = new List<string>();
            RoomIndex = new Vector2(2, 2);
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            this.Room.Empty = true;
            UIController.OnPlayerStatChange();
            UIController.OnPlayerInventoryChange();
        }

        public void Move(int direction)
        {
            if (this.Room.Enemy)
            {
                return;
            }
            //North
            if(direction == 0 && RoomIndex.y > 0)
            {
                Journal.Instance.Log("You moved north");
                RoomIndex -= Vector2.up;
            }
            //East
            if(direction == 1 && RoomIndex.x < world.Dungeon.GetLength(0)-1)
            {
                Journal.Instance.Log("You moved east");
                RoomIndex += Vector2.right;
            }
            //South
            if(direction == 2 && RoomIndex.y < world.Dungeon.GetLength(1)-1)
            {
                Journal.Instance.Log("You moved south");
                RoomIndex -= Vector2.down;
            }
            //West
            if(direction == 3 && RoomIndex.x > 0)
            {
                Journal.Instance.Log("You moved west");
                RoomIndex += Vector2.left;
            }
            if (this.Room.RoomIndex != RoomIndex)
            {
                Investigate();
            }
        }

        public void Investigate()
        {
            this.Room = world.Dungeon[(int)RoomIndex.x, (int)RoomIndex.y];
            encounter.ResetDynamicControls();
            if (this.Room.Empty)
            {
                Journal.Instance.Log("You are in an empty room");
            }
            else if (this.Room.Chest != null)
            {
                encounter.StartChestEncounter();
                Journal.Instance.Log("You've found a chest! What do you want to do?");
            }
            else if(this.Room.Enemy != null)
            {
                Journal.Instance.Log("You are jumped by a " + Room.Enemy.Description + "! What would you like to do?");
                encounter.StartCombat(); 
            }
            else if (this.Room.Exit)
            {
                encounter.StartExit();
                Journal.Instance.Log("The exit to the next floor. Would you like to continue?");
            }
        }

        public void AddItem(string item)
        {
            Journal.Instance.Log("You were given: " + item);
            UIController.OnPlayerInventoryChange();
            Inventory.Add(item);
        }

        public override void TakeDamage(int amount)
        {
            Debug.Log("Player takedamage");
            base.TakeDamage(amount);
            UIController.OnPlayerStatChange();
        }

        public override void Die()
        {
            Debug.Log("Player died");
            base.Die();
        }
    }
}