using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public float speed;
    public Vector3 offset;
    public float cameraRotationPower;
    public float cameraFOVmultiplayer;
    public float cameraFovScale = 0;
    Quaternion cameraStartRotation;
    Vector3 cameraBonusRotation;
    Vector3 targetLookPosition;
    private void Awake()
    {
        cameraStartRotation = transform.localRotation;
        transform.position = Manager.PlayerPosition + offset;
    }
    void Update()
    {

        Vector3 allAxisPosition = Vector3.Lerp(transform.position, Manager.PlayerPosition + offset, speed * Time.deltaTime / Time.timeScale);
        Vector3 playerPower = Manager.Player.GetComponent<Player>().movement.playerPower;
        targetLookPosition = Manager.PlayerPosition + (playerPower * cameraRotationPower);
        //cameraBonusRotation = new Vector3(playerPower.z, 0, 0);
        transform.position = allAxisPosition;
        //transform.localRotation = Quaternion.Euler(cameraStartRotation.eulerAngles + (cameraBonusRotation * cameraRotationPower));
        //transform.LookAt(targetLookPosition);
        Camera.main.fieldOfView = 60 + (cameraFOVmultiplayer * cameraFovScale);
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(targetLookPosition, 1f);
    }
}
