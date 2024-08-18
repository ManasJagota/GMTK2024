using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEnemy : MonoBehaviour
{
    [SerializeField] Vector3 m_endPosition;
    [SerializeField] float m_timeToreachEndPoint;
    [SerializeField] float m_WaitToStartEnemy;

    bool m_isEnemyStarted;
    Vector3 m_startPosition;

    private void Start()
    {
        m_startPosition = transform.position;
        StartCoroutine(WaitToStartEnemyMove());
    }

    IEnumerator WaitToStartEnemyMove()
    {
        yield return new WaitForSeconds(m_WaitToStartEnemy);
        StartCoroutine(Move());    
    }

    IEnumerator Move()
    {
        for (float t = 0; t < 1; t += Time.deltaTime / m_timeToreachEndPoint)
        {
            transform.position = Vector3.Lerp(m_startPosition, m_endPosition, t);
            yield return null;
        }
    }
}
