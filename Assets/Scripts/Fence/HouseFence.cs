using System.Linq;
using UnityEngine;
using UnityEngine.Splines;

public class HouseFence : MonoBehaviour
{
    [SerializeField] private FenceLandcapeFit _fencelandscapeFit;
    [SerializeField] private FenceBounds _fenceBounds;
    [SerializeField] private float _fenceHeight = 2f;
    [SerializeField] private SplineContainer _fenceSpline;

    [Header("Fence Edge Settings")]
    [SerializeField] private int _fenceEdgeSegments = 3;
    private float _knotStep;
    private BezierKnot[] _fenceKnots;
    

    private void OnEnable() => 
        SubscribeBuildFenceCallback();

    private void OnDisable() => 
        UnsubscribeBuildFenceCallback();

    private void SubscribeBuildFenceCallback() =>
       DesignSceneView.OnAddHouseFenceEvent += PlaceFence;
    private void UnsubscribeBuildFenceCallback() =>
        DesignSceneView.OnAddHouseFenceEvent -= PlaceFence;

    private void PlaceFence()
    {
        if (_fenceSpline.Spline.Knots.Count() > 0) return;
        _fenceBounds.CalculateFenceBounds();
        ConstructFenceSplineCorners();
        CacheSplineKnotsPositions();

        BezierKnot[] fittedKnots = _fencelandscapeFit.FitGround(_fenceKnots, _fenceHeight);
        _fenceSpline.Spline.Knots = fittedKnots;
    }

    private void ConstructFenceSplineCorners()
    {
        CalculateKnotStep();

        // Left edge
        Vector3 currentKnotPosition = _fenceBounds.LeftTopAnchor;
        AddSplineKnotAt(currentKnotPosition);

        currentKnotPosition.z -= _knotStep;
        AddSplineKnotAt(currentKnotPosition);

        currentKnotPosition.z -= _knotStep;
        AddSplineKnotAt(currentKnotPosition);

        // Bottom edge
        currentKnotPosition = _fenceBounds.LeftBottomAnchor;
        AddSplineKnotAt(currentKnotPosition);

        currentKnotPosition.x += _knotStep;
        AddSplineKnotAt(currentKnotPosition);

        currentKnotPosition.x += _knotStep;
        AddSplineKnotAt(currentKnotPosition);

        // Right edge
        currentKnotPosition = _fenceBounds.RightBottomAnchor;
        AddSplineKnotAt(currentKnotPosition);

        currentKnotPosition.z += _knotStep;
        AddSplineKnotAt(currentKnotPosition);

        currentKnotPosition.z += _knotStep;
        AddSplineKnotAt(currentKnotPosition);

        // Top edge
        currentKnotPosition = _fenceBounds.RightTopAnchor;
        AddSplineKnotAt(currentKnotPosition);

        currentKnotPosition.x -= _knotStep;
        AddSplineKnotAt(currentKnotPosition);

        currentKnotPosition.x -= _knotStep;
        AddSplineKnotAt(currentKnotPosition);
    }

    private void CalculateKnotStep()
    {
        float edgeLength = Mathf.Abs(_fenceBounds.LeftTopAnchor.z - _fenceBounds.LeftBottomAnchor.z);
        _knotStep = edgeLength / _fenceEdgeSegments;
    }

    private void AddSplineKnotAt(Vector3 position)
    {
        Vector3 invertedPosition = _fenceSpline.transform.InverseTransformPoint(position);
        invertedPosition.y = 0;
        invertedPosition.y += _fenceHeight;

        BezierKnot knot = new()
        {
            Position = invertedPosition
        };
         
        _fenceSpline.Spline.Add(knot);
    }
    private void CacheSplineKnotsPositions() => 
        _fenceKnots = _fenceSpline.Spline.ToArray();
}
