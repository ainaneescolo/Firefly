using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionUI : MonoBehaviour
{
    [SerializeField] private GameObject panel;

    public void DeactivatePanel()
    {
        panel.SetActive(false);
    }
}
