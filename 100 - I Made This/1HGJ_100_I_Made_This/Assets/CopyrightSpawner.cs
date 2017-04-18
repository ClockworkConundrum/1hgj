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

public class CopyrightSpawner : MonoBehaviour
{
    public GameObject CopyrightObject;
    public PlayerManager PlayerManager;

	// Use this for initialization
	void Start()
    {
        StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update()
    {
	
	}

    IEnumerator Spawn()
    {
        while (true)
        {
            if (this.PlayerManager.GameActive) GameObject.Instantiate(this.CopyrightObject, GetRandomDropLocation(15f, 10f), Quaternion.identity);

            yield return new WaitForSeconds(2f);
        }
    }

    Vector3 GetRandomDropLocation(float scale, float dropHeight)
    {
        Vector2 direction = Random.insideUnitCircle;

        return new Vector3(direction.x * scale, dropHeight, direction.y * scale);
    }
}
