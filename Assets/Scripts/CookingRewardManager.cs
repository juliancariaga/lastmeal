using UnityEngine;
using UnityEngine.SceneManagement;

public class CookingRewardManager : MonoBehaviour
{
    [Header("References")]
    public LogicScript logic;          // score from the minigame

    [Header("Settings")]
    public int goldPerCatch = 1;       // how much gold each caught food is worth
    public string daySceneName = "DayScene";

    [Header("Timer Settings")]
    public float minTime = 5f;
    public float maxTime = 10f;

    private float timer;

    void Start()
    {
        // Pick a random amount of time between minTime and maxTime
        timer = Random.Range(minTime, maxTime);
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            FinishMinigame();
        }
    }

    public void FinishMinigame()
    {
        if (GameManager.Instance == null)
        {
            SceneManager.LoadScene(daySceneName);
            return;
        }

        // How many foods the player caught
        int score = logic != null ? logic.playerScore : 0;
        int goldEarned = score * goldPerCatch;

        // Add gold to the shared GameManager value
        GameManager.Instance.gold += goldEarned;

        Debug.Log($"Mini-game finished. Score = {score}, Gold earned = {goldEarned}, Total gold = {GameManager.Instance.gold}");

        // Return to Day scene
        SceneManager.LoadScene(daySceneName);
    }
}
