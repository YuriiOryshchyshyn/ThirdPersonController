using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    [SerializeField] private Transform target;

    [SerializeField] private float rotateSpeed = 1.5f;

    private float _rotationY;

    private Vector3 _offset;
    public float z;
    public float y;
    public float x;

    private void Start()
    {
        _rotationY = transform.eulerAngles.y;
        _offset = target.position - transform.position;
    }

    private void LateUpdate()
    {

        _rotationY += Input.GetAxis("Mouse X") * rotateSpeed * 3;

        Quaternion rotation = Quaternion.Euler(0, _rotationY, 0);
        transform.position = target.position - (rotation * new Vector3(x, y, z));
        Vector3 targetLook = new Vector3(target.position.x, target.position.y + 1.5f, target.position.z);
        transform.LookAt(targetLook);
    }
}
