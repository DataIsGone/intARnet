using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PolyToolkit;
using System;

public class PolyTest : MonoBehaviour
{
    List<PolyAsset> polies;
    public RawImage img;
    // Start is called before the first frame update
    void Start()
    {
        PolyListAssetsRequest req = new PolyListAssetsRequest();
        // Only curated assets:
        req.curated = true;
        // Limit complexity to medium.
        req.maxComplexity = PolyMaxComplexityFilter.MEDIUM;
        // Only Blocks objects.
        req.formatFilter = PolyFormatFilter.BLOCKS;
        // Order from best to worst.
        req.orderBy = PolyOrderBy.BEST;
        // Up to 20 results per page.
        req.pageSize = 20;
        // Send the request.
        PolyApi.ListAssets(req, MyCallback);
        
    }

    // Update is called once per frame
    void Update()
    {
        PolyAsset asset;
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            asset = polies[0];
            PolyApi.Import(asset, PolyImportOptions.Default(), Spawn);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            asset = polies[1];
            PolyApi.Import(asset, PolyImportOptions.Default(), Spawn);
        }
    }

    private void Spawn(PolyAsset asset, PolyStatusOr<PolyImportResult> result)
    {
        if (!result.Ok)
        {
            // Handle error.
            Debug.Log("Issue spawning");
            return;
        }
        // Success. Place the result.Value.gameObject in your scene.
        Instantiate(result.Value.gameObject, result.Value.gameObject.transform.position, Quaternion.identity);
    }

    void MyCallback(PolyStatusOr<PolyListAssetsResult> result)
    {
        if (!result.Ok)
        {
            // Handle error.
            return;
        }
        // Success. result.Value is a PolyListAssetsResult and
        // result.Value.assets is a list of PolyAssets.
        polies = result.Value.assets;
        //Render thumbnails
        foreach (PolyAsset poly in polies)
        {
            Debug.Log(poly.name);
            PolyApi.FetchThumbnail(poly, DisplayMenu);
        }
    }

    void DisplayMenu(PolyAsset asset, PolyStatus status) {
        if (!status.ok)
        {
            // Handle error;
            return;
        }
        // Display the asset.thumbnailTexture.
        img.texture = asset.thumbnailTexture;
    }
}
