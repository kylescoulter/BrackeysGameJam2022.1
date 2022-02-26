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
    public class DungeonManager : MonoBehaviour
    {
        [SerializeField] private Transform chestSpawn;
        [SerializeField] private GameObject itemSpawn;
        [SerializeField] private GameObject playerSpawn;
        private GameObject player;
        private List<Transform> itemList;


        private void Start()
        {
            player = PlayerManager.GetPlayer();
            player.GetComponent<Inputs>().cursorLocked = true;
            player.transform.position = playerSpawn.transform.position;
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
            BaseGameManager.chestManager.SpawnOwnerItems(itemSpawn.GetComponentsInChildren<RectTransform>().ToList());
        }
    }
}