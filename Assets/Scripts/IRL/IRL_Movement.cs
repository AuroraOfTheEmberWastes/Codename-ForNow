using System.Collections;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class IRL_Movement : MonoBehaviour
{

    public int[,] tilemap;
    public int[] playerLocation;
    public int playerOrientation = 1; // 1-up, 2-right, 3-down, 4-left

	public float movementCooldown = 0.5f;
	public float movementTime = 0.15f;
	public float movementDistance = 1f;
	public float bumpDistance = 0.5f;
	public float turnTime = 1f;

	public float minSwipe = 40f;
	private float swipeDone;
	private Vector2 swipeDirection = Vector2.zero;

	public GameObject movementTrigger;
	public PhonePullout phonePullout;


	private Coroutine movementCoroutine;
	private Coroutine swipeCoroutine;


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
		swipeDone = 0;
		if (movementTime < 0.15f) movementTime = 0.15f;

    }




	public void ActivateMovement(InputAction.CallbackContext context) 
	{
		Vector2 direction = context.ReadValue<Vector2>();


		if (direction.magnitude > 1)
		{

			if (swipeCoroutine != null)
			{

				StopCoroutine(swipeCoroutine);
				swipeCoroutine = StartCoroutine(SwipeCheck(direction));
			}
			else swipeCoroutine = StartCoroutine(SwipeCheck(direction));
		}


		direction.Normalize();
		direction = new(Mathf.Round(direction.x), Mathf.Round(direction.y));

		if (movementCoroutine == null && (swipeDone == 0 || swipeDone >= minSwipe))
		{
			movementCoroutine = StartCoroutine(ActivateMovementCoroutine(direction));
		}
	}


	private IEnumerator ActivateMovementCoroutine(Vector2 direction)
	{
		swipeDone = 0;

		if (direction == new Vector2(0, 1)) MoveForward();
		else if (direction == new Vector2(1, 0)) TurnRight();
		else if (direction == new Vector2(-1, 0)) TurnLeft();
		else if (direction == new Vector2(0, -1)) OpenTamagochi();

		yield return new WaitForSeconds(movementCooldown);
		movementCoroutine = null;
	}

	private IEnumerator SwipeCheck(Vector2 direction)
	{
		float distance = direction.magnitude;
		direction.Normalize();
		direction = new(Mathf.Round(direction.x), Mathf.Round(direction.y));

		if (direction == swipeDirection) 
		{
			swipeDone += distance;
		}else
		{
			swipeDone = distance;
			swipeDirection = direction;
		}


		yield return new WaitForSeconds(0.05f);

		swipeDone = 0;
		swipeCoroutine = null;
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


	#region turn
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

	#endregion


	private void OpenTamagochi() 
	{
		Debug.Log("Open tamagochi");
		phonePullout.PullOut();
		movementTrigger.SetActive(false);
	}


}
