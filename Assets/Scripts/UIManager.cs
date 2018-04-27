using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public Text textName;
	public Text textContent;

	void Start ()
	{
		//SetNextTalk();
	}

	public void SetNextTalk()
	{
		Talk talk = TalkManager.Instance.GetNextTalk();

		textName.text = talk._name;
		textContent.text = talk._content;
	}
}