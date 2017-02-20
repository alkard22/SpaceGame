using UnityEngine;

public class Orbit : MonoBehaviour
{
    public float m_orbitSpeed = 20f;
    public float m_unitRotationSpeed = 7f;

    private Transform m_parent;
    private Vector3 localRotationDirection;

    void Start ()
    {
        m_parent = this.transform.parent;
        localRotationDirection = Vector3.up;
    }

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * m_unitRotationSpeed);
        transform.RotateAround(m_parent.position, localRotationDirection, m_orbitSpeed * Time.deltaTime);
    }

    public void SetLocalRotationDirection(Vector3 direction)
    {
        localRotationDirection = direction;
    }
}
