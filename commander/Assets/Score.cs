using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	public void setScore(int ChangedScore)
	{
		GetComponent<Text>().text = ChangedScore.ToString();
	}
	
}
