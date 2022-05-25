using UnityEngine;

public class DeviceOperator : MonoBehaviour
{
    [SerializeField] private Transform door;
    [SerializeField] private float _radius = 1.5f;

    private void Update()
    {
        if (Input.GetButtonDown("Fire3"))
        {
            Collider[] hitColliders = Physics.OverlapSphere(transform.position, _radius);
            foreach (Collider hitCollider in hitColliders)
            {
                Vector3 direction = hitCollider.transform.position - transform.position;
                if (Vector3.Dot(transform.forward, direction) > .5f)
                {
                    hitCollider.SendMessage("Operate",
                    SendMessageOptions.DontRequireReceiver);
                }
            }
        }
    }
}
