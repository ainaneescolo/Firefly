using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class AnimationMenuPanel : MonoBehaviour
{
    [Header("----- DoTween Conf -----")] 
    private Image rectTransform;
    [SerializeField] private float animationTime;
    [SerializeField] private Ease easeType;

    private float offset = -3000f;
    
    void Start()
    {
        rectTransform = GetComponent<Image>();
    }

    private void ShowPopUp()
    {
        
    }
}
