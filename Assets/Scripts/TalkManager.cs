using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TalkManager : MonoBehaviour
{
	public static TalkManager Instance;

	public int talkStage = 1;

	public int talkIndex = 0;

	public int stageIndex = 0;

	public string talk;
	public string[] talks;

	public List<Talk> listTalk;

	public List<Talk>[] listTalks = new List<Talk>[100];

	void Awake()
	{
		if(TalkManager.Instance == null)
			TalkManager.Instance = this;
		else
			Destroy(this.gameObject);

		Init();
	}

	void Start ()
	{
		
	}

	void Init()
	{
		talk = Resources.Load<TextAsset>("Text/Talk3").text;
		talks = talk.Split('\n');

		for(int i=0; i<talks.Length; i++)
		{
			if(i == 0)
				continue;
			
			int pos;
			int stage;
			int index;

			string name;
			string content;

			pos = talks[i].IndexOf(',');
			stage = int.Parse(talks[i].Substring(0, pos));
			talks[i] = talks[i].Substring(pos + 1, talks[i].Length - pos - 1);

			pos = talks[i].IndexOf(',');
			index = int.Parse(talks[i].Substring(0, pos));
			talks[i] = talks[i].Substring(pos + 1, talks[i].Length - pos - 1);

			pos = talks[i].IndexOf(',');
			name = talks[i].Substring(0, pos);
			talks[i] = talks[i].Substring(pos + 1, talks[i].Length - pos - 1);
			
			content = talks[i].Substring(0, talks[i].Length).Replace("\\n", string.Format("\n"));

			listTalk.Add( new Talk { _stage = stage, _index = index,_name = name, _content = content });

			listTalks[i] = new List<Talk>();
			listTalks[i].Add( new Talk { _stage = stage, _index = index, _name = name, _content = content });
		}

		//UIManager.Instance.SetNextTalk();
	}
	
	public Talk GetTalk()
	{
		if(listTalk.FindIndex((Talk talk) => talk._stage == talkStage && talk._index == talkIndex) >= (listTalk.FindLastIndex((Talk talk) => talk._stage == talkStage)))
		{
			if(talkStage == 1)
			{
				Talk talk;
				talk = new Talk{ _name = "END", _content = "END" };

				return talk;
			}
			else
				UIManager.Instance.panelTalk.SetActive(false);

			return listTalk[listTalk.FindIndex((Talk talk) => talk._stage == talkStage) + talkIndex];
		}

		return listTalk[listTalk.FindIndex((Talk talk) => talk._stage == talkStage) + talkIndex++];
	}

	public Talk GetNextTalk()
	{
		if(listTalk.Count > talkIndex)
			talkIndex ++;

		return listTalk[talkIndex - 1];
	}

	public Talk GetSuccess()
	{
		Talk talk;
		talk = new Talk{ _name = "할머니", _content = "영감, 고맙구려."};

		return talk;
	}

	public Talk GetFailed()
	{
		Talk talk;
		talk = new Talk{ _name = "할머니", _content = "이게 아니여 영감."};

		return talk;
	}

	public Talk GetFailed2()
	{
		Talk talk;
		talk = new Talk{ _name = "할아버지", _content = "그려?"};

		return talk;
	}
}