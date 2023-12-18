using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Niantic.Lightship.AR;
using Niantic.Lightship.AR.VpsCoverage;

public class SharedARCoverageAPI : MonoBehaviour
{
    // References to components
    [SerializeField]
    public CoverageClientManager CoverageClient;

    private Dictionary<string, string> LocationToPayload = new();

    private void OnTryGetCoverage(AreaTargetsResult args)
    {
        var areaTargets = args.AreaTargets;

        // Sort the area targets by proximity to the user
        areaTargets.Sort((a, b) =>
            a.Area.Centroid.Distance(args.QueryLocation).CompareTo(
                b.Area.Centroid.Distance(args.QueryLocation)));

        // Populate the dictionary with the 5 nearest names and anchors
        for (var i = 0; i < 5; i++)
        {
            LocationToPayload[areaTargets[i].Target.Name] = areaTargets[i].Target.DefaultAnchor;
        }
    }

    void Start()
    {
        CoverageClient.TryGetCoverage(OnTryGetCoverage);
    }
}
