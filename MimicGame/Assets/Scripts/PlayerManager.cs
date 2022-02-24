using Cinemachine;
using StarterAssets;
using UnityEngine;

namespace UnityTemplateProjects
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject mainCamera;
        
        private static GameObject player;
        private static GameObject playerFollowCamera;

        private void Awake()
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponent<StarterAssetsInputs>().cursorLocked = false;
            playerFollowCamera = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
            playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow =
                GameObject.FindGameObjectWithTag("CinemachineTarget").transform;
            mainCamera = GameObject.Find("MainCamera");
        }

        private void Start()
        {
            BaseGameManager.levelLoaded = EnablePlayer;
            DisablePlayer();
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerFollowCamera);
            DontDestroyOnLoad(mainCamera);
        }

        public static void EnablePlayer()
        {
            player.SetActive(true); 
            playerFollowCamera.SetActive(true);
        }

        public static void DisablePlayer()
        {
            player.SetActive(false);
            playerFollowCamera.SetActive(false);
        }

        public static GameObject GetPlayer()
        {
            return player;
        }
    }
}