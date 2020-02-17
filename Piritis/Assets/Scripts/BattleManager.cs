using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManager : MonoBehaviour
{
    public enum TResults { Win, Draw, Loose, Void }
    TResults[] results = new TResults[2];
    float[] PlayerRolls = new float[2];
    float[] EnemyRolls = new float[2];
    CartaObject[] playerHand = new CartaObject[2];
    CartaObject[] enemyHand = new CartaObject[2];

    public void Battle(CartaObject[] pH, CartaObject[] eH)
    {
        playerHand = pH;
        enemyHand = eH;
        RollDice();
        ApplyPasives();
        CheckResults();
    }
    public TResults[] ReturnResults() { return results; }
    public void FlushValues()
    {
        for (int i = 0; i < 3; i++)
        {
            results[i] = TResults.Void;
            PlayerRolls[i] = 0;
            EnemyRolls[i] = 0;
            playerHand[i] = null;
            enemyHand[i] = null;
        }
    }
    public void RollDice()
    {
        for (int i = 0; i < PlayerRolls.Length; i++)
            PlayerRolls[i] = Random.Range(1, 6);
        for (int i = 0; i < EnemyRolls.Length; i++)
            EnemyRolls[i] = Random.Range(1, 6);
    }
    public void CheckResults()
    {
        for (int i = 0; i < 3; i++)
        {
            if (results[i] == TResults.Void)
            {
                if (PlayerRolls[i] < EnemyRolls[i])
                    results[i] = TResults.Loose;
                else if (PlayerRolls[i] > EnemyRolls[i])
                    results[i] = TResults.Win;
                else
                    results[i] = TResults.Draw;
            }
        }
    }
    public void ApplyPasives()
    {
        for (int i = 0; i < 3; i++)
        {
            //PlayerPasive
            switch (playerHand[i].pasiva)
            {
                case CartaObject.TipoPasiva.BUEN_EMPATE:
                    break;
                case CartaObject.TipoPasiva.CAMBIAR_VALOR:
                    break;
                case CartaObject.TipoPasiva.CAPITANÍA:
                    break;
                case CartaObject.TipoPasiva.HUIDA:
                    break;
                case CartaObject.TipoPasiva.KING_SLAYER:
                    break;
                case CartaObject.TipoPasiva.MAL_EMPATE:
                    break;
                case CartaObject.TipoPasiva.SUMA_ALIADOS:
                    break;
                case CartaObject.TipoPasiva.SUMA_ORO:
                    break;
                case CartaObject.TipoPasiva.SUPER_EMPATE:
                    break;
            }
            if (results[i] != TResults.Void)
                return;
            //EnemyPasive
            switch (enemyHand[i].pasiva)
            {
                case CartaObject.TipoPasiva.BUEN_EMPATE:
                    break;
                case CartaObject.TipoPasiva.CAMBIAR_VALOR:
                    break;
                case CartaObject.TipoPasiva.CAPITANÍA:
                    break;
                case CartaObject.TipoPasiva.HUIDA:
                    break;
                case CartaObject.TipoPasiva.KING_SLAYER:
                    break;
                case CartaObject.TipoPasiva.MAL_EMPATE:
                    break;
                case CartaObject.TipoPasiva.SUMA_ALIADOS:
                    break;
                case CartaObject.TipoPasiva.SUMA_ORO:
                    break;
                case CartaObject.TipoPasiva.SUPER_EMPATE:
                    break;
            }
        }
    }
}
