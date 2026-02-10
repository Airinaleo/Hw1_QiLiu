using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CustomGrab : MonoBehaviour
{
    [Header("Settings")]
    public bool doubleRotation = false; 
    public InputActionReference toggleAction; 

    [Header("References")]
    public InputActionReference grabAction;
    public CustomGrab otherHand = null;

    [Header("State")]
    public List<Transform> nearObjects = new List<Transform>();
    public Transform grabbedObject = null;
    
    private Vector3 lastPos;
    private Quaternion lastRot;

    private void Start()
    {
        grabAction.action.Enable();
        if (toggleAction != null) toggleAction.action.Enable();

        foreach (CustomGrab c in transform.parent.GetComponentsInChildren<CustomGrab>())
        {
            if (c != this) otherHand = c;
        }
        
        lastPos = transform.position;
        lastRot = transform.rotation;
    }

    void Update()
    {
        // 1. Extra Credit: Toggle Double Rotation logic
        if (toggleAction != null && toggleAction.action.WasPressedThisFrame())
            doubleRotation = !doubleRotation;

        bool isGrabbing = grabAction.action.IsPressed();

        if (isGrabbing)
        {
            if (grabbedObject == null)
                grabbedObject = nearObjects.Count > 0 ? nearObjects[0] : otherHand?.grabbedObject;

            if (grabbedObject != null)
            {
                // 2. Calculate Frame Delta
                Vector3 deltaPos = transform.position - lastPos;
                Quaternion deltaRot = transform.rotation * Quaternion.Inverse(lastRot);

                // 3. Extra Credit: Double Rotation (10pts)
                if (doubleRotation)
                {
                    deltaRot.ToAngleAxis(out float angle, out Vector3 axis);
                    if (angle > 0.01f) deltaRot = Quaternion.AngleAxis(angle * 2.0f, axis);
                }

                // 4. Requirement: Combined Position (10pts)
                grabbedObject.position += deltaPos;

                // 5. Requirement: Orbiting - Rotate around controller (10pts)
                Vector3 oldOffset = grabbedObject.position - transform.position;
                Vector3 newOffset = deltaRot * oldOffset;
                // Apply the positional offset caused by rotation
                grabbedObject.position += (newOffset - oldOffset);

                // 6. Requirement: Combined Rotation (10pts)
                grabbedObject.rotation = deltaRot * grabbedObject.rotation;
            }
        }
        else 
        { 
            grabbedObject = null; 
        }

        // 7. Save current state for delta calculation in next frame
        lastPos = transform.position;
        lastRot = transform.rotation;
    }

    private void OnTriggerEnter(Collider other) 
    { 
        if (other.CompareTag("grabbable")) nearObjects.Add(other.transform); 
    }
    
    private void OnTriggerExit(Collider other) 
    { 
        if (other.CompareTag("grabbable")) nearObjects.Remove(other.transform); 
    }
}