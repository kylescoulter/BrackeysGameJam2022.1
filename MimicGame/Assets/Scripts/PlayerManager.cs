using System;
using System.Collections;
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
        private Chest chest;
        private Boolean chestMarked;
        

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
            BaseGameManager.levelLoaded += EnablePlayer;
            
            
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

            if (inputs.interact && !chestMarked)
            {
                Debug.Log("Attempting to mark");
                chest = GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>();
                chestMarked = chest.ActivateMark();
            }

            /*if (inputs.interact && chestMarked)
            {
                Debug.Log("Removing Mark");
                chestMarked = chest.DeactivateMark();
            }*/
            
            
        }

        public void EnablePlayer()
        {
            player.SetActive(true); 
            playerFollowCamera.SetActive(true);
            //chest = BaseGameManager.chestManager.GetChest().GetComponent<Chest>();
            //Debug.Log("Chest color " + chest.chestMaterial.color);
        }

        public void DisablePlayer()
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