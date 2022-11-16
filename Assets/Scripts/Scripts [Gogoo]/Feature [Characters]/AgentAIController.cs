using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgentAIController : MonoBehaviour
{
    public enum CharacterStates { Normal, Stuned }

    [Header("Agent AI Controller")]
    [SerializeField] private CharacterStates _characterState;
    private AgentAI _agentAI;

    void Start()
    {

        _agentAI = GetComponent<AgentAI>();
        _characterState = CharacterStates.Normal;
    }

    // Update is called once per frame
    void Update()
    {
        

        switch (_characterState)
        {
            case CharacterStates.Normal:
                if (_agentAI != null)
                {
                    _agentAI.Light.SetActive(true);
                    _agentAI.FieldOfView();
                    _agentAI.AIStateMachine();
                    _agentAI.AudioControll();
                }
                break;
            case CharacterStates.Stuned:
                if (_agentAI != null)
                    _agentAI.Light.SetActive(false);
                break;
        }
    }
}
