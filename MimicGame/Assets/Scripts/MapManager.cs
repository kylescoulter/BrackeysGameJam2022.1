using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityTemplateProjects;

public class MapManager : MonoBehaviour
{
    public Button TavernBtn;
    public Button DungeonBtn;
    public Button ForestBtn;

   
    
    // Start is called before the first frame update
    void Start()
    {
        TavernBtn.onClick.AddListener(LoadTavern);
        DungeonBtn.onClick.AddListener(LoadDungeon);
        ForestBtn.onClick.AddListener(LoadForest);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void LoadTavern()
    {
        SceneManager.LoadScene("Tavern");
        BaseGameManager.levelLoaded?.Invoke();
    }

    private void LoadDungeon()
    {
        SceneManager.LoadScene("Dungeon");
        BaseGameManager.levelLoaded?.Invoke();
    }

    private void LoadForest()
    {
        SceneManager.LoadScene("Forest");
        BaseGameManager.levelLoaded?.Invoke();
    }
}
