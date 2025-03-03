using UnityEngine;

public class PlayerCollide : MonoBehaviour
{
    [SerializeField] private PlayerScript script;

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Spoiler"))
        {
            script.state = PlayerScript.PlayerState.GoToTrunk;
            GameManager.Instance.inventoryManager.IsOpen(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Spoiler"))
        {
            script.state = PlayerScript.PlayerState.Outside;
            GameManager.Instance.inventoryManager.IsOpen(false);
        }
    }
}