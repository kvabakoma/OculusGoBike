using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public struct Award
{
	public string name;

	public int defaultCount;

	[HideInInspector]
	public int count;

	public GameObject visual;
}

public class AwardManager : MonoBehaviour
{
	public Transform spawnLocation;
	public Award[] awards;

	void Start()
	{
		LoadAwards();

		for (int i = 0; i < awards.Length; i++)
		{
			Debug.Log(awards[i].name + "\t" + awards[i].count);
		}
	}

	void LoadAwards()
	{
		for (int i = 0; i < awards.Length; i++)
		{
			awards[i].count = PlayerPrefs.GetInt(awards[i].name, awards[i].defaultCount);
		}
	}

	void SaveAwards()
	{
		SaveAwards(false);
	}
	void SaveAwards(bool writeDefaults)
	{
		for (int i = 0; i < awards.Length; i++)
		{
			PlayerPrefs.SetInt(awards[i].name, (writeDefaults ? awards[i].defaultCount : awards[i].count));
		}
		PlayerPrefs.Save();
	}

	public void ResetAwards()
	{
		SaveAwards(true);
	}
    
	List<int> CollcetAwardIds()
	{
		List<int> awardIdList = new List<int>();
		for (int awardId = 0; awardId < awards.Length; awardId++)
		{
			for (int i = 0; i < awards[awardId].count; i++)
			{
				awardIdList.Add(awardId);
			}
		}
		return awardIdList;

	}
	int[] FisherYatesShuffle(List<int> list)
	{
		int[] array = list.ToArray();
		for (int i = 0; i < array.Length; i++)
		{
			int temp = array[i];
			int randomIndex = Random.Range(i, array.Length);
			array[i] = array[randomIndex];
			array[randomIndex] = temp;
		}
		return array;
	}

    public void RollAward()
	{
		List<int> awardIdList = CollcetAwardIds();
		if (awardIdList.Count == 0)
		{
			return;
		}
		int[] awardsShuffled = FisherYatesShuffle(awardIdList);

		int randIndex = Random.Range(0, awardsShuffled.Length);
		int awardIndex = awardsShuffled[randIndex];

		GameObject award = GameObject.Instantiate(awards[awardIndex].visual);
		award.transform.SetParent(spawnLocation, false);

		awards[awardIndex].count--;

		SaveAwards();
	}
}
