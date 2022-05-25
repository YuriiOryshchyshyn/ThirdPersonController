using UnityEngine;

public class Rotation : MonoBehaviour
{
    public Transform projector;

    private Quaternion start;
    private Quaternion end;

    private void Start()
    {
        start = Quaternion.identity;
        end = Quaternion.Euler(0, 90, 0);
    }

    private void Update()
    {
        projector.rotation = Quaternion.LerpUnclamped(start, end, Time.time);
    }
}
