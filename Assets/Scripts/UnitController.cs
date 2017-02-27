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
    public GameObject trajectoryPath;
    public Material trajectoryLine;
    public Material trajectoryLineValid;
    public Material trajectoryLineInvalid;

    private float currentUnitRadius = 0;
    private float minBuildRadius = 0;
    private float maxBuildRadius = 0;

    void Start ()
    {
        currentState = State.Setup;

        if(!satelliteModel) {
            Debug.LogError(this.name + " is missing reference for satelliteModel object");
        }

        if(!trajectoryPath) {
            Debug.LogError(this.name + " is missing reference for trajectoryPath object");
        }

        if(!trajectoryLine) {
            Debug.LogError(this.name + " is missing reference for trajectoryLine material");
        }

        if(!trajectoryLineValid) {
            Debug.LogError(this.name + " is missing reference for trajectoryLineValid material");
        }

        if(!trajectoryLineInvalid) {
            Debug.LogError(this.name + " is missing reference for trajectoryLineInvalid material");
        }
    }

    void Update ()
    {
        switch(currentState) {
            case State.Setup:
                satelliteModel.GetComponent<Orbit>().enabled = false;;
                trajectoryPath.GetComponent<DrawOrbit>().EnableOrbitResize(true);
                currentState = State.SetRadius;
                break;
            case State.SetRadius:
                currentUnitRadius = Vector3.Distance(satelliteModel.position, this.transform.position);

                if(currentUnitRadius < minBuildRadius || currentUnitRadius > maxBuildRadius) {
                    trajectoryPath.GetComponent<LineRenderer>().material = trajectoryLineInvalid;
                } else {
                    trajectoryPath.GetComponent<LineRenderer>().material = trajectoryLineValid;
                }

                trajectoryPath.GetComponent<DrawOrbit>().SetOrbitRadius(currentUnitRadius);
                trajectoryPath.transform.LookAt(satelliteModel);
                break;
            case State.Activate:
                trajectoryPath.GetComponent<LineRenderer>().material = trajectoryLine;
                trajectoryPath.GetComponent<DrawOrbit>().EnableOrbitResize(false);
                //Vector3 rotationDirection = orbitRing.transform.TransformDirection(Vector3.up);
                //satelliteModel.GetComponent<Orbit>().SetLocalRotationDirection(rotationDirection);
                satelliteModel.GetComponent<Orbit>().enabled = true;
                satelliteModel.GetComponent<DestoryUnitOnCollision>().enableExplosion = true;
                currentState = State.Orbit;
                break;
            case State.Orbit:
                Vector3 rotationDirection2 = trajectoryPath.transform.TransformDirection(Vector3.up);
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
        if(currentUnitRadius < minBuildRadius || currentUnitRadius > maxBuildRadius) {
            Destroy(this.gameObject);
        } else {
            currentState = State.Activate;
        }
    }

    public void SetBuildZoneMinMax(float min, float max)
    {
        minBuildRadius = min;
        maxBuildRadius = max;
    }
}