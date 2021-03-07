using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PolyToolkit;
using System;

public class PolyModelManager : MonoBehaviour
{
    List<PolyAsset> polies;
    public GameObject entry;
    public GameObject imageList;
    public List<GameObject> entries;
    int numEntries = 40;
    int counter = 0;
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
        req.pageSize = numEntries;
        // Send the request.
        PolyApi.ListAssets(req, MyCallback);        
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
        GameObject model = Instantiate(result.Value.gameObject, Camera.main.transform.position + Camera.main.transform.forward * 1.5f, Quaternion.identity);
        model.AddComponent<MoveObj>();
        model.AddComponent<BoxCollider>();
        Destroy(GameObject.Find("PolyImport"));
    }

    public void buttonPress(PolyAsset asset) {
        PolyApi.Import(asset, PolyImportOptions.Default(), Spawn);       
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
        int i = 0;
        //Render thumbnails
        foreach (PolyAsset poly in polies)
        {            
            PolyApi.FetchThumbnail(poly, DisplayMenu);
            Vector3 pos = new Vector3(imageList.transform.position.x + 100 * i, imageList.transform.position.y, imageList.transform.position.z);
            entries.Add(Instantiate(entry, imageList.transform.position, Quaternion.identity));
            entry.name = "poly_" + i;
            entries[i].GetComponent<RectTransform>().gameObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
            entries[i].GetComponent<RectTransform>().position = new Vector2(entries[i].GetComponent<RectTransform>().position.x + i * 130, entries[i].GetComponent<RectTransform>().position.y);
            entries[i].SetActive(true);
            entries[i].transform.parent = imageList.transform;
            UnityEngine.Events.UnityAction action = delegate { buttonPress(poly); };
            entries[i].GetComponent<Button>().onClick.AddListener(action);
            ++i;
        }
    }

    void DisplayMenu(PolyAsset asset, PolyStatus status)
    {
        if (!status.ok)
        {
            // Handle error;
            return;
        }
        // Display the asset.thumbnailTexture.
        entries[counter].GetComponent<RawImage>().texture = asset.thumbnailTexture;
        counter++;
    }
}
