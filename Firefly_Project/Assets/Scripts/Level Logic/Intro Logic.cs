using System.Collections;
using UnityEngine;

public class IntroLogic : MonoBehaviour
{
    [SerializeField] private GameObject canvasMenu;
    
    void Start()
    {
        BackgroundMusic.instance._audioSource.Stop();
        canvasMenu.SetActive(false);
        StartCoroutine(StartGame());
    }

    public void SkipAnim()
    {
        StopCoroutine(StartGame());
        BackgroundMusic.instance._audioSource.Play();
        canvasMenu.SetActive(true);
        gameObject.SetActive(false);
    }
    
    IEnumerator StartGame()
    {
        yield return new WaitForSeconds(21f);
        //_audioSource.Stop();
        BackgroundMusic.instance._audioSource.Play();
        canvasMenu.SetActive(true);
        gameObject.SetActive(false);
    }
}
