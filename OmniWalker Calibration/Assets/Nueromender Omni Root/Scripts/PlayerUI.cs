using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    public GameObject _player;
    public TextMeshProUGUI _scoreText;
    public TextMeshProUGUI _distanceText;

    public float zOffset;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerManager>().gameObject;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        UIAnchor();
    }

    public void UIAnchor()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y, _player.transform.position.z + zOffset);
    }

    public void DisplayScore(int score)
    {
        _scoreText.text = ("Score: " + score);
    }

    public void DisplayDistance(float distance)
    {
        _distanceText.text = ("Distance: " + Mathf.RoundToInt(distance));
    }

}
