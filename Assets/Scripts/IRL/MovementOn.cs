using UnityEngine;

public class MovementOn : MonoBehaviour
{
	public IRL_Movement player;
	private void OnEnable()
	{
		player.tamagochiOn = false;
	}

}
