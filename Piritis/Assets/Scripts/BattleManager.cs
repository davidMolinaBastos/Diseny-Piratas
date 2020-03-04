﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum TResults { Win, Draw, Loose, Void }
    TResults[] results = new TResults[3];
    int[] PlayerRolls = new int[3];
    int[] EnemyRolls = new int[3];
    CartaObject[] playerHand = new CartaObject[3];
    CartaObject[] enemyHand = new CartaObject[3];
    bool doubled = false;

    public float masacreWinnings = 400;
    public float capturaWinnings = 300;
    public float pillajeWinnings = 250;
    public float bravataWinnings = 200;
    public float trifulcaWinnings = 150;

    public void Battle(CartaObject[] pH, CartaObject[] eH) //Start
    {
        results = new TResults[]{ TResults.Void, TResults.Void, TResults.Void };
        playerHand = pH;
        enemyHand = eH;
        RollDice();
        print("PlayerRolls: " + PlayerRolls[0] + " " +
            PlayerRolls[1] + " " + PlayerRolls[2]);
        print("EnemyRolls: " + EnemyRolls[0] + " " +
            EnemyRolls[1] + " " + EnemyRolls[2]);
        ApplyPasives();
        CheckResults();
        print("Results Pre Empates " + results[0] + " " + results[1] + " " + results[2]);
        ApplyLatePasives();
        CheckResults();
        print("Results" + results[0] + " " + results[1] + " " + results[2]);

        print("PlayerRolls: " + PlayerRolls[0] + " " +
            PlayerRolls[1] + " " + PlayerRolls[2]);
        print("EnemyRolls: " + EnemyRolls[0] + " " +
            EnemyRolls[1] + " " + EnemyRolls[2]);
    }

    //Public Handling
    public TResults[] ReturnResults() { return results; }
    public int[] ReturnPlayerRolls() { return PlayerRolls; }
    public float GetWinnings()
    {
        float wins, defeats, draws;
        float total = wins = defeats = draws = 0;

        for (int i = 0; i < 3; i++)
            if (results[i] == TResults.Win)
                wins++;
            else if (results[i] == TResults.Loose)
                defeats++;
            else
                draws++;
        switch (wins)
        {
            case 3:
                total = masacreWinnings;
                break;
            case 2:
                if (draws > 0)
                    total = capturaWinnings;
                else
                    total = pillajeWinnings;
                break;
            case 1:
                if (draws > 1)
                    total = bravataWinnings;
                else if (draws > 0)
                    total = trifulcaWinnings;
                break;
        }
        return doubled ? total * 2 : total;
    }
    public void FlushValues()
    {
        for (int i = 0; i < 3; i++)
        {
            results[i] = TResults.Void;
            PlayerRolls[i] = 0;
            EnemyRolls[i] = 0;
            playerHand[i] = null;
            enemyHand[i] = null;
            doubled = true;
        }
    }

    //Computacion
    void RollDice()
    {
        Random.InitState((int)System.DateTime.Now.Ticks);
        for (int i = 0; i < PlayerRolls.Length; i++)
            PlayerRolls[i] = Random.Range(1, 6);
        for (int i = 0; i < EnemyRolls.Length; i++)
            EnemyRolls[i] = Random.Range(1, 6);
    }
    void CheckResults()
    {
        for (int i = 0; i < 2; i++)
        {
            if (results[i] == TResults.Void)
            {
                if (PlayerRolls[i] < EnemyRolls[i])
                {
                    results[i] = TResults.Loose;
                    print("Loose");
                }
                else if (PlayerRolls[i] > EnemyRolls[i])
                {
                    results[i] = TResults.Win;
                    print("Win");
                }
                else
                {
                    results[i] = TResults.Draw;
                    print("Draw");
                }
            }
        }
    }
    void ApplyLatePasives()
    {
        for (int i = 0; i < 3; i++)
        {
            //PlayerPasive
            switch (playerHand[i].pasiva)
            {
                case CartaObject.TipoPasiva.BUEN_EMPATE:
                    if (results[i] == TResults.Draw)
                        results[i] = TResults.Win;
                    break;
                case CartaObject.TipoPasiva.MAL_EMPATE:
                    if (results[i] == TResults.Draw)
                        results[i] = TResults.Loose;
                    break;
                case CartaObject.TipoPasiva.SUPER_EMPATE:
                    if (results[i] == TResults.Draw)
                        results[i] = TResults.Win;
                    break;
            }
            if (results[i] != TResults.Draw)
                return;
            //EnemyPasive
            switch (enemyHand[i].pasiva)
            {
                case CartaObject.TipoPasiva.BUEN_EMPATE:
                    if (results[i] == TResults.Draw)
                        results[i] = TResults.Loose;
                    break;
                case CartaObject.TipoPasiva.MAL_EMPATE:
                    if (results[i] == TResults.Draw)
                        results[i] = TResults.Win;
                    break;
                case CartaObject.TipoPasiva.SUPER_EMPATE:
                    if (results[i] == TResults.Draw)
                        results[i] = TResults.Loose;
                    break;
            }
        }
    }
    void ApplyPasives()
    {
        for (int i = 0; i < 3; i++)
        {
            //PlayerPasive
            switch (playerHand[i].pasiva)
            {
                case CartaObject.TipoPasiva.CAMBIAR_VALOR:
                    PlayerRolls[i] += playerHand[i].valor;
                    break;
                case CartaObject.TipoPasiva.CAPITANÍA:
                    for (int j = 0; j < 3; j++)
                        PlayerRolls[j] += 2;
                    break;
                case CartaObject.TipoPasiva.HUIDA:
                    if (playerHand[i].levelCarta < enemyHand[i].levelCarta)
                        results[i] = TResults.Draw;
                    break;
                case CartaObject.TipoPasiva.KING_SLAYER:
                    PlayerRolls[i] *= 2;
                    break;
                case CartaObject.TipoPasiva.SUMA_ALIADOS:
                    for (int j = 0; j < 3; j++)
                        if (j != i)
                            PlayerRolls[j] += 2;
                    break;
                case CartaObject.TipoPasiva.SUMA_ORO:
                    PlayerRolls[i] += 1;
                    doubled = true;
                    break;
                case CartaObject.TipoPasiva.SUPER_EMPATE:
                    PlayerRolls[i] += 1;
                    break;
            }
            if (results[i] != TResults.Void)
                return;
            //EnemyPasive
            switch (enemyHand[i].pasiva)
            {
                case CartaObject.TipoPasiva.CAMBIAR_VALOR:
                    EnemyRolls[i] += enemyHand[i].valor;
                    break;
                case CartaObject.TipoPasiva.CAPITANÍA:
                    for (int j = 0; j < 3; j++)
                        EnemyRolls[j] += 2;
                    break;
                case CartaObject.TipoPasiva.HUIDA:
                    if (playerHand[i].levelCarta > enemyHand[i].levelCarta)
                        results[i] = TResults.Draw;
                    break;
                case CartaObject.TipoPasiva.KING_SLAYER:
                    EnemyRolls[i] *= 2;
                    break;
                case CartaObject.TipoPasiva.SUMA_ALIADOS:
                    for (int j = 0; j < 3; j++)
                        if (j != i)
                            EnemyRolls[j] += 2;
                    break;
                case CartaObject.TipoPasiva.SUMA_ORO:
                    EnemyRolls[i] += 1;
                    break;
                case CartaObject.TipoPasiva.SUPER_EMPATE:
                    EnemyRolls[i] += 1;
                    break;
            }
        }
    }
}
