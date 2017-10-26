using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [System.Serializable]

    public class PlayerStats
    {
        public int Health = 100;

        //public void KillPlayer();
    }


    public PlayerStats playerStats = new PlayerStats();
    public int fallYBoundary = -20;

    void Update()
    {
        if(transform.position.y <= fallYBoundary)
        {
            DamagePlayer(9999999);
        }
    }
    
    public void DamagePlayer(int damage)
    {
        playerStats.Health -= damage;
        if(playerStats.Health <= 0)
        {
            GameMaster.KillPlayer(this);
        }
    }

}
