using UnityEngine.UI;
using UnityEngine;

public class BitcoinScore : MonoBehaviour
{
    public Text bitcoinScore;
    private int score;
    private GameObject gameManagerObject;
    private GameManager gameManager;

    // Use this for initialization
    void Start()
    {
        gameManagerObject = GameObject.Find("GameManager");
        gameManager = gameManagerObject.GetComponent<GameManager>();
        
        score = gameManager.currentBitcoin;
    }

    // Update is called once per frame
    void Update()
    {
        bitcoinScore.text = score.ToString();
    }

    public void incrementBitcoinScore() 
    {
        score++;
        gameManager.currentBitcoin = score;
    }

}
