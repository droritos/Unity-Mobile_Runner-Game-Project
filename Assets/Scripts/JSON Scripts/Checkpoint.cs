using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public string saveFileName = "checkpoint_save.json";
    public int saveScoreThreshold = 500; // Save every 500 points
    private int lastSavedScore = 0;

    private float _score;


    void Update()
    {
        _score = ScoreManager.Instance.GetScore();
        if (_score >= lastSavedScore + saveScoreThreshold)
        {
            lastSavedScore = (int)_score; // Update the last saved score
            Debug.Log("Score threshold reached and state saved.");
        }
    }
}
