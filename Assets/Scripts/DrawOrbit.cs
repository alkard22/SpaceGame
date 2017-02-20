using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class DrawOrbit : MonoBehaviour
{
    [SerializeField]
    [Tooltip("The number of lines that will be used to draw the circle. The more lines, the more the circle will be \"flexible\".")]
    [Range(0, 1000)]
    private int _segments = 60;

    [SerializeField]
    [Tooltip("The radius of the horizontal axis.")]
    private float _horizRadius = 10;

    [SerializeField]
    [Tooltip("The radius of the vertical axis.")]
    private float _vertRadius = 10;

    [SerializeField]
    [Tooltip("The offset will be applied in the direction of the axis.")]
    private float _offset = 0;

    [SerializeField]
    [Tooltip("If checked, the circle will be rendered again each time one of the parameters change.")]
    private bool _orbitResize = false;

    private int _previousSegmentsValue;

    private LineRenderer _line;

    void Start()
    {
        _line = gameObject.GetComponent<LineRenderer>();
        _line.numPositions = (_segments + 1);
        _previousSegmentsValue = _line.numPositions;
        _line.useWorldSpace = false;
        _offset = 0;

        CreatePoints();
    }

    void Update()
    {
        if(_orbitResize) {
            CreatePoints();
        }
    }

    public void SetOrbitRadius(float radius)
    {
        _vertRadius = radius;
        _horizRadius = radius;
    }

    public void EnableOrbitResize(bool b)
    {
        _orbitResize = b;
    }

    void CreatePoints()
    {
        if(_previousSegmentsValue != _segments) {
            _line.numPositions = (_segments + 1);
            _previousSegmentsValue = _line.numPositions;
        }

        float x = 0; // Could change this to another objects position if we want don't want this to be a child
        float y = 0; // Could change this to another objects position if we want don't want this to be a child
        float z = _offset;

        float angle = 0f;

        for(int i = 0; i < (_segments + 1); i++) {
            x = Mathf.Sin(Mathf.Deg2Rad * angle) * _horizRadius;
            y = Mathf.Cos(Mathf.Deg2Rad * angle) * _vertRadius;

            _line.SetPosition(i, new Vector3(y, z, x));

            angle += (360f / _segments);
        }
    }
}
