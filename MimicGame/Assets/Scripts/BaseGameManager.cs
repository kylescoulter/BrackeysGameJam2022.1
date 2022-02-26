using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UnityTemplateProjects
{
    public class BaseGameManager : MonoBehaviour
    {
        public static ChestManager chestManager;
        public static Action levelLoaded;
        public static Action mapLoaded;
        
        [SerializeField] private Button startButton;
        [SerializeField] private TextMeshProUGUI time;

        
        private static int tavernPayment;
        private static int dungeonPayment;
        private static int forestPayment;


        private void Awake()
        {
            mapLoaded = GenerateChestPayments;
        }

        private void Start()
        {
            startButton.onClick.AddListener(delegate
            {
                mapLoaded?.Invoke();
                SceneManager.LoadScene("Map");
            });
            DontDestroyOnLoad(this);
            chestManager = GetComponent<ChestManager>();
            time.text = DateTime.Now.ToString("hh:mm tt");
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