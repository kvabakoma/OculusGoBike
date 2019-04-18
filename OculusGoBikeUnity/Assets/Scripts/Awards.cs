using UnityEngine;

[System.Serializable]
public struct Award
{
	public string name;

	public int defaultCount;

	[HideInInspector]
	public int count;

	public GameObject visual;
}

[CreateAssetMenu(menuName = "Awards")]
public class Awards : ScriptableObject
{
	public Award[] awards;

	public void ResetValues()
	{
		for(int i = 0; i < awards.Length; i++)
		{
			awards[i].count = awards[i].defaultCount;
			PlayerPrefs.SetInt(awards[i].name, awards[i].count);
		}
		PlayerPrefs.Save();
	}
}