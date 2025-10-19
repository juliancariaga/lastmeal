using UnityEngine;

public class FoodScript : MonoBehaviour
{

    public float deadZoneY = -9f;
    public LogicScript logic;

    void Start()
    {
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();

    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < deadZoneY)
            Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        logic.addScore();
        Destroy(gameObject);
    }
}
