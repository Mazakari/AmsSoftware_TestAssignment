using UnityEngine;
using UnityEngine.Splines;

public class HouseFence : MonoBehaviour
{
    [SerializeField] private FenceLandcapeFit _fencelandscapeFit;
    [SerializeField] private FenceBounds _fenceBounds;
    [SerializeField] private float _fenceHeight = 2f;
    [SerializeField] private SplineContainer _fenceSpline;

    private BezierKnot[] _fenceKnots;

    private void Start()
    {
        try
        {
            _fenceBounds.CalculateFenceBounds();
            ConstructFenceSplineCorners();
            CacheSplineKnotsPositions();

            PlaceFence();
        }
        catch (System.Exception e)
        {
            Debug.Log(e.Message);
        }
    }
    
    private void ConstructFenceSplineCorners()
    {
        AddSplineKnotAt(_fenceBounds.LeftTopAnchor);
        AddSplineKnotAt(_fenceBounds.LeftBottomAnchor);
        AddSplineKnotAt(_fenceBounds.RightBottomAnchor);
        AddSplineKnotAt(_fenceBounds.RightTopAnchor);
    }
    private void AddSplineKnotAt(Vector3 position)
    {
        //Vector3 invertedPosition = _fenceSpline.transform.InverseTransformPoint(position);
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

    private void PlaceFence()
    {
        BezierKnot[] fittedKnots = _fencelandscapeFit.FitGround(_fenceKnots, _fenceHeight);
        _fenceSpline.Spline.Knots = fittedKnots;
    }
}