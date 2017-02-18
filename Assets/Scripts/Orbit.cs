using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    #region member
    private float m_orbitSpeed = 8f;
    private Transform m_parent;
    #endregion

    #region mono
    // Use this for initialization
    void Start () {
        m_parent = this.transform.parent;
	}
	
	// Update is called once per frame
	void Update () {
        transform.Rotate(Vector3.up * Time.deltaTime * 3f);

        transform.RotateAround(m_parent.transform.position, Vector3.up, m_orbitSpeed * Time.deltaTime);
    }
    #endregion
}
