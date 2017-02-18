using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthGravity : MonoBehaviour {

    #region members
    [SerializeField]
    private float m_pullRadius = 5;
    [SerializeField]
    private float m_pullForce = 81;
    #endregion

    #region mono
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    //used for regidbody
    void FixedUpdate()
    {
        foreach(Collider collider in Physics.OverlapSphere(transform.position, m_pullRadius))
        {
            //only apply gravity if its not IgnoreGravity
            if(!collider.CompareTag("IgnoreGravity")) 
            {
                // calculate direction from target to me
                Vector3 forceDirection = transform.position - collider.transform.position;

                // apply force on target towards me
                Rigidbody rb = collider.GetComponent<Rigidbody>();
                rb.AddForce(forceDirection.normalized * m_pullForce * Time.fixedDeltaTime);
            }           
            
        }
    }
    #endregion

    #region properties
    public float PullRadius
    {
        get { return m_pullRadius; }
        set { m_pullRadius = value; }
        
    }

    public float PullForce
    {
        get { return m_pullForce; }
        set { m_pullForce = value; }

    }
    #endregion
}
