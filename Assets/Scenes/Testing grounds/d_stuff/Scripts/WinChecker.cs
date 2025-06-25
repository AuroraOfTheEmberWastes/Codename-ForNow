using UnityEngine;

public class WinChecker : MonoBehaviour
{
    [Header("References")]
    public GridManager gridManager;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (CheckWin())
                Debug.Log("You Win!");
            else
                Debug.Log("Not Yet...");
        }
    }

    public bool CheckWin()
    {
        int size = gridManager.GridSize;

        for (int x = 0; x < size; x++)
        {
            for (int y = 0; y < size; y++)
            {
                Tile tile = gridManager.GetTileAt(new Vector2Int(x, y));
                if (tile == null || tile.colorName == "")
                    return false;
            }
        }

        foreach (var dot in gridManager.colorDots)
        {
            Tile start = gridManager.GetTileAt(dot.start);
            Tile end = gridManager.GetTileAt(dot.end);

            if (start == null || end == null)
                return false;

            if (start.colorName != dot.colorName || end.colorName != dot.colorName)
                return false;
        }

        return true;
    }
}
