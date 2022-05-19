using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public PlayerUI _playerUI;

    public float _distance;
    public int _score;

    // Start is called before the first frame update
    void Start()
    {
        _playerUI = FindObjectOfType<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateDistance();
    }

    public void UpdateScore()
    {
        _score++;
        _playerUI.DisplayScore(_score);
    }

    public void UpdateDistance()
    {
        _distance = transform.position.z - 20;
        _playerUI.DisplayDistance(_distance);
    }
}
