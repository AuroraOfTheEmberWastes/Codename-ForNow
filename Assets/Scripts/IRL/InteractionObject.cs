using UnityEngine;
using UnityEngine.Events;

public class InteractionObject : MonoBehaviour
{
	public UnityEvent interaction;

	public void OnEventTrigger()
	{
		interaction.Invoke();
	}
}
