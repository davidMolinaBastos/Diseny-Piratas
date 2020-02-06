using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    List<IRestartGameElement> m_ResetNodes = new List<IRestartGameElement>();
    CardBlackboard cb;

    float gold,treasureParts;

    private void Start()
    {
        cb = gameObject.GetComponent<CardBlackboard>();
    }
    public void DisplayIslandHUD()
    {

    }
    public void StartEvent(EventNodeScript evento)
    {

    }

    public void AddResetElement(IRestartGameElement RestartGameElement)
    {
        m_ResetNodes.Add(RestartGameElement);
    }
}
public interface IRestartGameElement
{
    void ResetNode();
}