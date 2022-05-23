using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointManager : MonoBehaviour
{

    public List<CheckpointLogic> checkpoints = new List<CheckpointLogic>();

   
    public int currentCheckpointNum;
    public int advanceRate;
    public int checkpointFrontier;

    // Start is called before the first frame update
    void Start()
    {
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
            checkpoints[i].gameObject.SetActive(true);
        }
    }

    public void UpdateCheckpoints()
    {

        //if(currentCheckpointNum != checkpoints.Count - 1)
        //{
        //    currentCheckpointNum++;

        //    for (int i = 0; i < checkpoints.Count; i++)
        //    {
        //        if (i == currentCheckpointNum)
        //        {
        //            checkpoints[i].gameObject.SetActive(true);
        //        }
        //        else
        //        {

        //            checkpoints[i].gameObject.SetActive(false);
        //        }
        //    }
        //}
        //else
        //{
        //    checkpoints[currentCheckpointNum].gameObject.SetActive(false);
        //    Debug.Log("Complete!!");
        //}

        //AdvanceCheckpoints();


    }

    public void AdvanceCheckpoints(CheckpointLogic checkpoint)
    {

        float num = checkpoint.checkpointNumber;

        AdvanceFrontier();
        MoveCheckPoint(checkpoint);

        for (int i = 0; i < checkpoints.Count; i++)
        {

            if(checkpoints[i].checkpointNumber < num)
            {
                AdvanceFrontier();
                MoveCheckPoint(checkpoints[i]);
            }

        }

        
    }

    public void AdvanceFrontier()
    {
        currentCheckpointNum++;
        checkpointFrontier = advanceRate * currentCheckpointNum;
    }

    public void RemoveFromTail(CheckpointLogic checkpoint)
    {
        checkpoints.Remove(checkpoint);

    }

    public void AddToHead(CheckpointLogic checkpoint)
    {
        checkpoints.Add(checkpoint);
    }

    public void MoveCheckPoint(CheckpointLogic checkpoint)
    {
        checkpoint.transform.position = new Vector3(checkpoint.transform.position.x, checkpoint.transform.position.y, checkpointFrontier);
        checkpoint.checkpointNumber = currentCheckpointNum;
    }



}
