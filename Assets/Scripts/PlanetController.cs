using UnityEngine;
using Valve.VR.InteractionSystem;

[RequireComponent(typeof(DrawBuildZone))]
[RequireComponent(typeof(Interactable))]

public class PlanetController : MonoBehaviour
{
    private Vector3 oldPosition;
    private Quaternion oldRotation;

    private float attachTime;

    private Hand.AttachmentFlags attachmentFlags = Hand.defaultAttachmentFlags & (~Hand.AttachmentFlags.SnapOnAttach) & (~Hand.AttachmentFlags.DetachOthers);

    [SerializeField]
    [Tooltip("Minimum radius from the centre of the object a unit can be built.")]
    private float minBuildZone = 0;

    [SerializeField]
    [Tooltip("Maximum radius from the centre of the object a unit can be built.")]
    private float maxBuildZone = 0;

#if UNITY_EDITOR
    [SerializeField]
    [Tooltip("Editor only feature to view the unit build zone of the planet.")]
    private bool showBuildZone = false;

    private bool oldShowBuildZone;
    private float oldMinBuildZone;
    private float oldMaxBuildZone;
#endif

    public GameObject unitPrefab;
    private GameObject unit;
    private GameObject unitModel;

    //TODO: User clicks on planet for menu to display what units can be build around it, so need an array of unit prefabs

    private void Start()
    {

#if UNITY_EDITOR
        SetBuildZone();

        this.GetComponent<DrawBuildZone>().DisplayBuildZone(showBuildZone);
        oldShowBuildZone = showBuildZone;
#endif
    }

    private void Update()
    {
#if UNITY_EDITOR


        if(oldMinBuildZone != minBuildZone || oldMaxBuildZone != maxBuildZone) {
            if(unit) {
                unit.GetComponent<UnitController>().SetBuildZoneMinMax(minBuildZone, maxBuildZone);
            }

            SetBuildZone();
        }

        if (oldShowBuildZone != showBuildZone) {
            this.GetComponent<DrawBuildZone>().DisplayBuildZone(showBuildZone);
            oldShowBuildZone = showBuildZone;
        }
#endif
    }

    private void SetBuildZone()
    {
        // Scales the min and max values to correct size for planets scale
        float minBuildZoneScaled = (minBuildZone / this.transform.localScale.x);
        float maxBuildZoneScaled = maxBuildZone / this.transform.localScale.x;

        this.GetComponent<DrawBuildZone>().SetBuildZoneMinMax(minBuildZoneScaled, maxBuildZoneScaled);
        oldMinBuildZone = minBuildZone;
        oldMaxBuildZone = maxBuildZone;
    }


    //-------------------------------------------------
    // Called when a Hand starts hovering over this object
    //-------------------------------------------------
    private void OnHandHoverBegin(Hand hand)
    {
        //textMesh.text = "Hovering hand: " + hand.name;
    }


    //-------------------------------------------------
    // Called when a Hand stops hovering over this object
    //-------------------------------------------------
    private void OnHandHoverEnd(Hand hand)
    {
        //textMesh.text = "No Hand Hovering";
    }


    //-------------------------------------------------
    // Called every Update() while a Hand is hovering over this object
    //-------------------------------------------------
    private void HandHoverUpdate(Hand hand)
    {
        if(hand.GetStandardInteractionButtonDown()) {
            if(!unit && unitPrefab) {
                unit = (GameObject)Instantiate(unitPrefab, 
                    this.transform.position,
                    Quaternion.identity,
                    this.transform.parent.transform);
                unit.GetComponent<UnitController>().SetBuildZoneMinMax(minBuildZone, maxBuildZone);
                unitModel = unit.GetComponent<UnitController>().PlaceUnit();
            }

            if(hand.currentAttachedObject != unitModel) {
                // Call this to continue receiving HandHoverUpdate messages,
                // and prevent the hand from hovering over anything else
                hand.HoverLock(GetComponent<Interactable>());

                // Attach this object to the hand
                hand.AttachObject(unitModel, attachmentFlags);
            }
        } else if (hand.GetStandardInteractionButtonUp()) {
            hand.DetachObject(unitModel);
            hand.HoverUnlock(GetComponent<Interactable>());
            unit.GetComponent<UnitController>().ExitUnitPlacement();
            unit = null;
            unitModel = null;
        }


        //if(hand.GetStandardInteractionButtonDown() || ((hand.controller != null) && hand.controller.GetPressDown(Valve.VR.EVRButtonId.k_EButton_Grip))) {
        //    if(hand.currentAttachedObject != gameObject) {
        //        // Save our position/rotation so that we can restore it when we detach
        //        oldPosition = transform.position;
        //        oldRotation = transform.rotation;

        //        // Call this to continue receiving HandHoverUpdate messages,
        //        // and prevent the hand from hovering over anything else
        //        hand.HoverLock(GetComponent<Interactable>());

        //        // Attach this object to the hand
        //        hand.AttachObject(gameObject, attachmentFlags);
        //    } else {
        //        // Detach this object from the hand
        //        hand.DetachObject(gameObject);

        //        // Call this to undo HoverLock
        //        hand.HoverUnlock(GetComponent<Interactable>());

        //        // Restore position/rotation
        //        transform.position = oldPosition;
        //        transform.rotation = oldRotation;
        //    }
        //}
    }


    //-------------------------------------------------
    // Called when this GameObject becomes attached to the hand
    //-------------------------------------------------
    private void OnAttachedToHand(Hand hand)
    {
        //textMesh.text = "Attached to hand: " + hand.name;
        //attachTime = Time.time;
    }


    //-------------------------------------------------
    // Called when this GameObject is detached from the hand
    //-------------------------------------------------
    private void OnDetachedFromHand(Hand hand)
    {
        //textMesh.text = "Detached from hand: " + hand.name;
    }


    //-------------------------------------------------
    // Called every Update() while this GameObject is attached to the hand
    //-------------------------------------------------
    private void HandAttachedUpdate(Hand hand)
    {
        //textMesh.text = "Attached to hand: " + hand.name + "\nAttached time: " + ( Time.time - attachTime ).ToString( "F2" );
    }


    //-------------------------------------------------
    // Called when this attached GameObject becomes the primary attached object
    //-------------------------------------------------
    private void OnHandFocusAcquired(Hand hand)
    {
    }


    //-------------------------------------------------
    // Called when another attached GameObject becomes the primary attached object
    //-------------------------------------------------
    private void OnHandFocusLost(Hand hand)
    {
    }
}