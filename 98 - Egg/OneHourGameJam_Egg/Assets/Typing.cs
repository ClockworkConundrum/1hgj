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
using System.Text;

public class Typing : MonoBehaviour
{

    public enum State
    {
        None,
        E,
        G
    }

    public State state = State.None;

    public int times = 0;
    public int score = 0;
    public int multiplier = 1;

    public float roundTime = 10f;
    public float time;

    public StringBuilder uTyped = new StringBuilder();

	// Use this for initialization
	void Start()
	{
	    time = roundTime;
	}
	
	// Update is called once per frame
	void Update()
    {

	    if (time > 0f)
	    {

	        if (state == State.None)
	        {
	            if (Input.GetKeyUp(KeyCode.E))
	            {
	                state = State.E;
	            }
	            else if (LetterKeyUp())
	            {
	                score -= multiplier;
	                multiplier = 1;
	            }
	        }
	        else if (state == State.E)
	        {
	            if (Input.GetKeyUp(KeyCode.G))
	            {
	                state = State.G;
	            }
	            else if (LetterKeyUp())
	            {
	                score -= multiplier;
	                multiplier = 1;
	                state = State.None;
	            }
	        }
	        else if (state == State.G)
	        {
	            if (Input.GetKeyUp(KeyCode.G))
	            {
	                state = State.None;
	                times++;
	                score += multiplier;
	                multiplier++;
	            }
	            else if (LetterKeyUp())
	            {
	                score -= multiplier;
	                multiplier = 1;
	                state = State.None;
	            }
	        }

	        time -= Time.deltaTime;

	        uTyped.Append(Input.inputString);
	    }
	    else
	    {
	        if (Input.GetKeyUp(KeyCode.Return))
	        {
	            time = roundTime;

	            times = 0;
	            score = 0;
	            multiplier = 1;
                uTyped = new StringBuilder();
	        }
	    }
	}

    bool LetterKeyUp()
    {

        for (int i = 1; i < 26; i++)
        {
            KeyCode key = KeyCode.A + i;

            if (Input.GetKeyUp(key)) return true;
        }

        return false;
    }

    void OnGUI()
    {
        if (time > 0f)
        {
            GUILayout.Label("Type the word \"EGG\" as many times as possible.");
            GUILayout.Label("Score: " + score);
            GUILayout.Label("Multiplier: " + multiplier + "x");
            GUILayout.Label("Time remaining: " + time.ToString("F2") + "s");
        }
        else
        {
            GUILayout.Label("Time's up!");
            GUILayout.Label("Score: " + score);
            GUILayout.Label("You typed \"EGG\" " + times + " times");
            GUILayout.Label("You typed: " + uTyped.ToString());
            GUILayout.Label("Press Enter to restart");
        }
    }
}
