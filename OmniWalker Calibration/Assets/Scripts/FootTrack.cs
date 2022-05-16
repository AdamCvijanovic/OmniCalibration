using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootTrack : MonoBehaviour
{

    public Animator anim;

    public GameObject graphicsObj;
    public GameObject forwardDirection;

    public GameObject LFoot;
    public GameObject RFoot;

    public OmniController_Example OmniController;
    public OmniMovementComponent OmniMovement;


    public Vector3 Roffset;
    public Vector3 RVelocity;
    public Vector3 Loffset;
    public Vector3 LVelocity;
    public Vector2 hidInput;

    [Header("-----Right Foot Data-----")]
    public Vector3 RAccel;
    public Quaternion RQuat;
    public Vector3 RGyro;

    [Header("-----Left Foot Data-----")]
    public Vector3 LAccel;
    public Quaternion LQuat;
    public Vector3 LGyro;

    public OmniCommon.Messages.OmniRawData rawData;
    public OmniCommon.Messages.OmniMotionData motionData;
    public OmniCommon.Messages.OmniMotionAndRawData motionAndRawData;

    // Start is called before the first frame update
    void Start()
    {
        Roffset = RFoot.transform.localPosition;
        Loffset = LFoot.transform.localPosition;
        OmniController = GetComponent<OmniController_Example>();
        OmniMovement = GetComponent<OmniMovementComponent>();

        motionData = OmniMovement.motionData;
        rawData = OmniMovement.rawData;

    }

    // Update is called once per frame
    void Update()
    {
        motionAndRawData = OmniMovement.motionAndRawData;
        UpdateOmniData();

        hidInput = OmniMovement.hidInput;
        UpdateFeetPositions();

        UpdateForwardDirection();
    }

    public void UpdateOmniData()
    {

        if (motionAndRawData != null)
        {

            //Acceleration

            for (int i = 0; i < motionAndRawData.Pod1Accelerometer.Length; i++)
            {
                RAccel[i] = motionAndRawData.Pod1Accelerometer[i];
                LAccel[i] = motionAndRawData.Pod2Accelerometer[i];
            }

            //Quaternions

            for (int i = 0; i < motionAndRawData.Pod1Quaternions.Length; i++)
            {
                RQuat[i] = motionAndRawData.Pod1Quaternions[i];
                LQuat[i] = motionAndRawData.Pod2Quaternions[i];
            }

            //Gyroscope

            for (int i = 0; i < motionAndRawData.Pod1Gyroscope.Length; i++)
            {
                RGyro[i] = motionAndRawData.Pod1Gyroscope[i];
                LGyro[i] = motionAndRawData.Pod2Gyroscope[i];
            }

        }
    }

    public void UpdateFeetPositions()
    {

        //We first need to remove the influence of gravity by finding the current UP vector
        
        //RAccel = RQuat * RAccel;
        RAccel.y = RAccel.y + 0.5f;
        //LAccel = LQuat * LAccel;
        LAccel.y = LAccel.y + 0.5f;
        //Vector3 newAccelR = RQuat * RAccel; 


        //RFoot.transform.rotation =  RQuat * Quaternion.Euler(0,0,0);

        //normalize or not?
        //RFoot.transform.localPosition = Roffset + (0.5f * RAccel);

        RFoot.transform.localPosition = Vector3.SmoothDamp(RFoot.transform.localPosition, Roffset + RAccel, ref RVelocity, Time.deltaTime, 10f);

        //LFoot.transform.rotation = LQuat;
        //LFoot.transform.localPosition = Loffset + (0.5f*LAccel);
        LFoot.transform.localPosition = Vector3.SmoothDamp(LFoot.transform.localPosition, Loffset + LAccel, ref LVelocity, Time.deltaTime, 10f);



    }

    public void UpdateForwardDirection()
    {
        graphicsObj.transform.forward = forwardDirection.transform.forward;
    }

}
