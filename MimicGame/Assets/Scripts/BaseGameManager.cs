using System;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UnityTemplateProjects
{
    public class BaseGameManager : MonoBehaviour
    {
        public static ChestManager chestManager;
        public static UnityAction levelLoaded;
        public static Action mapLoaded;
        
        
        [SerializeField] private Button startButton;
        [SerializeField] private TextMeshProUGUI time;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject playerFollowCamera;
        [SerializeField] private GameObject mainCamera;
        //[SerializeField] private GameObject playerPrefab;

        
        private static int tavernPayment;
        private static int dungeonPayment;
        private static int forestPayment;


        private void Awake()
        {
            mapLoaded = GenerateChestPayments;
        }

        private void Start()
        {
            Cursor.visible = true;
            Screen.lockCursor = false;
            startButton.onClick.AddListener(delegate
            {
                mapLoaded?.Invoke();
                SceneManager.LoadScene("Map");
            });
            DontDestroyOnLoad(this);
            chestManager = GetComponent<ChestManager>();
            time.text = DateTime.Now.ToString("hh:mm tt");
            
            player.GetComponent<Inputs>().cursorLocked = false;
            
            playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow =
                GameObject.FindGameObjectWithTag("CinemachineTarget").transform;
            player.GetComponent<PlayerManager>().DisablePlayer();
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerFollowCamera);
            DontDestroyOnLoad(mainCamera);
            
        }

        private void GenerateChestPayments()
        {
            tavernPayment = Random.Range(100, 500);
            dungeonPayment = Random.Range(100, 500);
            forestPayment = Random.Range(100, 500);
        }

        public static int GetTavernPayment()
        {
            return tavernPayment;
        }
        
        public static int GetDungeonPayment()
        {
            return dungeonPayment;
        }
        
        public static int GetForestPayment()
        {
            return forestPayment;
        }
    }
}