using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityTemplateProjects;

public class MapManager : MonoBehaviour
{
    public Button TavernBtn;
    public Button DungeonBtn;
    public Button ForestBtn;

    [SerializeField] private TextMeshProUGUI tavernPayment;
    [SerializeField] private TextMeshProUGUI dungeonPayment;
    [SerializeField] private TextMeshProUGUI forestPayment;
    [SerializeField] private TextMeshProUGUI rent;
    [SerializeField] private TextMeshProUGUI day;
    [SerializeField] private TextMeshProUGUI playerMoney;

    private PlayerManager player;
    

   
    
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Screen.lockCursor = false;

        TavernBtn.onClick.RemoveAllListeners();
        TavernBtn.onClick.AddListener(LoadTavern);
        DungeonBtn.onClick.RemoveAllListeners();
        DungeonBtn.onClick.AddListener(LoadDungeon);
        ForestBtn.onClick.RemoveAllListeners();
        ForestBtn.onClick.AddListener(LoadForest);

        
        
        
        SetPayments();
        SetPlayerValues();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadTavern()
    {
        BaseGameManager.levelLoaded?.Invoke();
        Debug.Log("Tavern Loaded");
        SceneManager.LoadScene("Tavern");
        
    }

    private void LoadDungeon()
    {
        BaseGameManager.levelLoaded?.Invoke();
        Debug.Log("Dungeon Loaded");
        SceneManager.LoadScene("Dungeon");
    }

    private void LoadForest()
    {
        
        Debug.Log("Forest Loaded");
        SceneManager.LoadScene("Forest");
        BaseGameManager.levelLoaded?.Invoke();
    }

    private void SetPayments()
    {
        tavernPayment.text = BaseGameManager.GetTavernPayment() + "p";
        dungeonPayment.text = BaseGameManager.GetDungeonPayment() + "p";
        forestPayment.text = BaseGameManager.GetForestPayment() + "p";
    }

    private void SetPlayerValues()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerManager>();
        rent.text = "RENT: 10000p";
        playerMoney.text = "MONEY: " + player.Money + "p";
        day.text = "DAY: " + player.Day;
    }
}
