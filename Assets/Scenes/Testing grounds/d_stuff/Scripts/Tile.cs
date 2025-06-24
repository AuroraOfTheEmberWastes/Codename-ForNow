using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(Collider))]
public class Tile : MonoBehaviour
{
    public Vector2Int gridPos;
    private MeshRenderer meshRenderer;
    private MaterialPropertyBlock propBlock;

    public Color defaultColor = Color.white;
    public Color currentColor;
    public bool isEndpoint = false;
    public string colorName = "";

    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        propBlock = new MaterialPropertyBlock();
        ResetTile();
    }

    public void SetColor(Color color, string colorId)
    {
        currentColor = color;
        colorName = colorId;

        meshRenderer.GetPropertyBlock(propBlock);
        propBlock.SetColor("_BaseColor", color);
        meshRenderer.SetPropertyBlock(propBlock);
    }

    public void ResetTile()
    {
        if (!isEndpoint)
        {
            SetColor(defaultColor, "");
        }
    }
}
