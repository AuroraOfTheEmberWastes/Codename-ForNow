using System.Collections.Generic;
using UnityEngine;

public class PathManager : MonoBehaviour
{
    [Header("References")]
    public Camera mainCamera;
    public GridManager gridManager;
    public WinChecker winChecker;

    private string currentColorId;
    private Color currentColor;
    private List<Tile> activePath = new();
    private Dictionary<string, List<Tile>> allPaths = new();
    private bool isDrawing = false;
    private bool hasWon = false;

    public int connectedPathsCount = 0;

    void Update()
    {
        if (hasWon) return;

#if UNITY_EDITOR || UNITY_STANDALONE
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
            TryStartPath(Input.mousePosition);

        if (isDrawing && Input.GetMouseButton(0))
            ContinuePath(Input.mousePosition);

        if (isDrawing && Input.GetMouseButtonUp(0))
            EndPath();
    }

    void HandleTouchInput()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    TryStartPath(touch.position);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (isDrawing)
                        ContinuePath(touch.position);
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (isDrawing)
                        EndPath();
                    break;
            }
        }
    }

    void TryStartPath(Vector2 screenPosition)
    {
        Tile tile = GetTileUnderScreenPosition(screenPosition);
        if (tile != null && tile.isEndpoint)
        {
            currentColorId = tile.colorName;
            currentColor = tile.currentColor;

            ClearColorPath(currentColorId);

            activePath.Clear();
            activePath.Add(tile);
            isDrawing = true;
        }
    }

    void ContinuePath(Vector2 screenPosition)
    {
        Tile tile = GetTileUnderScreenPosition(screenPosition);
        if (tile == null) return;

        if (activePath.Contains(tile))
        {
            if (activePath.Count >= 2 && tile == activePath[activePath.Count - 2])
            {
                Tile lastTile = activePath[activePath.Count - 1];
                if (!lastTile.isEndpoint)
                    lastTile.ResetTile();

                activePath.RemoveAt(activePath.Count - 1);
            }
            return;
        }

        Tile last = activePath[activePath.Count - 1];

        if (!IsAdjacent(tile.gridPos, last.gridPos)) return;

        if (!tile.isEndpoint && tile.colorName != "")
            return;

        if (tile.isEndpoint && tile.colorName != currentColorId)
            return;

        tile.SetColor(currentColor, currentColorId);
        activePath.Add(tile);

        if (tile.isEndpoint && tile != activePath[0])
        {
            EndPath();
        }
    }

    void EndPath()
    {
        isDrawing = false;

        if (activePath.Count < 2 || !activePath[^1].isEndpoint || activePath[0].colorName != activePath[^1].colorName)
        {
            foreach (var tile in activePath)
                if (!tile.isEndpoint) tile.ResetTile();

            activePath.Clear();
        }
        else
        {
            bool wasAlreadyConnected = allPaths.ContainsKey(currentColorId);

            allPaths[currentColorId] = new List<Tile>(activePath);

            foreach (var tile in activePath)
                tile.SetColor(currentColor, currentColorId);

            if (!wasAlreadyConnected)
                connectedPathsCount++;

            Invoke(nameof(CheckForWin), 0.05f);
        }
    }

    void ClearColorPath(string colorId)
    {
        if (allPaths.ContainsKey(colorId))
        {
            foreach (Tile tile in allPaths[colorId])
            {
                if (!tile.isEndpoint)
                    tile.ResetTile();
            }
            allPaths.Remove(colorId);
            connectedPathsCount = Mathf.Max(connectedPathsCount - 1, 0);
        }
    }

    void CheckForWin()
    {
        if (winChecker != null && winChecker.CheckWin())
        {
            hasWon = true;
            Debug.Log("YOU WIN!");
        }
    }

    Tile GetTileUnderScreenPosition(Vector2 screenPosition)
    {
        Ray ray = mainCamera.ScreenPointToRay(screenPosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.collider.GetComponent<Tile>();
        }
        return null;
    }

    bool IsAdjacent(Vector2Int a, Vector2Int b)
    {
        int dx = Mathf.Abs(a.x - b.x);
        int dy = Mathf.Abs(a.y - b.y);
        return dx + dy == 1;
    }
}
