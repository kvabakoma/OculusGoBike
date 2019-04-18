using UnityEngine;
using System.Collections.Generic;

public class AwardManager : MonoBehaviour
{
	public Transform spawnLocation;
	public Awards awardData;

	void Start()
	{
		LoadAwards();

		for (int i = 0; i < awardData.awards.Length; i++)
		{
			Debug.Log(awardData.awards[i].name + "\t" + awardData.awards[i].count);
		}
	}

	void LoadAwards()
	{
		for (int i = 0; i < awardData.awards.Length; i++)
		{
			awardData.awards[i].count = PlayerPrefs.GetInt(awardData.awards[i].name, awardData.awards[i].defaultCount);
		}
	}

	void SaveAwards()
	{
		SaveAwards(false);
	}
	void SaveAwards(bool writeDefaults)
	{
		for (int i = 0; i < awardData.awards.Length; i++)
		{
			PlayerPrefs.SetInt(awardData.awards[i].name, (writeDefaults ? awardData.awards[i].defaultCount : awardData.awards[i].count));
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
		for (int awardId = 0; awardId < awardData.awards.Length; awardId++)
		{
			for (int i = 0; i < awardData.awards[awardId].count; i++)
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

		GameObject award = GameObject.Instantiate(awardData.awards[awardIndex].visual);
		award.transform.SetParent(spawnLocation, false);

		awardData.awards[awardIndex].count--;

		SaveAwards();
	}
}
