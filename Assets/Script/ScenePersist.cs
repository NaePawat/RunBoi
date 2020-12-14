using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenePersist : MonoBehaviour
{
    int startSceneIndex;
    // Start is called before the first frame update
    private void Awake()
    {
        int numScenePersist = FindObjectsOfType<ScenePersist>().Length;
        if(numScenePersist>1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        startSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }
    void Update()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        if(currentSceneIndex!=startSceneIndex)
        {
            Destroy(gameObject);
        }
    }

    public void ForceDestroy()
    {
        Destroy(gameObject);
    }
}
