using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class GameOverLogic : MonoBehaviour
{
    [SerializeField] private GameObject winPanel;
    [SerializeField] private GameObject losePanel;
    
    [SerializeField] private GameObject winVideo;
    [SerializeField] private GameObject loseVideo;
    
    [SerializeField] private AudioSource _audioSource;
    
    void Start()
    {
        if (GameManager.instance.gameWon)
        {
            StartCoroutine(CloseVideo(winVideo, 21f));
            winPanel.SetActive(true);
        }
        else
        {
            StartCoroutine(CloseVideo(loseVideo, 25.2f));
            losePanel.SetActive(true);
        }
        
        BackgroundMusic.instance._audioSource.Stop();
    }

    IEnumerator CloseVideo(GameObject video, float time)
    {
        yield return new WaitForSeconds(time);
        _audioSource.Stop();
        BackgroundMusic.instance._audioSource.Play();
        video.SetActive(false);
    }
    
    public void ChangeScene(int num)
    {
        GameManager.instance.OpenScene(num);
    }
    
    public void ChangeBoolGame(bool sate)
    {
        GameManager.instance.ChangeStateContinueGame(sate);
    }
    
    public void SkipAnim(GameObject obj)
    {
        if (GameManager.instance.gameWon)
        {
            StopCoroutine(CloseVideo(winVideo, 21f));
            winVideo.SetActive(false);
        }
        else
        {
            StopCoroutine(CloseVideo(loseVideo, 25.2f));
            loseVideo.SetActive(false);
        }
        
        _audioSource.Stop();
        BackgroundMusic.instance._audioSource.Play();
        obj.SetActive(false);
    }
}
