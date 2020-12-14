using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLive = 3;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;
    private void Awake()
    {
        int numGameSession = FindObjectsOfType<GameSession>().Length;
        if(numGameSession>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = playerLive.ToString();
        scoreText.text = score.ToString();
    }
    public void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }
    public void ProcessPlayerDeath()
    {
        if(playerLive>1)
        {
            TakeLife();
        } else
        {
            ResetGameSession();
        }
    }
    public void ResetGameSession()
    {
        Destroy(gameObject);
        SceneManager.LoadScene(0);
    }
    private void TakeLife()
    {
        playerLive--;
        livesText.text = playerLive.ToString();
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
