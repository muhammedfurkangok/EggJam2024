using DG.Tweening;
using UnityEngine;

namespace ScriptableObjects
{
    public enum GrabItemType
    {
        Cube,
        LightPrism,
        Teddy,
        Screwdriver,
        Screw,
        DoorMain,
        DoorFrameTop,
        DoorFrame0,
        DoorFrame1,
        DoorHandleInside,
        DoorHandleOutside,
        DoorHingeTop,
        DoorHingeDown,
    }

    public enum GrabItemColor
    {
        Color1,
        Color2,
        Color3
    }

    //[CreateAssetMenu(fileName = "GameConstants", menuName = "GameConstants", order = 0)]
    public class GameConstants : ScriptableObject
    {
        [Header("Player Movement")]
        public float playerWalkSpeed;
        public float playerRunSpeed;
        public float playerWalkAcceleration;
        public float playerRunAcceleration;
        public float playerDeceleration;
        public float playerWalkingSoundInterval;

        [Header("Player Look")]
        public float playerMinLook;
        public float playerMaxLook;
        public float playerLookSensitivity;
        public float playerInspectSensitivity;

        [Header("Player Noise")]
        public float playerIdleNoiseAmplitude;
        public float playerWalkNoiseAmplitude;
        public float playerRunNoiseAmplitude;
        public float playerIdleNoiseFrequency;
        public float playerWalkNoiseFrequency;
        public float playerRunNoiseFrequency;
        public float playerWalkNoiseChangeSpeed;
        public float playerRunNoiseChangeSpeed;

        [Header("Player Grab")]
        public LayerMask grabLayer;
        public float grabRange;
        public float grabForce;
        public float grabAngularForce;
        public float grabReleaseDeceleration;
        public float grabItemEnterInspectDuration;
        public Ease grabItemEnterInspectEase;
        public float grabItemExitInspectDuration;
        public Ease grabItemExitInspectEase;
        public float grabThrowForce;
        public LayerMask grabItemExcludeLayers;

        [Header("Tablet")]
        public float tabletMoveDuration;
        public Ease tabletMoveUpEase;
        public Ease tabletMoveDownEase;
        public float tabletUpLocalY;
        public float tabletDownLocalY;
        public float tabletAlphaDuration;
        public Ease tabletAlphaEase;

        [Header("Grab Item Snap")]
        public float grabItemPositionColliderSizeOffset;
        public float grabItemSnapMaxVelocity;
        public float grabItemSnapDuration;
        public Ease grabItemSnapEase;
        public Ease grabItemSnapRotationEase;
        public Color snapColor;
        public float snapColorChangeDuration;
        public Ease snapColorChangeEase;

        [Header("Door")]
        public Vector3 doorOpenRotation;
        public float doorOpenDuration;
        public Ease doorOpenEase;
        public Vector3 doorCloseRotation;
        public float doorCloseDuration;
        public Ease doorCloseEase;

        [Header("Player Interact")]
        public float playerInteractRange;
        public LayerMask playerInteractLayer;

        [Header("General")]
        public Color transparentColor;
    }
}