using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointLogic : MonoBehaviour
{

    public CheckpointManager _checkpointManager;
    public int checkpointNumber;
    public bool active;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetManager(CheckpointManager mngr)
    {
        _checkpointManager = mngr;
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Checkpoint " + gameObject.name + " Triggered" );
        if(_checkpointManager != null)
        {
            _checkpointManager.UpdateCheckpoints();
        }

    }

}
