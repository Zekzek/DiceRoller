using System;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    [SerializeField] private List<DieSide> sides;
    private Rigidbody _rigidbody;
    
    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Roll()
    {
        _rigidbody.velocity = 10 * Vector3.up;
        _rigidbody.AddTorque(new Vector3(UnityEngine.Random.Range(-1000, 1000), UnityEngine.Random.Range(-1000, 1000), UnityEngine.Random.Range(-1000, 1000)), ForceMode.Impulse);
        _rigidbody.AddForce(UnityEngine.Random.Range(20, 100) * Vector3.up + UnityEngine.Random.Range(-1000, 1000) * Vector3.right + UnityEngine.Random.Range(-1000, 1000) * Vector3.back);
    }

    public bool IsComplete()
    {
        return _rigidbody.velocity.sqrMagnitude < 0.001f && _rigidbody.angularVelocity.sqrMagnitude < 0.001f;
    }

    public int Evaluate()
    {
        DieSide topSide = sides[0];
        foreach (DieSide side in sides) {
            if (side.transform.position.y >= topSide.transform.position.y) {
                topSide = side;
            }
        }
        return topSide.value;
    }

    [Serializable]
    private struct DieSide
    {
        public int value;
        public Transform transform;
    }
}
