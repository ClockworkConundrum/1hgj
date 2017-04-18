using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Movement : MonoBehaviour
{
    public float speed;
    public float sensitivity = 0.1f;

    public GameObject target;

    bool timerActive = false;

    List<Fire> projectiles;

	// Use this for initialization
	void Start()
    {
        this.projectiles = new List<Fire>();
	}
	
	// Update is called once per frame
	void Update()
    {
        float vertAxis = Input.GetAxis("Vertical");
        float horzAxis = Input.GetAxis("Horizontal");

        Vector3 direction = new Vector3(horzAxis, 0f, vertAxis).normalized;

	    if (Mathf.Abs(vertAxis) > this.sensitivity || Mathf.Abs(horzAxis) > this.sensitivity)
        {
            SetTimerActive(true);
            target.transform.Translate(direction * Time.deltaTime);
        }
        else
        {
            SetTimerActive(false);
        }

        if (Input.GetAxis("Fire1") > this.sensitivity)
        {
            GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Quad);
            projectile.transform.position = target.transform.position;
            Fire fire = projectile.AddComponent<Fire>();
            fire.speed = 10f;

            Ray ray = Camera.main.ViewportPointToRay(Camera.main.ScreenToViewportPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 1f)));


            float deltaY = ray.origin.y;

            float dirY = ray.direction.y;

            float multiplier = deltaY / dirY;

            Debug.DrawRay(ray.origin, ray.direction * multiplier);

            Vector3 rayDirection = ray.direction * multiplier;
            Vector3 point = rayDirection - ray.origin;

            Debug.Log(point);

            Vector3 projectileDirection = target.transform.position - point;

            fire.direction = projectileDirection;

            this.projectiles.Add(fire);
        }
	}

    void SetTimerActive(bool active)
    {
        if (this.timerActive != active)
        {
            this.timerActive = active;
            Debug.Log("Movement.cs: " + (this.timerActive ? "StartTimer" : "StopTimer"));
            BroadcastMessage(this.timerActive ? "StartTimer" : "StopTimer");

            foreach (Fire projectile in this.projectiles)
            {
                projectile.SetTimerActive(this.timerActive);
            }
        }
    }
}
