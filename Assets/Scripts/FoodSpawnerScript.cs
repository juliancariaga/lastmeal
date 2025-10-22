using System.Security.Cryptography;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FoodSpawnerScript : MonoBehaviour
{
    public GameObject food;
    public float spawnRate = 2;
    public float insanemode = 0;
    private float timer = 0;
    public float deadZone = -9;
    float heightOffset = 10;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < spawnRate)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawnFood();
            timer = 0;
            insanemode += 1;
        }

        if (insanemode == 8) {
            spawnRate = 1;
        }

        if (transform.position.y < deadZone)
        {
            Destroy(gameObject);
        }
    }

    void spawnFood()
    {
        float lowestPoint = transform.position.x - heightOffset;
        float highestPoint = transform.position.x + heightOffset;
        Instantiate(food, new Vector3(Random.Range(lowestPoint, highestPoint), transform.position.y), transform.rotation);
    }
}
