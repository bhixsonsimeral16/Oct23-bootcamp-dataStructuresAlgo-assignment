using Unity.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.Tilemaps;

[ExecuteInEditMode]
public class CreateObjects : MonoBehaviour
{

    #region deprecated
    [Header("Deprecated")]
    public Vector2 _from;
    public Vector2 _to;
    public int _zValue;
    #endregion
    
    [Header("Create objects based on tilemaps")]
    public GameObject _objectToCreate;
    [SerializeField] private Tilemap _groundMap;
    [SerializeField] private Tilemap _propMap;
    [SerializeField] private Tilemap _treeMap;


    GameObject _tempObject;

    public void GenerateObjects()
    {
        for (int i = Mathf.RoundToInt(_from.x); i < _to.x; i++)
        {
            for (int j = Mathf.RoundToInt(_from.y); j < _to.y;j++)
            {
                _tempObject = PrefabUtility.InstantiatePrefab(_objectToCreate, transform) as GameObject;
                _tempObject.transform.position = new Vector3(i,j,_zValue);
            }
        }
    }

    public void GenerateObjectsOnGrass()
    {
        // Destroy child objects first
        while (transform.childCount > 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        for (int x = _groundMap.cellBounds.x; x < _groundMap.cellBounds.max.x; x++)
        {
            for (int y = _groundMap.cellBounds.y; y < _groundMap.cellBounds.max.y; y++)
            {
                // Ensure there are ground tiles in a 3x3 grid
                if (_groundMap.HasTile(new Vector3Int(x, y, 0)) &&
                    _groundMap.HasTile(new Vector3Int(x, y + 1, 0)) &&
                    _groundMap.HasTile(new Vector3Int(x, y - 1, 0)) &&
                    _groundMap.HasTile(new Vector3Int(x + 1, y, 0)) &&
                    _groundMap.HasTile(new Vector3Int(x + 1, y + 1, 0)) &&
                    _groundMap.HasTile(new Vector3Int(x + 1, y - 1, 0)) &&
                    _groundMap.HasTile(new Vector3Int(x - 1, y, 0)) &&
                    _groundMap.HasTile(new Vector3Int(x - 1, y - 1, 0)) &&
                    _groundMap.HasTile(new Vector3Int(x - 1, y + 1, 0)))
                {
                    // Don't place objects on top of trees or props
                    if (_propMap.HasTile(new Vector3Int(x, y, 0)) || _treeMap.HasTile(new Vector3Int(x, y, 0)))
                    {
                        continue;
                    }
                    _tempObject = PrefabUtility.InstantiatePrefab(_objectToCreate, transform) as GameObject;
                    _tempObject.transform.position = new Vector3(x + 1, y + 1, _zValue);
                }
            }
        }
    }
}
