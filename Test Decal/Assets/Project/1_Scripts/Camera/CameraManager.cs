using DG.Tweening;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager Instance;
    [SerializeField] private GameObject mainCamera;
    public Camera CurrentCamera { get; private set; }
    public Camera PreviousCamera { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void SwitchTo(Camera newCam, bool followPlayer = false, Vector3 zonePosition = default(Vector3), Ease easeFunction = Ease.InOutExpo, float easeDuration = 0.5f)
    {
        if (newCam == CurrentCamera) return;

        // set the new and previous camera
        // we store previous camera in order to NOT deactivate element associated immediately when we leave the zone but when we enter a new zone 
        PreviousCamera = CurrentCamera;
        CurrentCamera = newCam;

        if (mainCamera == null) return;
        
        if (followPlayer)
        {
            mainCamera.transform.DOLocalMove(zonePosition, easeDuration).SetEase(easeFunction);            
        }
    }
}
