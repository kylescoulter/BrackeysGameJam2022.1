using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityTemplateProjects
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform chestSpawn;
        [SerializeField] private GameObject itemSpawn;
        [SerializeField] private Transform playerSpawn;
        [SerializeField] private GameObject exitDoor;
        
        [Header("Chest Objects")]
        [SerializeField] private GameObject chestObj;
        
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
            GameObject chest = Instantiate(chestObj, chestSpawn.position, chestSpawn.rotation);
            BaseGameManager.chestManager.GenerateChest(chest);
            if (SceneManager.GetActiveScene().name == "Forest")
            {
                chest.GetComponent<Chest>().SetChestPayment(BaseGameManager.GetForestPayment());
            } 
            else if (SceneManager.GetActiveScene().name == "Tavern")
            {
                chest.GetComponent<Chest>().SetChestPayment(BaseGameManager.GetTavernPayment());
            }
            else if (SceneManager.GetActiveScene().name == "Dungeon")
            {
                chest.GetComponent<Chest>().SetChestPayment(BaseGameManager.GetDungeonPayment());
            }
        }

        private void SpawnItems()
        {
            var itemSpawnList = itemSpawn.GetComponentsInChildren<Transform>().ToList();
            itemSpawnList.RemoveAt(0);
            BaseGameManager.chestManager.SpawnOwnerItems(itemSpawnList);
        }
    }
}