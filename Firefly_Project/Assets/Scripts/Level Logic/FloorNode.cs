using UnityEngine;

public class FloorNode : MonoBehaviour
{
    public bool pure;
    [SerializeField] private Sprite darkPlant;
    [SerializeField] private Sprite purePlant;

    private void Start()
    {
        CheckState();
    }

    public void CheckState()
    {
        GetComponent<SpriteRenderer>().sprite = pure? purePlant : darkPlant;
    }
    
    public void MakeFloorPure()
    {
        pure = true;
        GetComponent<SpriteRenderer>().sprite = purePlant;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.GetComponent<PlayerController>() || pure) return;
        MakeFloorPure();
    }
}
