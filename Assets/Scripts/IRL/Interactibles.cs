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
        
        
        
        interactionObjects = new Dictionary<Vector3, GameObject>();
        foreach(Transform child in transform)
        {
            interactionObjects.Add(child.transform.position, child.gameObject);
        }

        foreach (Vector3 key in interactionObjects.Keys)
        {
            Debug.Log(key);
        }

    }


    public void Interact(InputAction.CallbackContext context)
    {
        if (context.canceled)
		{
			//updating these
			playerPosition = player.gameObject.transform.position;
			playerOrientation = player.playerOrientation;
			playerMovement = player.movementDistance;

			//getting interactible key
			Vector3 orientationModifier = new(-Mathf.Cos(Mathf.PI * playerOrientation / 2) * playerMovement, 0, Mathf.Sin(Mathf.PI * playerOrientation / 2) * playerMovement);
			Vector3 interactibleLocation = orientationModifier + playerPosition;
            foreach (Vector3 key in interactionObjects.Keys)
            {
                if(key == interactibleLocation)
                {
					interactionObjects[key].GetComponent<InteractionObject>().OnEventTrigger();
				}
            }
        }
    }



}
