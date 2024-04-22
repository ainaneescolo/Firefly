using UnityEngine;

public class Water : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.GetComponent<PlayerController>()) return;
        GameManager.instance.OpenScene(1);
    }
}
