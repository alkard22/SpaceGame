using UnityEngine;

public class Orbit : MonoBehaviour
{
    #region member
    public float m_orbitSpeed = 8f;
    public Transform m_parent;
    #endregion

    #region mono
    void Start ()
    {
        //m_parent = this.transform.parent;
	}

    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * 3f);
        transform.RotateAround(m_parent.transform.position, Vector3.up, m_orbitSpeed * Time.deltaTime);
    }
    #endregion
}
