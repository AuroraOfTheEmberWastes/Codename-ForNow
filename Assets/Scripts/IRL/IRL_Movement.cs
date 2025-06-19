using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class IRL_Movement : MonoBehaviour
{

    private int[,] tilemap;
    private int[] playerLocation;
    private int playerOrientation = 1; // 1-up, 2-right, 3-down, 4-left

	public float movementCooldown = 0.5f;
	public float movementTime = 0.15f;
	public float movementDistance = 1f;
	public float bumpDistance = 0.5f;
	public float turnTime = 1f;

	public GameObject movementTrigger;



	private Coroutine movementCoroutine;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        tilemap = new int[5, 5]
        {   {1, 1, 1, 1, 1},
			{1, 0, 0, 0, 1},
			{1, 0, 0, 0, 1},
			{1, 0, 0, 0, 1},
			{1, 1, 1, 1, 1}
		};
        playerLocation = new int[2] { 3, 2 };
		playerOrientation = 1;


    }




	public void ActivateMovement(InputAction.CallbackContext context) 
	{


		if (movementCoroutine == null) movementCoroutine = StartCoroutine(ActivateMovementCoroutine(context.ReadValue<Vector2>()));

	}


	private IEnumerator ActivateMovementCoroutine(Vector2 direction)
	{
		if (direction == new Vector2(0, 1)) MoveForward();
		else if (direction == new Vector2(1, 0)) TurnRight();
		else if (direction == new Vector2(-1, 0)) TurnLeft();
		else if (direction == new Vector2(0, -1)) OpenTamagochi();

		yield return new WaitForSeconds(movementCooldown);
		movementCoroutine = null;
	}



	#region move 
	private void MoveForward ()
	{                           // 1-up, 2-right, 3-down, 4-left
		if (playerOrientation == 1)
        {
			Vector3 destination = new(transform.position.x, transform.position.y, transform.position.z + movementDistance);

			if (tilemap[playerLocation[0] - 1,playerLocation[1]] == 0)
			{
				StartCoroutine(MoveToPosition(destination, movementTime));
				playerLocation[0]--;
			}else
            {
				StartCoroutine(WallBump(destination));
            }
        }
		else if (playerOrientation == 2)
		{
			Vector3 destination = new(transform.position.x + movementDistance, transform.position.y, transform.position.z);

			if (tilemap[playerLocation[0], playerLocation[1] + 1] == 0)
			{
				StartCoroutine(MoveToPosition(destination, movementTime));
				playerLocation[1]++;
			}
			else
			{
				StartCoroutine(WallBump(destination));
			}
		}
		else if (playerOrientation == 3)
		{
			Vector3 destination = new(transform.position.x, transform.position.y, transform.position.z - movementDistance);

			if (tilemap[playerLocation[0] + 1, playerLocation[1]] == 0)
			{
				StartCoroutine(MoveToPosition(destination, movementTime));
				playerLocation[0]++;
			}
			else
			{
				StartCoroutine(WallBump(destination));
			}
		}
		else if (playerOrientation == 4)
		{
			Vector3 destination = new(transform.position.x - movementDistance, transform.position.y, transform.position.z);

			if (tilemap[playerLocation[0], playerLocation[1] - 1] == 0)
			{
				StartCoroutine(MoveToPosition(destination, movementTime));
				playerLocation[1]--;
			}
			else
			{
				StartCoroutine(WallBump(destination));
			}
		}
	}

	private IEnumerator MoveToPosition(Vector3 target, float timeMod)
	{
		float t = 0;
		Vector3 start = transform.position;

		while (t <= 1)
		{
			t += Time.fixedDeltaTime / timeMod;
			transform.localPosition = Vector3.Lerp(start, target, t);
			yield return null;
		}
	}
	private IEnumerator WallBump(Vector3 target)
	{
		//Debug.Log(tilemap[playerLocation[0], playerLocation[1]] + " " + playerLocation[0] + " " + playerLocation[1]);

		Vector3 currentPosition = transform.position;
		StartCoroutine(MoveToPosition(currentPosition + (target - currentPosition) * bumpDistance, movementTime / 2));
		yield return new WaitForSeconds(movementTime / 2);
		StartCoroutine(MoveToPosition(currentPosition, movementTime / 2));
	}

	#endregion

	private void TurnRight()
	{
		Quaternion targetRotation = Quaternion.LookRotation(transform.right);
		StartCoroutine(TurnCamera(targetRotation));

		playerOrientation++;
		if (playerOrientation == 5) playerOrientation = 1;
	}
	private void TurnLeft() 
	{
		Quaternion targetRotation = Quaternion.LookRotation(-transform.right);
		StartCoroutine(TurnCamera(targetRotation));

		playerOrientation--;
		if (playerOrientation == 0) playerOrientation = 4;
	}

	private IEnumerator TurnCamera(Quaternion destination)
	{
		Quaternion currentRotation = transform.rotation;
		float t = 0f;
		while (t <= 1)
		{
			t += Time.fixedDeltaTime / turnTime;
			transform.rotation = Quaternion.Slerp(currentRotation, destination, t);
			yield return null;
		}


	}




	private void OpenTamagochi() 
	{
		Debug.Log("Open tamagochi");
		movementTrigger.SetActive(false);
	}


}
