using System.Security.Cryptography;
using Unity.Collections;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class DogSpawnerScript : MonoBehaviour
{
    [Header("Spawn Settings")]
    public GameObject dogPrefab;       // prefab to spawn
    public float spawnRate = 2f;       // seconds between spawns
    public float spawnRadius = 8f;     // how far from the player to spawn
    public int maxDogs = 20;           // optional cap on total dogs

    private float timer = 0f;
    private Transform player;          // reference to player transform

    void Start()
    {
        GameObject found = GameObject.FindGameObjectWithTag("Player");
        if (found != null)
            player = found.transform;
        else
            Debug.LogWarning("DogSpawnerScript: No GameObject tagged 'Player' found!");
    }

    void Update()
    {
        if (player == null) return;

        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnDog();
            timer = 0f;
        }
    }

    void SpawnDog()
    {

        if (maxDogs > 0 && GameObject.FindGameObjectsWithTag("Enemy").Length >= maxDogs)
            return;

        // choose a random angle + distance around player
        float angle = Random.Range(0f, Mathf.PI * 2f);
        float distance = Random.Range(spawnRadius * 0.5f, spawnRadius);
        Vector2 offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;

        Vector3 spawnPos = player.position + new Vector3(offset.x, offset.y, 0f);

        Instantiate(dogPrefab, spawnPos, Quaternion.identity);
    }
}
