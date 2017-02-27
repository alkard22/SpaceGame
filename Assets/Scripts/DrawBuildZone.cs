using UnityEngine;

[RequireComponent(typeof(LineRenderer))]

public class DrawBuildZone : MonoBehaviour
{
#if UNITY_EDITOR
    //[SerializeField]
    [Tooltip("The number of lines that will be used to draw the circle. The more lines, the more the circle will be \"flexible\".")]
    [Range(0, 1000)]
    private int _segments = 60;

    //[SerializeField]
    [Tooltip("The radius of the horizontal axis.")]
    private float _horizRadius = 10;

    //[SerializeField]
    [Tooltip("The radius of the vertical axis.")]
    private float _vertRadius = 10;

    private LineRenderer _line;

    private float minRadius = 0;
    private float maxRadius = 0;

    void Awake()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        
        _line.numPositions = _segments + 1;
        _line.useWorldSpace = false;
    }

    public void SetBuildZoneMinMax(float min, float max)
    {
        minRadius = min;
        maxRadius = max;
        CreatePoints();
    }

    public void DisplayBuildZone(bool b)
    {
        _line.enabled = b;
    }

    void CreatePoints()
    {
        // Find the min and max mid point to create line
        float radiusMidPoint = (minRadius + maxRadius) / 2;
        _horizRadius = radiusMidPoint;
        _vertRadius = radiusMidPoint;

        // Increase width to fill the space between min and max
        _line.widthMultiplier = maxRadius - minRadius;

        float x;
        float y;
        float z = 0;

        float angle = 0f;

        for(int i = 0; i < (_segments + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _horizRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * _vertRadius;

            // Builds the line on the Z axis
            _line.SetPosition(i, new Vector3(x, y, z));

            angle += (360f / _segments);
        }

        // Quick fix to handle the large line width not correctly connecting at the ends
        _line.SetPosition(0, new Vector3(0, _line.GetPosition(1).y, 0));
        _line.SetPosition(_segments, new Vector3(0, _line.GetPosition(1).y, 0));
    }
#endif
}