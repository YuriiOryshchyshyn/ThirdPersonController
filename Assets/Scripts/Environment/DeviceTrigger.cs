using UnityEngine;

public class DeviceTrigger : MonoBehaviour
{
    [SerializeField] private GameObject[] triggers;

    public bool RequireKey;

    private void OnTriggerEnter(Collider other)
    {
        if (RequireKey && Managers.Inventory.equippedItem != "Key")
        {
            return;
        }

        foreach (GameObject trigger in triggers)
        {
            trigger.SendMessage("Activate");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        foreach (GameObject trigger in triggers)
        {
            trigger.SendMessage("Deactivate");
        }
    }
}
