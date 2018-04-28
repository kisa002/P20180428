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
	public Image imageProlog;

	public bool isClear = false;

	//bool isTalk = false;

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
		//StartCoroutine(PlayProlog());
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
		if(TalkManager.Instance.talkStage == 2) // 2가 가장 스무스 ㅇㅈ?
			StartCoroutine(PlayProlog());
		
		if(TalkManager.Instance.talkStage != 1)
		{
			panelStage.gameObject.SetActive(true);
			textStage.text = "챕터 " + (TalkManager.Instance.talkStage - 1);

			for(int i=0; i<50; i++)
			{
				panelStage.color = new Color(0, 0, 0, panelStage.color.a + 0.02f);
				yield return new WaitForSeconds(0.01f);
			}

			if(isClear)
			{
				PlayerController.instance.EndChapter();
				PlayerController.instance.StartChapter(TalkManager.Instance.talkStage - 1);
				isClear = false;
			}

			if(TalkManager.Instance.talkStage != 1)
				SetNextTalk();
			
			yield return new WaitForSeconds(.5f);

			for(int i=0; i<50; i++)
			{
				textStage.color = new Color(1, 1, 1, textStage.color.a + 0.02f);
				yield return new WaitForSeconds(0.01f);
			}

			yield return new WaitForSeconds(.5f);
			
			for(int i=0; i<50; i++)
			{
				textStage.color = new Color(1, 1, 1, textStage.color.a - 0.02f);
				yield return new WaitForSeconds(0.01f);
			}

			yield return new WaitForSeconds(.5f);

			for(int i=0; i<50; i++)
			{
				panelStage.color = new Color(0, 0, 0, panelStage.color.a - 0.02f);
				yield return new WaitForSeconds(0.01f);
			}

			yield return new WaitForSeconds(.5f);
			panelStage.gameObject.SetActive(false);

			yield return new WaitForSeconds(.5f);
		}
		else
		{
			SetNextTalk();			
		}
	}

	public void SetSuccessTalk()
	{
		Talk talk = TalkManager.Instance.GetSuccess();

		isClear = true;

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

	IEnumerator PlayProlog()
	{
		imageProlog.gameObject.SetActive(true);

		for(int i=1; i<13; i++)
		{
			imageProlog.sprite = Resources.Load<Sprite>("Sprites/Prolog/" + i);
			yield return new WaitForSeconds(.2f);
		}

		yield return new WaitForSeconds(.5f);

		for(int i=0; i<50; i++)
		{
			imageProlog.color = new Color(1, 1, 1, imageProlog.color.a - 0.02f);
			yield return new WaitForSeconds(0.01f);
		}

		imageProlog.gameObject.SetActive(false);
		
		SetNextTalk();
		PlayerController.instance.StartChapter(1);
	}
}