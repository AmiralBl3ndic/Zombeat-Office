using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionPeriod : MonoBehaviour {
    
    public RhythmEventProvider eventProvider;

    public PlayerScript player;

    private int halfPeriodFrames = 0;


    private int frameCount = 0; // The framecount
    private bool countFrames = false;


    private bool actionTime = false;
    


	// Use this for initialization
	void Start () {
        eventProvider.Beat += initializer; 
	}

	// Update is called once per frame
	void Update () {
		
	}

    // To remove events
    void onDestroy ()
    {
        eventProvider.SubBeat -= onSubBeat;
    }


    private void initializer (Beat beat)
    {
        eventProvider.FrameChanged += initFrameCounter;
        eventProvider.SubBeat += initHalfQuarterBeat;
    }


    // This method is called only during the first quarter beat of the song
    private void initHalfQuarterBeat(Beat beat, int beatIndex)
    {
        eventProvider.FrameChanged -= initFrameCounter;
        eventProvider.SubBeat -= initHalfQuarterBeat;
        eventProvider.Beat -= initializer;

        eventProvider.SubBeat += onSubBeat;
        eventProvider.FrameChanged += onFrameChanged;

        halfPeriodFrames = halfPeriodFrames / 2;
    }


    private void initFrameCounter(int a, int b)
    {
        halfPeriodFrames += 1;
    }



    // Used for counting frames in the action period
    private void onFrameChanged (int a, int b) // Do not know what a and b are, but seems to work (update: nothing better after checking official documentation)
    {
        if (countFrames)
        {
            frameCount += 1;

            // Checking if the number of frames counted corresponds to the beginning of the action period
            if (frameCount == halfPeriodFrames)
            {
                actionTime = true;
                player.actionPeriod = true;
                player.hasMoved = false;
            }

            // Checking if the number of frames counted corresponds to the end of the action period
            if (frameCount == 2 * halfPeriodFrames)
            {
                actionTime = false;
                player.actionPeriod = false;
            }
        }
    }


    private void onSubBeat(Beat beat, int beatIndex)
    {
        // Checking if we are in the 3rd (and last) quarter beat, if so starting to count beats
        if (beatIndex == 3)
        {
            countFrames = true;
        }
    }
}
