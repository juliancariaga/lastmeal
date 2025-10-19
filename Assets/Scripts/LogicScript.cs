using UnityEngine;
using UnityEngine.UI;

public class LogicScript : MonoBehaviour
{

    public int playerScore;
    public Text scoreText;

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        playerScore += 1;
        scoreText.text = playerScore.ToString();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
