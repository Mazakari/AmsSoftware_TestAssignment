using UnityEngine;
using UnityEngine.Splines;

public class FenceLandcapeFit : MonoBehaviour
{
    [SerializeField] private float _rayastDistance = 5f;
    public BezierKnot[] FitGround(BezierKnot[] knots, float fenceHeight)
    {
        BezierKnot[] fittedKnots = knots;
        Vector3 fittedPosition = Vector3.zero;

        for (int i = 0; i < fittedKnots.Length; i++)
        {
            fittedPosition = fittedKnots[i].Position;
            
            fittedPosition = ScanGround(fittedPosition);
            if (fittedPosition == Vector3.zero) continue;

            fittedKnots[i].Position.y = fittedPosition.y;
        }

        return fittedKnots;
    }

    private Vector3 ScanGround(Vector3 sourcePosition)
    {
        Vector3 groundPosition = Vector3.zero;
        Vector3 convertedPosition = transform.TransformPoint(sourcePosition);
        convertedPosition.y = sourcePosition.y + 5;

        Ray ray = new(convertedPosition, Vector3.down);

        if (Physics.Raycast(ray, out RaycastHit hit, _rayastDistance))
        {
            groundPosition = hit.point;
        }

        return groundPosition;
    }
}

