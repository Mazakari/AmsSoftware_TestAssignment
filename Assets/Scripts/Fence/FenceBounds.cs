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
    }

    private void SetCornerAnchors(float fenceBoundMinX, float fenceBoundMaxX, float fenceBoundMinZ, float fenceBoundMaxZ)
    {
        LeftTopAnchor = new(fenceBoundMinX, 0, fenceBoundMaxZ);
        LeftBottomAnchor = new(fenceBoundMinX, 0, fenceBoundMinZ);
        RightTopAnchor = new(fenceBoundMaxX, 0, fenceBoundMaxZ);
        RightBottomAnchor = new(fenceBoundMaxX, 0, fenceBoundMinZ);
    }
}
