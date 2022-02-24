using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityTemplateProjects
{
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private Transform chestSpawn;
        private GameObject player;


        private void Start()
        {
            player = PlayerManager.GetPlayer();
            player.GetComponent<StarterAssetsInputs>().cursorLocked = true;
            SpawnChest();
        }

        private void SpawnChest()
        {
            var chest = BaseGameManager.chestManager.GenerateChest();
            Instantiate(chest, chestSpawn.position, chestSpawn.rotation);
        }
        
        
    }
}