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
        [SerializeField] private Button startButton;
        public static Action levelLoaded;

        private void Start()
        {
            startButton.onClick.AddListener(delegate { SceneManager.LoadScene("Map"); });
        }
        
    }
}