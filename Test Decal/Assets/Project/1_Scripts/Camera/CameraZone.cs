using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class CameraZone : MonoBehaviour
{
    // The camera linked to this area
    public Camera zoneCamera; 
    
    [Header("Options")]
    [SerializeField] private bool deactivateCameraOnStart = false;
    [SerializeField] private bool followPlayer;
    
    [Tooltip("these gameObjects are activated / deactivated when player enters / leave the zone")]
    [SerializeField] private GameObject[] objectToActivate;
    
    [Header("RenderTexture Options")]
    [SerializeField] private RawImage _renderTexture;
    [Tooltip("The Color of the RenderTexture when player not in the zone")]
    [SerializeField] private Color _colorDeactivated;
    
    private void Start()
    {
        // if we want to deactivate camera associated at start 
        if (zoneCamera != null)
            zoneCamera.enabled = !deactivateCameraOnStart;
        
        // set this zone as "inactive" : player is not here
        // deactivate all object associated
        foreach (GameObject obj in objectToActivate)
            obj.SetActive(false);
        
        // set render texture associated to this zone as "inactive"
        ManageRenderTexturesLight(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CameraManager.Instance.SwitchTo(zoneCamera, followPlayer);
            LightZoneManager.Instance.OnEnterNewZone(this);
            ManagerObjectToActivate(true);
            ManageRenderTexturesLight(true);
        }
    }

    public void ManagerObjectToActivate(bool activate)
    {
        foreach (GameObject obj in objectToActivate)
            obj.SetActive(activate);
    }
    
    public void ManageRenderTexturesLight(bool activate)
    {
        if (_renderTexture == null) return;

        _renderTexture.DOColor(activate? Color.white : _colorDeactivated, 0.2f);
    }
}