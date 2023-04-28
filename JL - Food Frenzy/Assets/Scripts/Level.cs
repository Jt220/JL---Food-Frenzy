using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    public enum LevelType
    {
        TIMER,
        OBSTACLE,
        MOVES,
    };

    public GameGrid grid;
    public HUD hud;

    public int score1Star;
    public int score2Star;
    public int score3Star;

    private int score2;

    public Text scoreText;

    protected LevelType type;

    public LevelType Type
    {
        get { return type; }
    }

    protected int currentScore;

    protected bool didWin;

    // Start is called before the first frame update
    private void Start()
    {
        hud.SetScore(currentScore);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void GameWin()
    {
        didWin = true;
        grid.GameOver();
        StartCoroutine(WaitForGridFill());
    }

    public virtual void GameLose()
    {
        didWin = false;
        grid.GameOver();
        StartCoroutine(WaitForGridFill());
    }

    public virtual void OnMove()
    {
        
    }

    public virtual void OnPieceCleared(GamePiece piece)
    {
        //Update Score
        currentScore = currentScore + piece.score;
        hud.SetScore(currentScore);
        Debug.Log(currentScore);
    }

    protected virtual IEnumerator WaitForGridFill()
    {
        while (grid.IsFilling)
        {
            yield return 0;
        }

        if(didWin && !grid.IsFilling)
        { 
            hud.SetScore(currentScore);
            Invoke("DelayGameWin", 1f);
            Debug.Log("passed");
        }
        else
        {
            hud.OnGameLose();
        }

    }
    public void DelayGameWin()
    {
        hud.OnGameWin(currentScore);
    }
}
