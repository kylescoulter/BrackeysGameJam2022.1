using System;
using System.Collections.Generic;
using System.Linq;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityTemplateProjects
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform chestSpawn;
        [SerializeField] private GameObject itemSpawn;
        [SerializeField] private Transform playerSpawn;
        private GameObject player;
        private List<Transform> itemList;


        private void Awake()
        { 
            player = PlayerManager.GetPlayer();
            player.GetComponent<Inputs>().cursorLocked = true;
            player.transform.position = playerSpawn.localPosition;
            player.transform.rotation = playerSpawn.localRotation;
        }

        private void Start()
        {
            SpawnChest();
            SpawnItems();
        }

        private void SpawnChest()
        {
            var chest = BaseGameManager.chestManager.GenerateChest();
            Instantiate(chest, chestSpawn.position, chestSpawn.rotation);
        }

        private void SpawnItems()
        {
            var itemSpawnList = itemSpawn.GetComponentsInChildren<Transform>().ToList();
            itemSpawnList.RemoveAt(0);
            BaseGameManager.chestManager.SpawnOwnerItems(itemSpawnList);
            foreach (var spawn in itemSpawnList)
            {
                Debug.Log("Spawn name: " + spawn.name);
            }
            
        }
    }
}