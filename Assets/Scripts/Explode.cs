using UnityEngine;

public class Explode : MonoBehaviour
{
    public float speed = 0.2f;
    public float explosionTimeLength = 7f;

    void Start () {
        Destroy(this.gameObject, explosionTimeLength);
    }
	
	void Update () {
        this.transform.Rotate(RandomAxis(), RandomAxis(), RandomAxis());
        this.transform.localScale += Vector3.one * speed * Time.deltaTime;
    }

    private float RandomAxis()
    {
        return Random.Range(0f, 50f) * Time.deltaTime;
    }
}
