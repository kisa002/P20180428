using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class TalkManager : MonoBehaviour
{
	public static TalkManager Instance;

	public int talkType = -1;

	public int talkIndex;

	public string talk;
	public string[] talks;

	public List<Talk> listTalk;

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

			listTalk.Add( new Talk { _stage = stage, _index = index, _name = name, _content = content });
		}
	}
	
	// public Talk GetTalk(int stage)
	// {
	// 	return listTalk[listTalk[stage].];
	// }

	public Talk GetNextTalk()
	{
		if(listTalk.Count > talkIndex)
			talkIndex ++;

		return listTalk[talkIndex - 1];
	}
}
