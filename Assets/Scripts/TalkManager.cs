using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TalkManager : MonoBehaviour
{
	public static TalkManager Instance;

	public int talkType = -1;

	public int talkIndex = -1;

	public string talk;
	public string[] talks;

	public List<Talk> listTalk;
	
	public int currentType = -1;
	public int currentStage = 1;

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
			int type;

			string name;
			string content;

			pos = talks[i].IndexOf(',');
			stage = int.Parse(talks[i].Substring(0, pos));
			talks[i] = talks[i].Substring(pos + 1, talks[i].Length - pos - 1);

			pos = talks[i].IndexOf(',');
			type = int.Parse(talks[i].Substring(0, pos));
			talks[i] = talks[i].Substring(pos + 1, talks[i].Length - pos - 1);

			pos = talks[i].IndexOf(',');
			name = talks[i].Substring(0, pos);
			talks[i] = talks[i].Substring(pos + 1, talks[i].Length - pos - 1);
			
			content = talks[i].Substring(0, talks[i].Length).Replace("\\n", string.Format("\n"));

			listTalk.Add( new Talk { _stage = stage, _type = type, _name = name, _content = content });
		}
	}
	
	// public Talk GetTalk(int stage)
	// {
	// 	if(listTalk[stage]._type == 0)
	// 		return listTalk[talkIndex++];
	// 	else if(talkIndex == -1)
	// 	{
	// 		int rand = Random.Range(1, 3);

	// 		int cnt = 0;
	// 		while(true)
	// 		{
	// 			if(listTalk[talkIndex]._type == rand)
	// 			{
	// 				currentType = tal
	// 			}

	// 			cnt++;
	// 		}
	// 		return listTalk[talkIndex++];
	// 	}
	// }

	public Talk GetNextTalk()
	{
		if(listTalk.Count > talkIndex)
			talkIndex ++;

		return listTalk[talkIndex - 1];
	}
}
