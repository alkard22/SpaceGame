using UnityEngine;

public class DestoryUnitOnCollision : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    public bool enableExplosion = false;

    // Use collisionInfo if I want to know which object collided, exact point of contact
    /*private void OnCollisionEnter(Collision collision)
    {
        ContactPoint contact = collision.contacts[0];
        Quaternion rot = Quaternion.FromToRotation(Vector3.up, contact.normal);
        Vector3 pos = contact.point;
        Instantiate(explosionEffectPrefab, pos, rot);
        Destroy(this.transform.parent.gameObject);
    }*/


    private void OnTriggerEnter(/*Collider other*/)
    {
        if(explosionEffectPrefab && enableExplosion) {
            Instantiate(explosionEffectPrefab, this.transform.position, Quaternion.identity);
            Destroy(this.transform.parent.gameObject);
        }
    }
}
