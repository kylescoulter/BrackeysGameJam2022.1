using System;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UnityTemplateProjects
{
    public class BaseGameManager : MonoBehaviour
    {
        public static ChestManager chestManager;
        public static Action levelLoaded;
        [SerializeField] private Button startButton;
        [SerializeField] private TextMeshProUGUI time;


        private void Start()
        {
            startButton.onClick.AddListener(delegate { SceneManager.LoadScene("Map"); });
            DontDestroyOnLoad(this);
            chestManager = GetComponent<ChestManager>();
            time.text = DateTime.Now.ToString("HH:mm tt");
        }
    }
}