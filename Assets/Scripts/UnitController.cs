using UnityEngine;

public class UnitController : MonoBehaviour {

    private enum State
    {
        Setup,
        SetRadius,
        Activate,
        Orbit
    }

    private State currentState;

    public Transform satelliteModel;
    public GameObject orbitRing;

	void Start ()
    {
        currentState = State.Setup;

        if(!satelliteModel) {
            Debug.LogError(this.name + " is missing reference for SatelliteModel object");
        }

        if(!orbitRing) {
            Debug.LogError(this.name + " is missing reference for OrbitRing object");
        }
    }

    void Update ()
    {
        switch(currentState) {
            case State.Setup:
                satelliteModel.GetComponent<Orbit>().enabled = false;;
                orbitRing.GetComponent<DrawOrbit>().EnableOrbitResize(true);
                currentState = State.SetRadius;
                break;
            case State.SetRadius:
                float radius = Vector3.Distance(satelliteModel.position, this.transform.position);
                orbitRing.GetComponent<DrawOrbit>().SetOrbitRadius(radius);
                orbitRing.transform.LookAt(satelliteModel);
                break;
            case State.Activate:
                orbitRing.GetComponent<DrawOrbit>().EnableOrbitResize(false);
                //Vector3 rotationDirection = orbitRing.transform.TransformDirection(Vector3.up);
                //satelliteModel.GetComponent<Orbit>().SetLocalRotationDirection(rotationDirection);
                satelliteModel.GetComponent<Orbit>().enabled = true;
                satelliteModel.GetComponent<DestoryUnitOnCollision>().enableExplosion = true;
                currentState = State.Orbit;
                break;
            case State.Orbit:
                Vector3 rotationDirection2 = orbitRing.transform.TransformDirection(Vector3.up);
                satelliteModel.GetComponent<Orbit>().SetLocalRotationDirection(rotationDirection2);
                break;
        }
    }

    public GameObject PlaceUnit()
    {
        currentState = State.Setup;
        return satelliteModel.gameObject;
    }

    public void ExitUnitPlacement()
    {
        currentState = State.Activate;
    }
}