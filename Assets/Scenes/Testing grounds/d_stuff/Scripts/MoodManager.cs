using UnityEngine;

public class MoodManager : MonoBehaviour
{
    [Range(0, 3)] public int hunger = 3;
    [Range(0, 3)] public int happiness = 3;

    public Renderer characterRenderer;
    public Material normalMaterial;
    public Material angryMaterial;
    public Material sadMaterial;
    public Material conflictedMaterial;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) GetHungry();
        if (Input.GetKeyDown(KeyCode.J)) EatFood();
        if (Input.GetKeyDown(KeyCode.K)) GetSad();
        if (Input.GetKeyDown(KeyCode.L)) CheerUp();

        UpdateMoodMaterial();
    }

    public void GetHungry() => hunger = Mathf.Max(hunger - 1, 0);
    public void EatFood() => hunger = Mathf.Min(hunger + 1, 3);
    public void GetSad() => happiness = Mathf.Max(happiness - 1, 0);
    public void CheerUp() => happiness = Mathf.Min(happiness + 1, 3);

    private void UpdateMoodMaterial()
    {
        if (hunger == 1 && happiness == 1)
        {
            characterRenderer.material = conflictedMaterial;
        }
        else if (hunger == 1)
        {
            characterRenderer.material = angryMaterial;
        }
        else if (happiness == 1)
        {
            characterRenderer.material = sadMaterial;
        }
        else
        {
            characterRenderer.material = normalMaterial;
        }
    }
}
