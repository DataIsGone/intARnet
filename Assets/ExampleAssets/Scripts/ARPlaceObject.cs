// CREDIT: Patryk Galach
// SOURCE: https://www.patrykgalach.com/2020/02/24/markerless-ar-with-ar-foundation-in-unity/

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
/*
#if UNIT_IOS
    using Microsoft.Azure.SpatialAnchors.Unity.IOS.ARKit;
#elif UNITY_ANDROID
    using Microsoft.Azure.SpatialAnchors.Unity.Android;
#endif
*/
namespace Microsoft.Azure.SpatialAnchors {
    [RequireComponent(typeof(ARRaycastManager))]
    public class ARPlaceObject : MonoBehaviour
    {
        // For Azure Spatial Anchors
        //private CloudSpatialAnchorSession cloudSession;
        private XRCameraFrame xRCameraFrame;

        // Reference to the AR Raycast Manager
        private ARRaycastManager raycastManager;

        // Prefab which will be spawned in the real world.
        [SerializeField]
        private GameObject prefab;

        // Instance of the prefab.
        private GameObject prefabInstance;

        private void Start()
        {
            // --- Azure Spatial Anchor setup
            //this.cloudSession = new CloudSpatialAnchorSession();
            SetUpAzureKeys();

            // Assumes iPhone or Android device is being used
            //this.cloudSession.Session = aRSession.subsystem.nativePtr.GetPlatformPointer();
            //this.cloudSession.Start();

            raycastManager = GetComponent<ARRaycastManager>();
            prefabInstance = Instantiate(prefab);
            prefabInstance.SetActive(false);
        }

        private void Update()
        {
            // // List of the hit points in real world.
            // var hitList = new List<ARRaycastHit>();
            // // Raycast from the center of the screen which should hit only detected surfaces.
            // if (raycastManager.Raycast(new Vector2(Screen.width / 2, Screen.height / 2), hitList, TrackableType.PlaneWithinBounds | TrackableType.PlaneWithinPolygon))
            // {
            //     // In the instance is inactive, enable it.
            //     if (!prefabInstance.activeInHierarchy)
            //     {
            //         prefabInstance.SetActive(true);
            //     }
            //     // Sort hit list base on distance to the camera.
            //     hitList = hitList.OrderBy(h => h.distance).ToList();
            //     var hitPoint = hitList[0];
            //     // Position instance in the closest hit point.
            //     prefabInstance.transform.position = hitPoint.pose.position;
            //     prefabInstance.transform.up = hitPoint.pose.up;
            // }
            // else
            // {
            //     // In the instance is active, disable it.
            //     if (prefabInstance.activeInHierarchy)
            //     {
            //         prefabInstance.SetActive(false);
            //     }
            // }

            Vector3 hitPosition = new Vector3();
            Vector2 screenCenter = new Vector2(0.5f, 0.5f);
            List<ARRaycastHit> aRRaycastHits = new List<ARRaycastHit>();
           /* if(arRaycastManager.Raycast(screenCenter, aRRaycastHits) && aRRaycastHits.Count > 0)
            {
                ARRaycastHit hit = aRRaycastHits[0];
                hitPosition = hit.pose.position;
            }*/

            Quaternion rotation = Quaternion.AngleAxis(0, Vector3.up);
            /*this.localAnchor = GameObject.Instantiate(prefab, hitPosition, rotation);
            this.localAnchor.AddComponent<CloudNativeAnchor>();

            // Shows content at this anchor
            // Then saves when the user confirms placement
            CloudNativeAnchor cloudNativeAnchor = this.localAnchor.GetComponent<CloudNativeAnchor>();
            if (cloudNativeAnchor.CloudAnchor == null)
            { 
                cloudNativeAnchor.NativeToCloud();
            }  
            CloudSpatialAnchor cloudAnchor = cloudNativeAnchor.CloudAnchor;
            await this.cloudSession.CreateAnchorAsync(cloudAnchor);
            this.feedback = $"Created a cloud anchor with ID={cloudAnchor.Identifier}";

            // Needs sufficient environment data captured before
            // trying to create a new cloud spatial anchor
            // `RecommendedForCreateProgress` must be above 1.0
            SessionStatus value = await this.cloudSession.GetSessionStatusAsync();
            if (value.RecommendedForCreateProgress < 1.0f) return;
            */
        }

        private void SetUpAzureKeys()
        {
        //this.cloudSession.Configuration.AccountKey = @"MyAccountKey";
        //this.cloudSession.Configuration.AccessToken = @"MyAccessToken";
        //this.cloudSession.Configuration.AuthenticationToken = @"MyAuthenticationToken";
        }

        private void ProvideFramesToSession()
        {/*
            if (aRCameraManager.subsystem.TryGetLatestFrame(cameraParams, out xRCameraFrame))
            {
                long latestFrameTimeStamp = xRCameraFrame.timestampNs;

                bool newFrameToProcess = latestFrameTimeStamp > lastFrameProcessedTimeStamp;

                if (newFrameToProcess)
                {
                    session.ProcessFrame(xRCameraFrame.nativePtr.GetPlatformPointer());
                    lastFrameProcessedTimeStamp = latestFrameTimeStamp;
                }
            }
            */
        }
    } // end class
}