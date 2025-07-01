using UnityEngine;
using UnityEngine.Events;

public class EventOnEnable : MonoBehaviour
{

	public UnityEvent enableEvent;
	public UnityEvent disableEvent;

	private void OnEnable()
	{
		enableEvent.Invoke();
	}

	private void OnDisable()
	{
		disableEvent.Invoke();
	}

}
