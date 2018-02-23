using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour {

    public float m_moveSpeed = 6;

    Rigidbody m_rigidbody;
    Camera m_viewCamera;
    Vector3 m_velocity;

	void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        m_viewCamera = Camera.main;
	}
	
	void Update () {
        //Vector3 mousePos = m_viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, m_viewCamera.transform.transform.position.y));
        Vector3 mousePos = GetWorldPositionOnPlane(Input.mousePosition, 0);
        transform.LookAt(mousePos + Vector3.up * transform.position.y);
        m_velocity = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical")).normalized * m_moveSpeed;
	}

    void FixedUpdate()
    {
        m_rigidbody.MovePosition(m_rigidbody.position + m_velocity * Time.fixedDeltaTime);
    }

    public Vector3 GetWorldPositionOnPlane(Vector3 screenPosition, float z)
    {
        Ray ray = Camera.main.ScreenPointToRay(screenPosition);
        Plane xy = new Plane(Vector3.up, new Vector3(0, 0, z));
        float distance;
        xy.Raycast(ray, out distance);
        return ray.GetPoint(distance);
    }

}
