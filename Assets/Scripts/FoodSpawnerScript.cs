using System.Security.Cryptography;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class FoodSpawnerScript : MonoBehaviour
{
    public GameObject food;
    public float spawnRate = 2;
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
