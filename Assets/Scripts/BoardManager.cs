using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public Grid grid;
    public Vector2 screenSize;
    public List<GameObject> matchVariety;
    public GameObject backgrountTile;
    private void Awake()
    {
        screenSize = new Vector2(Camera.main.aspect * Camera.main.orthographicSize * 2, Camera.main.aspect * Camera.main.orthographicSize * 1f);
    }
    [ContextMenu("Debug")]

    public void DebugSomething()
    {

    }
    public void GenerateGrid()
    {
        int largestXLength = -1;
        foreach (var item in grid.y)
        {

            if (item.x.Length>largestXLength)
            {
                largestXLength = item.x.Length;
            }
            
        }
        int largestYLength = grid.y.Length;
        Vector3 perTileSize;
        if (largestXLength < largestYLength)
        {
            perTileSize = Vector3.one * screenSize.y / (largestYLength + 2); 

        }
        else
        {
            perTileSize = Vector3.one * screenSize.x / (largestXLength + 2);

        }
        Debug.Log(perTileSize);
        for (int y = 0; y < grid.y.Length; y++)
        {
            for (int x = 0; x < grid.y[y].x.Length; x++)
            {
                grid.y[y].x[x].go = Instantiate(backgrountTile,
                    (Vector3.left*(largestXLength)/2)+(Vector3.right*(x-1)*perTileSize.x)
                    + (Vector3.down * (largestYLength) / 2) + (Vector3.up* (y-1) * perTileSize.y)
                    , Quaternion.identity,transform);
                grid.y[y].x[x].go.transform.localScale = perTileSize;
                var rand = Random.Range(0, matchVariety.Count);
                grid.y[y].x[x].Tile = new Tile(rand, Instantiate(matchVariety[rand],transform.GetChild(0)));
            }
        }
    }
    void Start()
    {
        GenerateGrid();
    }

    
}

[System.Serializable]
public class Grid
{
    public GridX[] y; 
}
[System.Serializable]
public class GridX
{
    public TileContainer[] x; 
}
[System.Serializable]
public class TileContainer{


    public Vector2 gridPosition;
    public GameObject go;
    [SerializeField] Tile tile;

    public Tile Tile { get => tile; set { tile = value; SetChildTile(); } }

    public void SetChildTile()
    {
        if (Tile != null&&Tile.equiped)
        {
            
            Tile.go.transform.position = go.transform.position + Vector3.back * 2;
            Tile.go.transform.localScale = go.transform.localScale*0.9f;
        }   
    }

}

[System.Serializable]
public class Tile{
    public int id;
    public bool equiped;
    public GameObject go;

    public Tile(int id, GameObject go)
    {
        this.id = id;
        this.go = go;
        equiped = true;
    }
    
} 