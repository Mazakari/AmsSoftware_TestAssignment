using UnityEngine;

public class FenceBounds : MonoBehaviour
{
    public int SingleFenceSegmentResolution { get; private set; } = 3;

    public Vector3 LeftTopAnchor { get; private set; }
    public Vector3 LeftBottomAnchor { get; private set; }
    public Vector3 RightTopAnchor { get; private set; }
    public Vector3 RightBottomAnchor { get; private set; }

    [Header("House Settings")]
    [SerializeField] private BoxCollider _houseCollider;
    [SerializeField] private float _fenceToHouseOffset = 10f;

    [Header("Test")]
    [SerializeField] private bool _showTestAnchors = false;

    [SerializeField] private Transform _point1;
    [SerializeField] private Transform _point2;
    [SerializeField] private Transform _point3;
    [SerializeField] private Transform _point4;

    [SerializeField] private Transform _leftTopAnchorTransform;
    [SerializeField] private Transform _leftBottomAnchorTransform;
    [SerializeField] private Transform _rightTopAnchorTransform;
    [SerializeField] private Transform _rightBottomAnchorTransform;

    public void CalculateFenceBounds()
    {
        float houseColliderMinX = _houseCollider.bounds.min.x;
        float houseColliderMaxX = _houseCollider.bounds.max.x;

        float houseColliderMinZ = _houseCollider.bounds.min.z;
        float houseColliderMaxZ = _houseCollider.bounds.max.z;

        CalculateBounds(houseColliderMinX, houseColliderMaxX, houseColliderMinZ, houseColliderMaxZ);
    }
    private void CalculateBounds(float houseColliderMinX, float houseColliderMaxX, float houseColliderMinZ, float houseColliderMaxZ)
    {
        float fenceBoundMinX = houseColliderMinX - _fenceToHouseOffset;
        float fenceBoundMaxX = houseColliderMaxX + _fenceToHouseOffset;

        float fenceBoundMinZ = houseColliderMinZ - _fenceToHouseOffset;
        float fenceBoundMaxZ = houseColliderMaxZ + _fenceToHouseOffset;

        SetCornerAnchors(fenceBoundMinX, fenceBoundMaxX, fenceBoundMinZ, fenceBoundMaxZ);

        if (_showTestAnchors)
        {
            SetTestAnchors(fenceBoundMinX, fenceBoundMaxX, fenceBoundMinZ, fenceBoundMaxZ);
        }
    }

    private void SetCornerAnchors(float fenceBoundMinX, float fenceBoundMaxX, float fenceBoundMinZ, float fenceBoundMaxZ)
    {
        LeftTopAnchor = new(fenceBoundMinX, 0, fenceBoundMaxZ);
        LeftBottomAnchor = new(fenceBoundMinX, 0, fenceBoundMinZ);
        RightTopAnchor = new(fenceBoundMaxX, 0, fenceBoundMaxZ);
        RightBottomAnchor = new(fenceBoundMaxX, 0, fenceBoundMinZ);
    }

    private void SetTestAnchors(float fenceBoundMinX, float fenceBoundMaxX, float fenceBoundMinZ, float fenceBoundMaxZ)
    {
        // Test points
        Debug.Log($"_targetMinX = {fenceBoundMinX}");
        _point1.position = new(fenceBoundMinX, _point1.position.y, _point1.position.z);

        Debug.Log($"_targetMaxX = {fenceBoundMaxX}");
        _point2.position = new(fenceBoundMaxX, _point2.position.y, _point2.position.z);

        Debug.Log($"_targetMinZ = {fenceBoundMinZ}");
        _point3.position = new(_point3.position.x, _point3.position.y, fenceBoundMinZ);

        Debug.Log($"targetMaxZ = {fenceBoundMaxZ}");
        _point4.position = new(_point4.position.x, _point4.position.y, fenceBoundMaxZ);


        // Test anchors
        Debug.Log($"_targetMinX = {fenceBoundMinX}");
        _leftTopAnchorTransform.position = new(fenceBoundMinX, _leftTopAnchorTransform.position.y, fenceBoundMaxZ);

        Debug.Log($"_targetMaxX = {fenceBoundMaxX}");
        _leftBottomAnchorTransform.position = new(fenceBoundMinX, _leftBottomAnchorTransform.position.y, fenceBoundMinZ);

        Debug.Log($"_targetMinZ = {fenceBoundMinZ}");
        _rightTopAnchorTransform.position = new(fenceBoundMaxX, _rightTopAnchorTransform.position.y, fenceBoundMaxZ);

        Debug.Log($"targetMaxZ = {fenceBoundMaxZ}");
        _rightBottomAnchorTransform.position = new(fenceBoundMaxX, _rightBottomAnchorTransform.position.y, fenceBoundMinZ);
    }
}
