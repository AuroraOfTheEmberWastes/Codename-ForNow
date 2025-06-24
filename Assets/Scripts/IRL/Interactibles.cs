using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactibles : MonoBehaviour
{
    public IRL_Movement player;
    private Vector3 playerPosition;
    private int playerOrientation;
    private float playerMovement;
    private Dictionary<Vector3, GameObject> interactionObjects;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerPosition = player.gameObject.transform.position;
        playerOrientation = player.playerOrientation;
        playerMovement = player.movementDistance;
        
        
        
        interactionObjects = new Dictionary<Vector3, GameObject>();
        foreach(GameObject child in transform)
        {
            interactionObjects.Add(child.transform.position, child);
        }


    }


    public void Interact(InputAction.CallbackContext context)
    {
        Vector3 orientationModifier = new(Mathf.Sin(Mathf.PI * playerOrientation), 0, Mathf.Cos(Mathf.PI * playerOrientation));
        Vector3 interactibleLocation = orientationModifier + playerPosition;

        interactionObjects[interactibleLocation].GetComponent<InteractionObject>().OnEventTrigger();
    }



}
