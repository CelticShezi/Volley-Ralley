using System.Collections;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject ball;
    public TextMeshProUGUI scoreText;

    private int score = 0;
    private bool gameActive = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        InvokeRepeating("SpawnBall", 2, 5);
        scoreText.text = "Score: " + score;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegisterHit()
    {
        score++;
        scoreText.text = "Score: " + score;
    }

    public void EndGame()
    {
        gameActive = false;
        Debug.Log("Game Over");
    }

    private void SpawnBall()
    {
        if (gameActive)
        {
            Instantiate(ball, GetSpawnPosition(), ball.transform.rotation);
        }
    }

    Vector3 GetSpawnPosition()
    {
        return new Vector3(Random.Range(-4.5f, 4.5f), 0, 9);
    }
}
