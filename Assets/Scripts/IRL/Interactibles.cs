using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interactibles : MonoBehaviour
{
    public IRL_Movement player;
    private Vector3 playerPosition;
    private int playerOrientation;
    private float playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
        
        


	}


    public void Interact(InputAction.CallbackContext context)
    {


	    Dictionary<Vector3, GameObject> interactionObjects;

        if (context.canceled)
		{

			interactionObjects = new Dictionary<Vector3, GameObject>();
			foreach (Transform child in transform)
			{
				if (child.gameObject.activeInHierarchy)
				{
					interactionObjects.Add(child.transform.position, child.gameObject);
				}
			}





			//updating these
			playerPosition = player.gameObject.transform.position;
			playerOrientation = player.playerOrientation;
			playerMovement = player.movementDistance;

			//getting interactible key
			Vector3 orientationModifier = new(-Mathf.Cos(Mathf.PI * playerOrientation / 2) * playerMovement, 0, Mathf.Sin(Mathf.PI * playerOrientation / 2) * playerMovement);
			Vector3 interactibleLocation = orientationModifier + playerPosition;

            Debug.Log(interactibleLocation);

            foreach (Vector3 key in interactionObjects.Keys)
			{
				if (key == interactibleLocation)
                {
					interactionObjects[key].GetComponent<InteractionObject>().OnEventTrigger();
                    break;
				}
            }
        }
    }



}
