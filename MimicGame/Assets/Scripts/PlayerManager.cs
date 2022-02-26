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


        private static GameObject player;
        private static GameObject playerFollowCamera;
        private Inputs inputs;
        private PlayerInput playerInput;
        private GameObject book;
        private GameObject phone;
        private BookManager bookManager;
        private PhoneManager phoneManager;
        private Chest chest;
        private Boolean chestMarked;
        private ExitDoor exitDoor;
        

        private void Awake()
        {
            player = Instantiate(playerPrefab, Vector3.zero, Quaternion.identity);
            player.GetComponent<Inputs>().cursorLocked = false;
            playerFollowCamera = GameObject.FindGameObjectWithTag("PlayerFollowCamera");
            playerFollowCamera.GetComponent<CinemachineVirtualCamera>().Follow =
                GameObject.FindGameObjectWithTag("CinemachineTarget").transform;

            book = player.GetComponentInChildren<BookManager>().gameObject;
            bookManager = book.GetComponent<BookManager>(); 
            
            phone = player.GetComponentInChildren<PhoneManager>().gameObject;
            phoneManager = phone.GetComponent<PhoneManager>();
            
            playerInput = player.GetComponent<PlayerInput>();
            inputs = player.GetComponent<Inputs>();

            
        }

        private void Start()
        {
            BaseGameManager.levelLoaded = delegate { StartCoroutine(EnablePlayer()); };
            
            
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

            if (chest != null)
            {
                if (inputs.interact && !chestMarked)
                {
                    chestMarked = chest.ActivateMark();
                }
            }
            

            /*if (inputs.interact && chestMarked)
            {
                Debug.Log("Removing Mark");
                chestMarked = chest.DeactivateMark();
            }*/
            if (exitDoor != null)
            {
                if (inputs.interact && exitDoor.GetLeavable() && !phoneManager.IsPhoneOpen())
                {
                    exitDoor.ToggleHint(false);
                    phoneManager.OpenPhone();
                    playerFollowCamera.GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.FindGameObjectWithTag("PhoneTarget").transform;
                    playerInput.SwitchCurrentActionMap("Phone");
                }
                
                if (inputs.interact && phoneManager.IsPhoneOpen())
                {
                    Debug.Log("Closing Phone");
                    exitDoor.ToggleHint(true);
                    phoneManager.ClosePhone();
                    playerFollowCamera.GetComponent<CinemachineVirtualCamera>().LookAt = null;
                    playerInput.SwitchCurrentActionMap("Player");
                }
            }
        }

        public IEnumerator EnablePlayer()
        {
            player.SetActive(true); 
            playerFollowCamera.SetActive(true);
            
            yield return new WaitForSeconds(5);
            
            chest = GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>();
            exitDoor = GameObject.FindGameObjectWithTag("ExitDoor").GetComponent<ExitDoor>();
            
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