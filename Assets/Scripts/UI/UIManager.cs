using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] AudioClip clickSound;
    [Header("Win")]
    [SerializeField] GameObject winScreen;
    [SerializeField] AudioClip winSound;
    [Header("Pause")]
    [SerializeField] GameObject pauseScreen;
    [SerializeField] AudioClip pauseSound;
    GameObject player;
    public bool winState;
    public bool pauseState;
    void Start()
    {
        winScreen.SetActive(false);
        pauseScreen.SetActive(false);
        pauseState= false;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Win()
    {
        winScreen.SetActive(true);
        SoundManager.instance.PlaySound(winSound);
    }

    public void Menu()
    {
        SoundManager.instance.PlaySound(clickSound);
        StartCoroutine(MenuDelay());
    }
    IEnumerator MenuDelay()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(0);
    }

    public void Restart()
    {
        SoundManager.instance.PlaySound(clickSound);
        player.transform.position = Vector3.zero;
        winScreen.SetActive(false);
    }

    public void Exit()
    {
        SoundManager.instance.PlaySound(clickSound);
        StartCoroutine(ExitDelay());
    }
    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(0.25f);
        Application.Quit();
        UnityEditor.EditorApplication.isPlaying = false;
    }
    public void Resume()
    {
        SoundManager.instance.PlaySound(pauseSound);
        pauseScreen.SetActive(false);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(pauseScreen.activeSelf == true)
            {
                Resume();
            } else
            {
                SoundManager.instance.PlaySound(pauseSound);
                pauseScreen.SetActive(true);
            }
        }
        winState = winScreen.activeSelf;
        pauseState = pauseScreen.activeSelf;
    }
}
