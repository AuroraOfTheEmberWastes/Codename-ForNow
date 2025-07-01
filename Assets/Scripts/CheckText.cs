using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CheckText : MonoBehaviour
{
	public string code;


	public void Verify()
	{
		string text = GetComponent<TMP_InputField>().text;

		if (code == text)
		{
			GetComponent<InteractionObject>().OnEventTrigger();
		}else
		{
			Debug.Log(text);
		}
	}


}
