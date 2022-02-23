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
        private GameObject player;

        private void Start()
        {
            BaseGameManager.levelLoaded?.Invoke();
            player = PlayerManager.GetPlayer();
            player.GetComponent<StarterAssetsInputs>().cursorLocked = true;
        }
    }
}