using System;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UnityTemplateProjects
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private GameObject playerPrefab;
        [SerializeField] private GameObject mainCamera;
        private InputAction openBook;
        private InputAction closeBook;
        
        
        private static GameObject player;
        private static GameObject playerFollowCamera;
        private Inputs inputs;
        private PlayerInput playerInput;
        private GameObject book;
        private BookManager bookManager;
        

        private void Awake()
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponent<Inputs>().cursorLocked = false;
            playerFollowCamera = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
            playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow =
                GameObject.FindGameObjectWithTag("CinemachineTarget").transform;

            book = player.GetComponentInChildren<BookManager>().gameObject;
            bookManager = book.GetComponent<BookManager>();
            
            playerInput = player.GetComponent<PlayerInput>();
            inputs = player.GetComponent<Inputs>();
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
            if (inputs.openBook && !bookManager.IsBookOpen())
            {
                bookManager.OpenBook();
                playerInput.SwitchCurrentActionMap("Book");
                playerFollowCamera.GetComponent<CinemachineVirtualCamera>().LookAt = book.transform;
                Debug.Log("Action Map Is : " + playerInput.currentActionMap);
            }
            
            if (inputs.closeBook && bookManager.IsBookOpen())
            {
                playerInput.SwitchCurrentActionMap("Player");
                bookManager.CloseBook();
                playerFollowCamera.GetComponent<CinemachineVirtualCamera>().LookAt = null;
                Debug.Log("Action Map Is : " + playerInput.currentActionMap);
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