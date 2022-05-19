using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLogic : MonoBehaviour
{

    public PlayerManager _player;

    public CheckpointManager _checkpointManager;
    public int checkpointNumber;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        _player = FindObjectOfType<PlayerManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        CheckpointMissed();
    }

    public void SetManager(CheckpointManager mngr)
    {
        _checkpointManager = mngr;
    }

    public void CheckpointMissed()
    {
        if(_player.transform.position.z > this.transform.position.z + 10)
        {
            _checkpointManager.AdvanceCheckpoints(this);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint " + gameObject.name + " Triggered" );
        if(_checkpointManager != null)
        {
            //_checkpointManager.UpdateCheckpoints();
            _checkpointManager.AdvanceCheckpoints(this);
        }

        if(_player != null)
        {
            _player.UpdateScore();
        }

    }

    public void ActivateCheckpoint()
    {
        gameObject.SetActive(true);
    }

    public void DectivateCheckpoint()
    {
        gameObject.SetActive(false);
    }

}
