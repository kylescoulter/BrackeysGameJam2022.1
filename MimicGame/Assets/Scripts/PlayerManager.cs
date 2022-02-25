using System;
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
        private StarterAssetsInputs inputs;
        [SerializeField] private GameObject book;
        [SerializeField] private Animator bookAnimator;

        private void Awake()
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponent<StarterAssetsInputs>().cursorLocked = false;
            playerFollowCamera = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
            playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow =
                GameObject.FindGameObjectWithTag("CinemachineTarget").transform;
            //mainCamera = GameObject.Find("MainCamera");

            inputs = player.GetComponent<StarterAssetsInputs>();
        }

        private void Start()
        {
            BaseGameManager.levelLoaded = EnablePlayer; 
            
            DisablePlayer();
            DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerFollowCamera);
            DontDestroyOnLoad(mainCamera);
        }

        private void Update()
        {
            if (inputs.interact)
            {
                Instantiate(book);
                book.transform.position = new Vector3(player.transform.position.x + 5, player.transform.position.y,
                    player.transform.position.z);
                bookAnimator.SetTrigger("Open");
            }
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