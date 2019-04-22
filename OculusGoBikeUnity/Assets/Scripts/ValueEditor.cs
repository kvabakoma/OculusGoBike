using UnityEngine;
using UnityEngine.UI;

public class ValueEditor : MonoBehaviour
{
	public Awards awards;

	public int awardId;
	public Text name;
	public Text value;

	public string awardName;
	int defaultCount;

	private void Start()
	{
		if (awardId >= 0 && awardId < awards.awards.Length)
		{
			awardName = awards.awards[awardId].name;
			defaultCount = awards.awards[awardId].defaultCount;

			name.text = awardName;
		}
	}
	public void AddValue()
	{
		int val = PlayerPrefs.GetInt(awardName, defaultCount);
		val++;
		SaveValue(val);
	}
	public void RemoveValue()
	{
		int val = PlayerPrefs.GetInt(awardName, defaultCount);
		val--;
		SaveValue(val);
	}

	void SaveValue(int val)
	{
		val = Mathf.Clamp(val, 0, defaultCount*2);
		PlayerPrefs.SetInt(awardName, val);

		value.text = val.ToString();
	}

	private void Update()
	{
		int val = PlayerPrefs.GetInt(awardName, defaultCount);
		value.text = val.ToString();
	}
}
