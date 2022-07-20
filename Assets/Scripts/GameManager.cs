using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    //Variables
    [Header("References")]
    [SerializeField] private Player m_player = null;
    [SerializeField] private Generator m_generator = null;
    [SerializeField] private CollectableGenerator m_collectableGenerator = null;
    [SerializeField] private CanvasGroup m_continueButton = null;
    [SerializeField] private List<GameObject> m_onMenuObjects = null;
    [SerializeField] private List<GameObject> m_onGameObjects = null;
    [SerializeField] private TextMeshProUGUI m_scoreText = null;
    [SerializeField] private string m_scorePrefix = "Score: ";

    //Methods
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        ToMenu();
        m_continueButton.interactable = Data.game.initialized;
    }

    /// <summary>
    /// This function is called when the object becomes enabled and active.
    /// </summary>
    private void OnEnable()
    {
        Collectable.OnInteract += OnCollectableInteract;
    }

    /// <summary>
    /// This function is called when the behaviour becomes disabled or inactive.
    /// </summary>
    private void OnDisable()
    {
        Collectable.OnInteract -= OnCollectableInteract;
    }

    /// <summary>
    /// Create a new file with a new generation values.
    /// </summary>
    public void NewGame()
    {
        Data.Reset();
        Data.game.cells = m_generator.Generate();
        Data.game.playerPosition = m_player.transform.position;
        Data.game.collectables = m_collectableGenerator.Generate(Data.game.cells);

        Data.game.initialized = true;
        Data.Save();

        LoadData();
        ToGame();
    }

    /// <summary>
    /// Load the current data and set the generation values.
    /// </summary>
    public void ContinueGame()
    {
        LoadData();
        ToGame();
    }

    /// <summary>
    /// Load the data and place the environment objects.
    /// </summary>
    private void LoadData()
    {
        m_player.transform.position = Data.game.playerPosition;
        m_generator.PlaceGeneration(Data.game.cells);
        m_collectableGenerator.PlaceCollectables(Data.game.collectables);
    }

    public void ToMenu()
    {
        m_onMenuObjects.ForEach(c => c.SetActive(true));
        m_onGameObjects.ForEach(c => c.SetActive(false));
    }

    public void ToGame()
    {
        m_onMenuObjects.ForEach(c => c.SetActive(false));
        m_onGameObjects.ForEach(c => c.SetActive(true));
    }

    private void OnCollectableInteract()
    {
        m_scoreText.text = m_scorePrefix + Data.game.score;
    }
}
