using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardControl : MonoBehaviour
{

    private CharacterController m_CharacterController;
    public float m_keySpeed;
    public float m_rotSpeed;

    // Start is called before the first frame update
    void Start()
    {

        m_CharacterController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        if (Input.GetKey(KeyCode.W))
        {
            m_CharacterController.SimpleMove(transform.forward * m_keySpeed);
        }

        if (Input.GetKey(KeyCode.S))
        {
            m_CharacterController.SimpleMove(transform.forward * -m_keySpeed);
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.up * -m_rotSpeed * Time.deltaTime);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(Vector3.up * m_rotSpeed * Time.deltaTime);
        }
    }
}
