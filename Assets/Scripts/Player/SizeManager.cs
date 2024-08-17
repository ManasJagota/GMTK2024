using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] KeyCode m_changeSizeKey;
    [SerializeField] List<sizeState> SizeStateList;
    [SerializeField] List<CharacterController> m_characterList;
    [SerializeField] float m_changeSizeTimer = 2f;

    bool m_isSizeChanged;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(m_changeSizeKey))
        {
            if(!m_isSizeChanged)
                ChangeSize();
        }
    }

    public void ChangeSize()
    {
        foreach (CharacterController Character in m_characterList)
        {
            if (Character.m_isChangeSizeAvail == false)
                return;
        }
        OnCharacterSizeStateChanged();
    }


    void OnCharacterSizeStateChanged()
    {
        m_isSizeChanged = true;
        foreach(CharacterController Character in m_characterList)
        {
            if (Character.m_currentSizeState.Equals(ESizeState.large))
            {
                sizeState ChangeSizeState = SizeStateList.Find(l_SizeState => l_SizeState.SizeState.Equals(ESizeState.small));
                Character.StateChanged(ChangeSizeState);
            }
            else if (Character.m_currentSizeState.Equals(ESizeState.small))
            {
                sizeState ChangeSizeState = SizeStateList.Find(l_SizeState => l_SizeState.SizeState.Equals(ESizeState.large));
                Character.StateChanged(ChangeSizeState);
            }
        }
        StartCoroutine(StartTimerToChangeSize());
    }

    IEnumerator StartTimerToChangeSize()
    {
        yield return new WaitForSeconds(m_changeSizeTimer);
        m_isSizeChanged = false;
    }
}

[Serializable]
public struct sizeState
{
    public ESizeState SizeState;
    public float Speed;
    public float Jump;
    public float Size;
}
