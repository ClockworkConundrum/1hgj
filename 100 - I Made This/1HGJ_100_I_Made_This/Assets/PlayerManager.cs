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
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public bool GameActive;
    public string PlayerName;
    public int CopyrightsInfringed;
    public float PlayerSpeed = 5f;

    public Text Text;

    private ParticleSystem _system;
    private TextMesh _textMesh;

	// Use this for initialization
	void Start()
    {
        _textMesh = GetComponentInChildren<TextMesh>();
        _system = GetComponentInChildren<ParticleSystem>();

        Activate();
	}
	
	// Update is called once per frame
	void Update()
    {
        if (this.GameActive)
        {
            Vector3 direction = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical")).normalized;

            transform.Translate(direction * Time.deltaTime * this.PlayerSpeed);

            this.Text.text = "Copyrights Infringed: " + this.CopyrightsInfringed;
        }
	}

    public void Activate()
    {
        this.GameActive = true;
    }

    public void Deactivate()
    {
        this.GameActive = false;
    }

    public void SetName(string name)
    {
        this.PlayerName = name;
        _textMesh.text = name;
    }

    public void InfringeACopyright()
    {
        this.CopyrightsInfringed++;
        _system.Play();
    }

    public void Reset()
    {
        this.PlayerName = string.Empty;
        _textMesh.text = string.Empty;
        this.CopyrightsInfringed = 0;
    }
}
