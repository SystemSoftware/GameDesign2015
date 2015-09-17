using UnityEngine;
using System.Collections;

public class T4E29_Controller : Controller {
    protected override void OnAssignCameraAndControl() {
        cameraIdealDistance = 30;
        float w = ctrlAttachedCamera.rect.width / 4,
        h = ctrlAttachedCamera.rect.height / 4;
        Rect pos = new Rect(ctrlAttachedCamera.rect.center.x - w / 2, ctrlAttachedCamera.rect.min.y, w, h);
        if (this.GetComponentInChildren<Camera>() != null) {
            this.GetComponentInChildren<Camera>().rect = pos;
            this.GetComponentInChildren<Camera>().enabled = ctrlAttachedCamera.enabled;
        }

    }

    new void LateUpdate() {
        if (ctrlAttachedCamera != null) {
            Vector3 idealLocation =
                transform.position - transform.forward * cameraIdealDistance + transform.up * cameraIdealYOffset;
            Vector3 deltaToIdeal = idealLocation - ctrlAttachedCamera.transform.position;
            ctrlAttachedCamera.transform.transform.position += deltaToIdeal * (1.0f - Mathf.Pow(0.02f, Time.deltaTime));
            ctrlAttachedCamera.transform.rotation = Quaternion.Lerp(ctrlAttachedCamera.transform.rotation, transform.rotation, (1.0f - Mathf.Pow(0.02f, Time.deltaTime)));
            ;

        }
    }
}
