using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
	public static UIManager Instance;

	public Text textName;
	public Text textContent;

	public Image panelStage;
	public Text textStage;

	public GameObject buttons;

	bool isTalk = false;

	void Awake()
	{
		if(UIManager.Instance == null)
			UIManager.Instance = this;
		else
			Destroy(this.gameObject);
	}

	void Start ()
	{
		StartCoroutine(Fade());
	}
	
	void Update()
	{
		if(Input.GetKeyDown(KeyCode.A))
			SetSuccessTalk();
		if(Input.GetKeyDown(KeyCode.D))
			SetFailedTalk();
	}

	IEnumerator Fade()
	{
		panelStage.gameObject.SetActive(true);

		if(TalkManager.Instance.talkStage == 0)
			textStage.text = "프롤로그";
		else
			textStage.text = "챕터 " + TalkManager.Instance.talkStage;

		for(int i=0; i<100; i++)
		{
			panelStage.color = new Color(0, 0, 0, panelStage.color.a + 0.01f);
			yield return new WaitForSeconds(0.01f);
		}

		SetNextTalk();		
		yield return new WaitForSeconds(1f);		

		for(int i=0; i<100; i++)
		{
			textStage.color = new Color(1, 1, 1, textStage.color.a + 0.01f);
			yield return new WaitForSeconds(0.01f);
		}

		yield return new WaitForSeconds(1f);
		
		for(int i=0; i<100; i++)
		{
			textStage.color = new Color(1, 1, 1, textStage.color.a - 0.01f);
			yield return new WaitForSeconds(0.01f);
		}

		yield return new WaitForSeconds(1f);

		for(int i=0; i<100; i++)
		{
			panelStage.color = new Color(1, 1, 1, panelStage.color.a - 0.01f);
			yield return new WaitForSeconds(0.01f);
		}
		
		panelStage.gameObject.SetActive(false);
	}

	public void SetSuccessTalk()
	{
		Talk talk = TalkManager.Instance.GetSuccess();

		textName.text = talk._name;
		textContent.text = talk._content;
	}

	public void SetFailedTalk()
	{
		Talk talk = TalkManager.Instance.GetFailed();

		textName.text = talk._name;
		textContent.text = talk._content;
	}

	public void SetNextTalk()
	{
		Talk talk = TalkManager.Instance.GetTalk();

		if(talk._name == "END")
		{
			TalkManager.Instance.talkIndex = 0;					
			TalkManager.Instance.talkStage ++;
			
			StartCoroutine(Fade());
		}
		else
		{
			textName.text = talk._name;
			textContent.text = talk._content;
		}
	}
}