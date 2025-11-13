using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    public int health;
    public Text scoreText;

    [ContextMenu("Increase Score")]
    public void addScore()
    {
        health += 1;
        scoreText.text = health.ToString();
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
