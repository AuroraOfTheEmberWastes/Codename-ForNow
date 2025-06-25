using UnityEngine;

public class GridManager : MonoBehaviour
{
    [Header("Grid Settings")]
    public GameObject tilePrefab;
    public int gridSize = 5;
    public float spacing = 1.1f;

    [System.Serializable]
    public class ColorDot
    {
        public string colorName;
        public Color color;
        public Vector2Int start;
        public Vector2Int end;
    }

    [Header("Color Dots")]
    public ColorDot[] colorDots;

    private Tile[,] grid;

    public Tile[,] Grid => grid;
    public int GridSize => gridSize;

    void Start()
    {
        GenerateGrid();
        PlaceColorDots();
    }

    private void GenerateGrid()
    {
        grid = new Tile[gridSize, gridSize];
        Vector3 origin = transform.position;

        for (int x = 0; x < gridSize; x++)
        {
            for (int y = 0; y < gridSize; y++)
            {
                Vector3 worldPos = origin + new Vector3(x * spacing, 0f, y * spacing);
                GameObject tileGO = Instantiate(tilePrefab, worldPos, Quaternion.identity, transform);
                tileGO.name = $"Tile_{x}_{y}";

                Tile tile = tileGO.GetComponent<Tile>();
                tile.gridPos = new Vector2Int(x, y);
                tile.ResetTile();

                grid[x, y] = tile;
            }
        }
    }

    private void PlaceColorDots()
    {
        foreach (var dot in colorDots)
        {
            PlaceDot(dot.start, dot.color, dot.colorName);
            PlaceDot(dot.end, dot.color, dot.colorName);
        }
    }

    private void PlaceDot(Vector2Int pos, Color color, string colorId)
    {
        if (IsValidPos(pos))
        {
            Tile tile = grid[pos.x, pos.y];
            tile.isEndpoint = true;
            tile.SetColor(color, colorId);
        }
    }

    public Tile GetTileAt(Vector2Int pos)
    {
        return IsValidPos(pos) ? grid[pos.x, pos.y] : null;
    }

    public bool IsValidPos(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < gridSize && pos.y >= 0 && pos.y < gridSize;
    }
}
