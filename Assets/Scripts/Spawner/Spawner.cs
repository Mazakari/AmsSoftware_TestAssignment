using UnityEngine;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField] private GameObject _housePrefab;
    [SerializeField] private GameObject _treePrefab;

    [Space(10)]
    [Header("View Control")]
    private Camera _camera;
    [SerializeField] private LayerMask _groundLayerMask;
    [SerializeField] private LayerMask _obstaclesLayers;

    private void OnEnable() => 
        SubscribeSpawnCallbacks();

    private void Start() => 
        CacheCamera();

    private void OnDisable() => 
        UnsubscribeSpawnCallbacks();

    private void SpawnHouseAtRandomPosition()
    {
        Debug.Log("House spawn");
        Spawn(_housePrefab);
    }
    private void SpawnTreeAtRandomPosition()
    {
        Debug.Log("Tree spawn");
        Spawn(_treePrefab);
    }

    private void Spawn(GameObject prefab)
    {
        Vector3 spawnPoint = GetRandomSpawnPoint();

        if (SpawnPointObscured(spawnPoint)) return;

        Instantiate(prefab, spawnPoint, Quaternion.identity);
    }

    private Vector3 GetRandomSpawnPoint()
    {
        Vector3 randomPoint = Vector2.zero;
        Vector2 randomScreenPoint = GetRandomScreenPoint(Screen.width, Screen.height);

        Ray ray = _camera.ScreenPointToRay(randomScreenPoint);

        if(Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _groundLayerMask))
        {
           randomPoint = hit.point;
        }

        return randomPoint;
    }

    private Vector2 GetRandomScreenPoint(int screenWitdth, int screenHeight)
    {
        Vector2 value = Vector2.zero;
        value.x = Random.Range(0, screenWitdth);
        value.y = Random.Range(0, screenHeight);

        return value;
    }

    private bool SpawnPointObscured(Vector3 point)
    {
        bool pointObscured = false;
        float checkRadius = 5f;

        Collider[] collisions = new Collider[1];
        int hitColliders = Physics.OverlapSphereNonAlloc(point, checkRadius, collisions);

        if (hitColliders > 0)
        {
            Debug.Log("Spawn point obscured");
            pointObscured = true;
        }

        return pointObscured;
    }
    private void CacheCamera() =>
      _camera = Camera.main;

    private void SubscribeSpawnCallbacks()
    {
        DesignSceneView.OnAddHouseEvent += SpawnHouseAtRandomPosition;
        DesignSceneView.OnAddTreeEvent += SpawnTreeAtRandomPosition;
    }
    private void UnsubscribeSpawnCallbacks()
    {
        DesignSceneView.OnAddHouseEvent -= SpawnHouseAtRandomPosition;
        DesignSceneView.OnAddTreeEvent -= SpawnTreeAtRandomPosition;
    }
}
