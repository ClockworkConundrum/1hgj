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
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class OneButton : MonoBehaviour
{
    public List<Button> Buttons;

    public Text ScoreText;
    public Text TimeText;

    public int Score;
    public float RemainingTime;

    public float InitialTime;

    private int MagicButton;


	// Use this for initialization
	void Start()
    {
        UpdateButtons();
        this.RemainingTime = this.InitialTime;
        StartCoroutine("GameTime");
	}
	
	// Update is called once per frame
	void Update()
    {
        this.ScoreText.text = "Score: " + this.Score;
        this.TimeText.text = "Time: " + this.RemainingTime.ToString("F2");
	}

    IEnumerator GameTime()
    {
        while (true)
        {
            while (this.RemainingTime > 0f)
            {
                this.RemainingTime -= Time.deltaTime;
                yield return null;
            }

            this.RemainingTime = this.InitialTime;
            this.Score = 0;
            yield return null;
        }
    }

    void ClickCorrect()
    {
        this.Score++;
        this.UpdateButtons();
        this.RemainingTime = this.InitialTime;
    }

    void ClickIncorrect()
    {
        this.Score = 0;
        this.UpdateButtons();
        this.RemainingTime = this.InitialTime;
    }

    void UpdateButtons()
    {
        int newButton = Random.Range(0, this.Buttons.Count - 1);

        if (newButton >= this.MagicButton) newButton++;

        this.MagicButton = newButton;

        for (int i = 0; i < this.Buttons.Count; i++)
        {
            Button currentButton = this.Buttons[i];

            Text buttonText = currentButton.GetComponentInChildren<Text>();

            if (i == this.MagicButton)
            {
                buttonText.text = GenerateSum(equalsOne: true);

                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(ClickCorrect);

            }
            else
            {
                buttonText.text = GenerateSum(equalsOne: false);

                currentButton.onClick.RemoveAllListeners();
                currentButton.onClick.AddListener(ClickIncorrect);
            }
        }
    }

    string GenerateSum(bool equalsOne)
    {
        string pattern = "{0} {1} {2}";

        int a = Random.Range(1, 10);

        int b;

        string op;

        if (equalsOne)
        {
            b = a - 1;

            op = "-";
        }
        else
        {
            b = Random.Range(1, 10);

            int opVal;

            if (b == a - 1)
            {
                opVal = Random.Range(1, 3);
            }
            else
            {
                opVal = Random.Range(0, 3);
            }

            switch (opVal)
            {
                case 0:
                    op = "-";
                    break;
                case 1:
                    op = "+";
                    break;
                case 2:
                    op = "*";
                    break;
                default:
                    op = "/";
                    break;
            }
        }

        return string.Format(pattern, a, op, b);
    }
}
