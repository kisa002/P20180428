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
	public GameObject panelTalk;
	public Text textStage;

	public GameObject buttonReplay;
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

		switch(TalkManager.Instance.talkStage)
		{
			case 2:
				textStage.text = "이야기의 시작";
				break;
			
			case 3:
				textStage.text = "보물";
				break;

			case 4:
				textStage.text = "모험";
				break;

			case 5:
				textStage.text = "바보같은 추억";
				break;
		}
		
		if(TalkManager.Instance.talkStage != 1)
		{
			panelStage.gameObject.SetActive(true);
			
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
			
			//SetNextTalk();

			if(textContent.text == "영감, 고맙구려.")
			{
				Talk talk2 = TalkManager.Instance.GetTalk();

				textName.text = talk2._name;
				textContent.text = talk2._content;
			}
			
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
			
			panelStage.gameObject.SetActive(false);

			yield return new WaitForSeconds(.5f);
		}
		else
		{
			SetNextTalk();
		}
	}

	public void Replay()
	{
		panelTalk.SetActive(true);

		TalkManager.Instance.talkIndex = 0;
		SetNextTalk();
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

	public void SetFailedTalk2()
	{
		Talk talk = TalkManager.Instance.GetFailed2();

		textName.text = talk._name;
		textContent.text = talk._content;
	}

	public void SetNextTalk()
	{
		if(textContent.text == "이게 아니여 영감.")
		{
			SetFailedTalk2();
			return ;
		}

		if(textContent.text == "영감, 고맙구려.")
		{
			TalkManager.Instance.talkIndex = 0;					
			TalkManager.Instance.talkStage ++;
			
			StartCoroutine(Fade());

			return;
		}

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
		
		PlayerController.instance.StartChapter(1);
	}
}