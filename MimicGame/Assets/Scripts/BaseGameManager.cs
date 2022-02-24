using System;
using Cinemachine;
using StarterAssets;
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
        

        private void Start()
        {
            startButton.onClick.AddListener(delegate { SceneManager.LoadScene("Map"); });
            DontDestroyOnLoad(this);
            chestManager = GetComponent<ChestManager>();
        }
    }
}