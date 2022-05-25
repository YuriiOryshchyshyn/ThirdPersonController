using UnityEngine;

public class DoorOpenDevice : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private Vector3 doorPosition;
    [SerializeField] private Vector3 doorRotation;

    private bool _open;

    public void Operate()
    {
        if (_open)
        {
            Vector3 position = door.position - doorPosition;
            door.position = position;
        }
        else
        {
            Vector3 position = door.position + doorPosition;
            door.position = position;
        }

        _open = !_open;
    }

    public void Activate()
    {
        if (!_open)
        {
            door.rotation *= Quaternion.Euler(doorRotation);
        }
        _open = true;
    }

    public void Deactivate()
    {
        if (_open)
        {
            door.rotation *= Quaternion.Euler(-doorRotation);
        }
        _open = false;
    }
}
