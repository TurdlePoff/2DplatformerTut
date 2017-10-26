using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

    public static GameMaster gm; //instance of gm

    public void Start()
    {
        if(gm == null)
        {
            gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameMaster>();
        }
    }

    public Transform playerPrefab;
    public Transform spawnPoint;
    public float spawnDelay = 2;
    public Transform spawnPlayerPrefab;


    public IEnumerator RespawnPlayer()
    {
        GetComponent<AudioSource>().Play();
        yield return new WaitForSeconds(spawnDelay);

        Instantiate(playerPrefab, spawnPoint.position, spawnPoint.rotation);

        Transform spawnParticles = Instantiate(spawnPlayerPrefab, spawnPoint.position, spawnPoint.rotation);
        Destroy(spawnParticles.gameObject, 3f);
    }

    public static void KillPlayer(Player player)
    {
        Destroy(player.gameObject);
        gm.StartCoroutine(gm.RespawnPlayer());
    }

    
}
