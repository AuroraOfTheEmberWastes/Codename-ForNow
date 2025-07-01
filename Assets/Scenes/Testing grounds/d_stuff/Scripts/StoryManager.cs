using UnityEngine;

public class StoryManager : MonoBehaviour
{

    public InteractionObject interactionObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void playTamaDialogue()
    {
        //play dialogue
        interactionObject.OnEventTrigger();
    }


    public void UIRemover()
    {
        //logic to remove buttons from ui
    }
}
