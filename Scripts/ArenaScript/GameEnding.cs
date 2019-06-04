﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameEnding : MonoBehaviour
{
    public float aFadeDuration = 1f;
    public float aDisplayImageDuration = 1f;
    
    public CanvasGroup aMageWin;
    public CanvasGroup aKnightWin;

    public int p_WinMaxCount;
    public GameObject aWallsPrefab;

    private PlayerManager aPlayerManager;
    private Respawn aRespawn;
    private float aTimer = 0;
    private GameObject aWalls;
    private bool aEndGame = false;

    void Start()
    {
        aWalls = GameObject.Find("/Walls");
        

        GameObject vRespawn = GameObject.Find("Respawn");
        aRespawn = vRespawn.GetComponent<Respawn>();
        aRespawn.setRespawnPoints();

        GameObject vPlayerManagerGameObject = GameObject.Find("PlayerManager");

        if (vPlayerManagerGameObject != null)
        {
            aPlayerManager = vPlayerManagerGameObject.GetComponent<PlayerManager>();
            aPlayerManager.setGameEnding(this);
        }


    }

    public void winnerFound(int pWinnerNumber)
    {
        aPlayerManager.addPoint(pWinnerNumber);
        int vScore = aPlayerManager.getScore(pWinnerNumber);

        if (vScore == p_WinMaxCount)
        {
            aEndGame = true;

        }
        else
        {
            ResetArena();
        }
    }

    private void Update()
    {
        if (aEndGame)
        {
            EndGame(aMageWin, true);
        }
    }


    public void ResetArena()
    {
        aPlayerManager.CreatePlayers();
        Destroy(aWalls);
        aWalls = Instantiate(aWallsPrefab);
    }

    void EndGame(CanvasGroup pImageCanvasGroup, bool pDoRestart)
    {
        Debug.Log("EndGame");
        aTimer += Time.deltaTime;
        pImageCanvasGroup.alpha = aTimer / aFadeDuration;
        if (aTimer > aFadeDuration + aDisplayImageDuration)
        {
            if (pDoRestart)
            {
                Destroy(aPlayerManager);
                SceneManager.LoadScene(0);
            }
            else
            {
                Debug.Log("shouldquit");
                Application.Quit();
            }
        }
    }
}

