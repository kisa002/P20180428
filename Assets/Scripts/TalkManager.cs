using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TalkManager : MonoBehaviour
{
	public static TalkManager Instance;

	public int talkStage = 1;

	public int talkIndex = 0;

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
	}

	void Start ()
	{
		talk = Resources.Load<TextAsset>("Text/Ang").text;
		talks = talk.Split('\n');

		for(int i=0; i<talks.Length; i++)
		{
			if(i == 0)
				continue;
			
			int pos;
			int stage;

			string name;
			string content;

			pos = talks[i].IndexOf(',');
			stage = int.Parse(talks[i].Substring(0, pos));
			talks[i] = talks[i].Substring(pos + 1, talks[i].Length - pos - 1);

			pos = talks[i].IndexOf(',');
			name = talks[i].Substring(0, pos);
			talks[i] = talks[i].Substring(pos + 1, talks[i].Length - pos - 1);
			
			content = talks[i].Substring(0, talks[i].Length).Replace("\\n", string.Format("\n"));

			listTalk.Add( new Talk { _stage = stage, _name = name, _content = content });

			listTalks[i] = new List<Talk>();
			listTalks[i].Add( new Talk { _stage = stage, _name = name, _content = content });
		}

		UIManager.Instance.SetNextTalk();
	}
	
	public Talk GetTalk()
	{
		Debug.LogError("CURRENT : " + listTalk.FindIndex((Talk talk) => talk._stage == talkStage) + talkIndex + " / LAST : " + (listTalk.FindLastIndex((Talk talk) => talk._stage == talkStage) + 1));
		if(listTalk.FindIndex((Talk talk) => talk._stage == talkStage) + talkIndex == (listTalk.FindLastIndex((Talk talk) => talk._stage == talkStage) + 1))
		{
			Talk talk;
			talk = new Talk{ _name = "END", _content = "END" };

			return talk;
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
		talk = new Talk{ _name = "할머니", _content = "헤에에에" + string.Format("\n") + "기억해 주고있었네에....?"};

		return talk;
	}

	public Talk GetFailed()
	{
		Talk talk;
		talk = new Talk{ _name = "할머니", _content = "동작 그만, 내가 치매 걸렸다고 밑장 빼기냐" + string.Format("\n") + "내가 빙다리 핫바지로 보이더냐"};

		return talk;
	}
}