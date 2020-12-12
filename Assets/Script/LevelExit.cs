using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 2f;
    [SerializeField] float SlowMoSpeed = 0.2f;
    Collider2D exitCollider;
    // Start is called before the first frame update
    private void OnTriggerEnter2D(Collider2D collision)
    {
        exitCollider = GetComponent<BoxCollider2D>();
        if (exitCollider.IsTouchingLayers(LayerMask.GetMask("Player")))
        {
            StartCoroutine(LoadLevel());
        }
    }
    IEnumerator LoadLevel()
    {
        Time.timeScale = SlowMoSpeed;
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        Time.timeScale = 1f;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
