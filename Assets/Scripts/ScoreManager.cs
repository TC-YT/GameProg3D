using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance; //singleton for global access

    public TextMeshProUGUI TxtScore;

    public TextMeshProUGUI TxtScoreGameOver;

    public int totalScore;

    public int distanceMultiplier = 1;

    private Transform player;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        UpdateScore();
    }

    public void UpdateScore()
    {
        totalScore = Mathf.FloorToInt(player.position.z * distanceMultiplier);
        TxtScore.text = totalScore.ToString();
        TxtScoreGameOver.text = totalScore.ToString();
    }

    public void ScoreRestart()
    {
        totalScore = 0;
    }
}
