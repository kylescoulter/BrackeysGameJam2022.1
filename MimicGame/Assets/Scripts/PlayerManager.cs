using System;
using System.Collections;
using Cinemachine;
using StarterAssets;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

        private static int day;
        private static int money;
        private bool turningPage;

        private bool phoneHasBeenOpened;
        
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

            day = 1;
            money = 0;
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
                if (bookManager.ReadyToOpen)
                {
                    bookManager.OpenBook();
                    playerInput.SwitchCurrentActionMap("Book");
                    playerFollowCamera.GetComponent<CinemachineVirtualCamera>().LookAt = book.transform;
                    Debug.Log("Action Map Is : " + playerInput.currentActionMap);
                }
              
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
                if (inputs.interact && exitDoor.GetLeavable() && !phoneManager.IsPhoneOpen() && !phoneHasBeenOpened)
                {
                    phoneHasBeenOpened = true;
                    exitDoor.ToggleHint(false);
                    phoneManager.OpenPhone();
                    playerFollowCamera.GetComponent<CinemachineVirtualCamera>().LookAt = GameObject.FindGameObjectWithTag("PhoneTarget").transform;
                    playerInput.SwitchCurrentActionMap("Phone");
                }
            }

            if (inputs.exit && exitDoor.GetLeavable() && phoneManager.IsPhoneOpen())
            {
                if (chestMarked)
                {
                    chestMarked = false;
                }
                playerInput.SwitchCurrentActionMap("Player");
                phoneHasBeenOpened = false;
                Debug.Log("Loading map from phone");
                phoneManager.ClosePhone(false);
                DisablePlayer();
                BaseGameManager.mapLoaded?.Invoke();
                playerFollowCamera.GetComponent<CinemachineVirtualCamera>().LookAt = null;
                if (day < 14)
                {
                    SceneManager.LoadScene("Map");
                }
                else
                {
                    SceneManager.LoadScene("End");
                }
                day++;
            }

            if (inputs.nextPage)
            {
                
                if (!turningPage)
                {
                    Debug.Log("Attempting next page");
                    StartCoroutine(TurnNextPage());
                }
                
            }
            
            if (inputs.prevPage)
            {
                
                if (!turningPage)
                {
                    Debug.Log("Attempting previous page");
                    StartCoroutine(TurnPrevPage());
                }
                
            }

            if (inputs.escapeGame)
            {
                Application.Quit();
            }
        }

        public IEnumerator TurnNextPage()
        {
            turningPage = true;
            bookManager.NextPage();
            yield return new WaitForSeconds(.25f);
            turningPage = false;
        }
        
        public IEnumerator TurnPrevPage()
        {
            turningPage = true;
            bookManager.PrevPage();
            yield return new WaitForSeconds(.25f);
            turningPage = false;
        }

        public IEnumerator EnablePlayer()
        {
            player.SetActive(true); 
            playerFollowCamera.SetActive(true);
            player.GetComponent<Inputs>().cursorLocked = false;
            
            yield return new WaitForSeconds(5);
            
            chest = GameObject.FindGameObjectWithTag("Chest").GetComponent<Chest>();
            exitDoor = GameObject.FindGameObjectWithTag("ExitDoor").GetComponent<ExitDoor>();
            
        }

        public void DisablePlayer()
        {
            player.GetComponent<Inputs>().cursorLocked = false;
            player.SetActive(false);
            playerFollowCamera.SetActive(false);
        }

        public static GameObject GetPlayer()
        {
            return player;
        }

        public static void AddMoney(int moneyIn)
        {
            Debug.Log("Adding money to player");
            money += moneyIn;
        }

        public static int Money => money;

        public static int Day => day;
    }
}