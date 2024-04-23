using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class LineAnimControl : MonoBehaviour
{
    private Coroutine LineFlicker = null;
    [SerializeField]
    private UILineRenderer myLineRenderer;

    private void Start()
    {
        myLineRenderer.enabled = false;
        if (LineFlicker != null)
        {
            StopCoroutine(LineFlicker);
            LineFlicker = null;
        }
        LineFlicker = StartCoroutine(LineRoutine());
    }

    private IEnumerator LineRoutine()
    {
        int i = 0;
        while(i < 2)
        {
            myLineRenderer.enabled = true;
            yield return new WaitForSeconds(2);
            myLineRenderer.enabled = false;
            yield return new WaitForSeconds(2);
            myLineRenderer.enabled = true;
            yield return new WaitForSeconds(2);
            myLineRenderer.enabled = false;
            yield return new WaitForSeconds(4);
            i++;
        }
    }

    private void OnDestroy()
    {
        if (LineFlicker != null)
        {
            StopCoroutine(LineFlicker);
            LineFlicker = null;
        }
    }
}
