using System.Collections;
using UnityEngine;

public class AudioSyncScale : AudioSyncer
{
    
    [SerializeField] private Vector3 _beatScale;
    
    private Vector3 _restScale;
    

    private void Awake()
    {
        _restScale = transform.localScale;
    }
    private IEnumerator MoveToScale(Vector3 _target)
    {
        Vector3 curr = transform.localScale;
        Vector3 initial = curr;
        float timer = 0;

        while (curr != _target)
        {
            curr = Vector3.Lerp(initial, _target, timer / _timeToBeat);
            timer += Time.deltaTime;

            transform.localScale = curr;

            yield return null;
        }

        _isBeat = false;
    }

    public override void OnUpdate()
    {
        base.OnUpdate();

        if (_isBeat) return;

        transform.localScale = Vector3.Lerp(transform.localScale, _restScale, _restSmoothTime * Time.deltaTime);
    }

    public override void OnBeat()
    {
        base.OnBeat();

        StopCoroutine("MoveToScale");
        StartCoroutine("MoveToScale", _beatScale);
    }
}
