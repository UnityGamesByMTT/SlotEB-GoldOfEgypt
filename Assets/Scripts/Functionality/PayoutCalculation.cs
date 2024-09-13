using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI.Extensions;

public class PayoutCalculation : MonoBehaviour
{
    [SerializeField]
    private int x_Distance;
    [SerializeField]
    private int y_Distance;

    [SerializeField]
    private Transform LineContainer;
    [SerializeField]
    private GameObject Line_Prefab;

    [SerializeField]
    private Vector2 InitialLinePosition = new Vector2(-315, 100);

    GameObject TempObj = null;


    internal void GeneratePayoutLinesBackend(List<int> y_index, int Count, bool isStatic = false)
    {
        GameObject MyLineObj = Instantiate(Line_Prefab, LineContainer);
        MyLineObj.transform.localPosition = new Vector2(InitialLinePosition.x, InitialLinePosition.y);
        UILineRenderer MyLine = MyLineObj.GetComponent<UILineRenderer>();
        for (int i = 0; i < Count; i++)
        {
            var points = new Vector2() { x = i * x_Distance, y = y_index[i] * -y_Distance };
            var pointlist = new List<Vector2>(MyLine.Points);
            pointlist.Add(points);
            MyLine.Points = pointlist.ToArray();
        }
        var newpointlist = new List<Vector2>(MyLine.Points);
        newpointlist.RemoveAt(0);
        MyLine.Points = newpointlist.ToArray();

        if (isStatic)
        {
            TempObj = MyLineObj;
        }
    }

    internal void ResetStaticLine()
    {
        if(TempObj!=null)
        {
            Destroy(TempObj);
            TempObj = null;
        }
    }

    internal void ResetLines()
    {
        foreach (Transform child in LineContainer)
        {
            Destroy(child.gameObject);
        }
    }
}
