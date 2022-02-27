using System;
using System.Collections;
using Cinemachine;
using StarterAssets;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace UnityTemplateProjects
{
    public class PlayerManager : MonoBehaviour
    {
        //
        private GameObject mainCamera;


        private GameObject player;
        private GameObject playerFollowCamera;
        private Inputs inputs;
        private PlayerInput playerInput;
        private GameObject book;
        private GameObject phone;
        private BookManager bookManager;
        private PhoneManager phoneManager;
        private GameObject chestObj;
        private Chest chest;
        private Boolean chestMarked;
        private ExitDoor exitDoor;

        private int day;
        private int money;
        
        private void Awake()
        {
            
        }

        private void Start()
        {
            player = gameObject;
            playerFollowCamera = GameObject.FindGameObjectWithTag("PlayerFollowCamera");

            book = player.GetComponentInChildren<BookManager>().gameObject;
            bookManager = book.GetComponent<BookManager>(); 
            
            phone = player.GetComponentInChildren<PhoneManager>().gameObject;
            phoneManager = phone.GetComponent<PhoneManager>();
            
            playerInput = player.GetComponent<PlayerInput>();
            inputs = player.GetComponent<Inputs>();

            day = 1;
            money = 0;
            EnablePlayer();


            //DisablePlayer();
            /*DontDestroyOnLoad(player);
            DontDestroyOnLoad(playerFollowCamera);
            DontDestroyOnLoad(mainCamera);*/
        }

        private void Update()
        {
            if (chest == null)
            {
                chestObj = GameObject.FindGameObjectWithTag("Chest");
                if (chestObj != null)
                {
                    chest = chestObj.GetComponent<Chest>();
                    exitDoor = GameObject.FindGameObjectWithTag("ExitDoor").GetComponent<ExitDoor>(); 
                }
                
            }
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
            }

            if (inputs.exit && exitDoor.GetLeavable() && phoneManager.IsPhoneOpen())
            {
                playerInput.SwitchCurrentActionMap("Player");
                Debug.Log("Loading map from phone");
                phoneManager.ClosePhone(false);
                DisablePlayer();
                BaseGameManager.mapLoaded?.Invoke();
                playerFollowCamera.GetComponent<CinemachineVirtualCamera>().LookAt = null;
                SceneManager.LoadScene("Map");
                day++;
            }
            
            
        }

        public void EnablePlayer()
        {
            player.SetActive(true); 
            //playerFollowCamera.SetActive(true);
        }

        public void DisablePlayer()
        {
            
            player.GetComponent<Inputs>().cursorLocked = false;
            //player.SetActive(false);
            //playerFollowCamera.SetActive(false);
        }

        public GameObject GetPlayer()
        {
            return player;
        }

        public void AddMoney(int moneyIn)
        {
            Debug.Log("Adding money to player");
            money += moneyIn;
        }

        public int Money => money;

        public int Day => day;
    }
}