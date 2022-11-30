using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUi : MonoBehaviour
{
    [SerializeField] AudioClip click;
    [Header("Information")]
    [SerializeField] GameObject InfoScreen;
    void Start()
    {
        InfoScreen.SetActive(false);
    }
    public void Play()
    {
        SoundManager.instance.PlaySound(click);
        StartCoroutine(PlayDelay());
    }
    IEnumerator PlayDelay()
    {
        yield return new WaitForSeconds(0.25f);
        SceneManager.LoadScene(1);
    }
    public void Exit()
    {
        SoundManager.instance.PlaySound(click);
        StartCoroutine(ExitDelay());
    }
    IEnumerator ExitDelay()
    {
        yield return new WaitForSeconds(0.25f);
        Application.Quit();
    }
    public void Info()
    {
        SoundManager.instance.PlaySound(click);
        InfoScreen.SetActive(true);
    }
    public void InfoExit()
    {
        SoundManager.instance.PlaySound(click);
        InfoScreen.SetActive(false);
    }
}
