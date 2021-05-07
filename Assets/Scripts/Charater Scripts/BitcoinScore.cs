using UnityEngine.UI;
using UnityEngine;

public class BitcoinScore : MonoBehaviour
{
    public Text bitcoinScore;
    private int score;

    // Use this for initialization
    void Start()
    {
        score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        bitcoinScore.text = score.ToString();
    }

    public void incrementBitcoinScore() 
    {
        score++;
    }

}
