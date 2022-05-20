using UnityEngine;

public class SwingEffect : MonoBehaviour
{
    [SerializeField] private Transform _swingTarget;
    [SerializeField] float _swingFactor;

    void Update()
    {
        transform.rotation = new Quaternion(transform.rotation.x,transform.rotation.y,_swingTarget.position.y * _swingTarget.position.x * _swingFactor / 20 ,transform.rotation.w);
    }
}
