/*
    Copyright (c) 2017 Dan Livings

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
*/

using UnityEngine;

public class Timewarp : MonoBehaviour
{
    public float startTime;
    public float remainingTime;

    public bool timerActive;

    public UnityEngine.UI.Text text;

	// Use this for initialization
	void Start()
    {
        remainingTime = startTime;
        this.text.text = "Time remaining: " + this.remainingTime.ToString("F2");
    }
	
	// Update is called once per frame
	void Update()
    {
        if (this.remainingTime <= 0f) StopTimer();

	    if (timerActive)
        {
            remainingTime -= Time.deltaTime;
            this.text.text = "Time remaining: " + this.remainingTime.ToString("F2");
        }
	}

    public void StartTimer()
    {
        this.timerActive = true;
        BroadcastTimerActive();
    }

    public void StopTimer()
    {
        this.timerActive = false;
        BroadcastTimerActive();
    }

    void BroadcastTimerActive()
    {
        Debug.Log("Timewarp.cs: SetTimerActive(" + this.timerActive + ")");
        BroadcastMessage("SetTimerActive", this.timerActive);
    }

    public bool GetTimerActive()
    {
        return this.timerActive;
    }
}
