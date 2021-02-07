using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayWithoutLogginIn : MonoBehaviour
{

    [SerializeField] GameObject loginPanel;
    [SerializeField] GameObject loadingPanel;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Play()
    {
        StartCoroutine(WaitForPlay());
    }

    IEnumerator WaitForPlay()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(1);
    }
}
