using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public List<CheckpointLogic> checkpoints = new List<CheckpointLogic>();
   
    public int currentCheckpointNum;

    // Start is called before the first frame update
    void Start()
    {
        currentCheckpointNum = 0;

        SetCheckpointsActive();

        for (int i = 0; i < checkpoints.Count; i++)
        {
            checkpoints[i].SetManager(this);
            checkpoints[i].transform.SetParent(this.transform);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCheckpointsActive()
    {
        for (int i = 0; i < checkpoints.Count; i++)
        {
            if (i == currentCheckpointNum)
            {
                checkpoints[i].gameObject.SetActive(true);
            }
            else
            {

                checkpoints[i].gameObject.SetActive(false);
            }
        }
    }

    public void UpdateCheckpoints()
    {

        if(currentCheckpointNum != checkpoints.Count - 1)
        {
            currentCheckpointNum++;

            for (int i = 0; i < checkpoints.Count; i++)
            {
                if (i == currentCheckpointNum)
                {
                    checkpoints[i].gameObject.SetActive(true);
                }
                else
                {

                    checkpoints[i].gameObject.SetActive(false);
                }
            }
        }
        else
        {
            checkpoints[currentCheckpointNum].gameObject.SetActive(false);
            Debug.Log("Complete!!");
        }

        
    }
}
