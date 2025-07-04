using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckText : MonoBehaviour
{
	public string code;
	public Destroyer destroyer;


	public void Verify()
	{
		string text = GetComponent<TMP_InputField>().text;

		if (code == text)
		{
			destroyer.DestroyTarget();
			GetComponent<InteractionObject>().OnEventTrigger();
		}else
		{
			Debug.Log(text);
		}
	}


}
