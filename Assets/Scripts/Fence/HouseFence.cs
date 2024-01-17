using UnityEngine;
using UnityEngine.Splines;
using UnityEngine.UIElements;

public class HouseFence : MonoBehaviour
{
    [SerializeField] private FenceBounds _fenceBounds;
    [SerializeField] private float _fenceHeight = 2f;
    [SerializeField] private SplineContainer _fenceSpline;

    private BezierKnot[] _fenceKnots;

    private void Start()
    {
        try
        {
            _fenceBounds.CalculateFenceBounds();
            CacheSplineKnotsPositions();

            ConstructFenceSplineCorners();
            //PlaceFence();
        }
        catch (System.Exception e)
        {

            Debug.Log(e.Message);
        }
    }

   

    private void CacheSplineKnotsPositions() => 
        _fenceKnots = _fenceSpline.Spline.ToArray();
    
    private void ConstructFenceSplineCorners()
    {
        AddSplineKnotAt(_fenceBounds.LeftTopAnchor);
        AddSplineKnotAt(_fenceBounds.LeftBottomAnchor);
        AddSplineKnotAt(_fenceBounds.RightBottomAnchor);
        AddSplineKnotAt(_fenceBounds.RightTopAnchor);
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

    private void PlaceFence()
    {
        for (int i = 0; i < _fenceKnots.Length; i++)
        {
            Debug.Log($"_fenceKnots[i].Position = {_fenceKnots[i].Position}");
            Debug.Log($"_fenceSpline.Spline.ToArray()[i] = {_fenceSpline.Spline.ToArray()[i].Position}");
        }
    }
}
