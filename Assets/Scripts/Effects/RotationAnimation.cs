using System;
using UnityEngine;
using UnityEngine.UIElements;

public class RotationAnimation : MonoBehaviour
{
    [SerializeField] AxisType AxisToRotate;
    [SerializeField] float RotationSpeed;

    private void Start()
    {
        Rotate();
    }

    private void Rotate()
    {
        // Rotate axis loop 
    }
    private void RotateAxis()
    {
        switch (AxisToRotate)
        {
            
        }
    }
    
   
    
}

public enum AxisType
{
    X,
    Y,
    Z,
    All
};