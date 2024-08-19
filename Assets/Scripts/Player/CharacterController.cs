using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_jump;
    [SerializeField] List<string> m_Grounds;
    [SerializeField] AudioSource m_jumpAudio;

    [SerializeField] List <string> m_EnemyTags;
    [SerializeField] string m_movingPlatformTag;
    public ESizeState m_currentSizeState;

    private float m_move;
    private EState m_currentState;
    public bool m_isGrounded;
    public bool m_isAccomplished;
    public bool m_isChangeSizeAvail = true;


    public Action<sizeState> StateChanged;

    [SerializeField] private Rigidbody2D m_Rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_currentState = EState.Idle;
        StateChanged += OnStateChanged;
    }

    private void OnDestroy()
    {
        StateChanged -= OnStateChanged;
    }

    private void OnStateChanged(sizeState a_sizeData)
    {
        m_currentSizeState = a_sizeData.SizeState;
        m_speed = a_sizeData.Speed;
        m_jump = a_sizeData.Jump;
        m_jumpAudio.clip = a_sizeData.m_clip;
        transform.localScale = transform.localScale * a_sizeData.Size;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0 && m_isGrounded == true)
            m_currentState = EState.Idle;

        if (Input.GetAxis("Horizontal") != 0)
        {
            m_move = Input.GetAxis("Horizontal");
            Debug.Log("State on Move : " + m_currentState + " MoveSpeed : " + m_move);
            m_Rigidbody.velocity = new Vector2(m_speed * m_move, m_Rigidbody.velocity.y);
            if(m_isGrounded == true)
                m_currentState = EState.Moving;
        }
        if (Input.GetButtonDown("Jump") && m_currentState != EState.Jumping && m_isGrounded)
        {
            Debug.Log("State on Jump : " + m_currentState);
            m_jumpAudio.Play();
            m_isGrounded = false;
            m_Rigidbody.AddForce(new Vector2(m_Rigidbody.velocity.x, m_jump));
            m_currentState = EState.Jumping;
        }
    }

    private void OnCollisionEnter2D(Collision2D a_other)
    {
        if (a_other.transform.CompareTag("Walls"))
            return;
        if (m_Grounds.Exists(ground => a_other.transform.CompareTag(ground)))
        {
            m_currentState.Equals(EState.Idle);
            m_isGrounded = true;
        }
        if (a_other.gameObject.tag == m_movingPlatformTag)
            this.transform.parent = a_other.transform;
        if (m_EnemyTags.Exists(enemy => a_other.transform.CompareTag(enemy)))
        {
            m_currentState.Equals(EState.Dead);
            LevelManager.MissionFailed(transform.position);
            Debug.Log("MISSION FAILED!!!!");
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Walls"))
            return;
        if (m_Grounds.Exists(ground => collision.transform.CompareTag(ground)))
        {
            m_currentState.Equals(EState.Idle);
            m_isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "MovingPlatform")
        {
            if(transform.parent.gameObject.activeSelf)
                transform.parent = null;
        }
        if (m_Grounds.Exists(ground => other.transform.CompareTag(ground)))
        {
            m_isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("DontChangeSize"))
            m_isChangeSizeAvail = false;
        if (m_currentSizeState.Equals(ESizeState.small))
        {
            if (other.CompareTag(LevelManager.MissionCompletedTag_Small))
            {
                m_isAccomplished = true;
                LevelManager.CheckIfMissionCompleted();
            }
        }
        if (m_currentSizeState.Equals(ESizeState.large))
        {
            if (other.CompareTag(LevelManager.MissionCompletedTag_Large))
            {
                m_isAccomplished = true;
                LevelManager.CheckIfMissionCompleted();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("DontChangeSize"))
            m_isChangeSizeAvail = true;
        if (other.CompareTag(LevelManager.MissionCompletedTag_Small))
            m_isAccomplished = false;

        if (other.CompareTag(LevelManager.MissionCompletedTag_Large))
            m_isAccomplished = false;
    }
}

public enum EState : byte
{
    Idle = 0,
    Moving = 1,
    Jumping = 2,
    Dead = 3,
}

public enum ESizeState : byte
{
    small = 0,
    large = 1,
}
