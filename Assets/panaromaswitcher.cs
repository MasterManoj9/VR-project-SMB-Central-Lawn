using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR;

public class PanoramaSwitcher : MonoBehaviour
{
    public Material[] panoramas;
    private int current = 0;
    private bool stickReady = true;
    private Camera viewCamera;
    private GameObject nextButton;
    private GameObject backButton;
    private GameObject gazedButton;
    private float gazeTime;

    void Start()
    {
        Show(0);
        viewCamera = Camera.main;
        if (viewCamera != null)
        {
            backButton = MakeButton("BACK", -1.3f);
            nextButton = MakeButton("NEXT", 1.3f);
        }
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.dKey.wasPressedThisFrame ||
                Keyboard.current.rightArrowKey.wasPressedThisFrame)
                Show(current + 1);

            if (Keyboard.current.aKey.wasPressedThisFrame ||
                Keyboard.current.leftArrowKey.wasPressedThisFrame)
                Show(current - 1);
        }

        var controller = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
        if (controller.TryGetFeatureValue(UnityEngine.XR.CommonUsages.primary2DAxis, out Vector2 stick))
        {
            if (Mathf.Abs(stick.x) < 0.3f) stickReady = true;
            else if (stickReady && Mathf.Abs(stick.x) > 0.7f)
            {
                Show(current + (stick.x > 0 ? 1 : -1));
                stickReady = false;
            }
        }

        if (viewCamera == null) return;
        GameObject hitButton = null;
        if (Physics.Raycast(viewCamera.transform.position, viewCamera.transform.forward,
            out RaycastHit hit, 5f))
        {
            if (hit.collider.gameObject == nextButton || hit.collider.gameObject == backButton)
                hitButton = hit.collider.gameObject;
        }

        if (hitButton != gazedButton) { gazedButton = hitButton; gazeTime = 0f; }
        if (gazedButton != null)
        {
            gazeTime += Time.deltaTime;
            if (gazeTime >= 1.5f)
            {
                Show(current + (gazedButton == nextButton ? 1 : -1));
                gazeTime = 0f;
            }
        }

        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            Ray clickRay = viewCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
            if (Physics.Raycast(clickRay, out RaycastHit clickHit, 5f))
            {
                if (clickHit.collider.gameObject == nextButton) Show(current + 1);
                if (clickHit.collider.gameObject == backButton) Show(current - 1);
            }
        }
    }

    GameObject MakeButton(string label, float horizontalOffset)
    {
        Transform eye = viewCamera.transform;
        GameObject button = GameObject.CreatePrimitive(PrimitiveType.Cube);
        button.name = label + " panorama button";
        button.transform.position = eye.position + eye.forward * 2f +
            eye.right * horizontalOffset - eye.up * 0.2f;
        button.transform.rotation = eye.rotation;
        button.transform.localScale = new Vector3(0.45f, 0.18f, 0.04f);
        Shader shader = Shader.Find("Universal Render Pipeline/Unlit");
        if (shader == null) shader = Shader.Find("Unlit/Color");
        Material material = new Material(shader);
        material.color = Color.white;
        button.GetComponent<Renderer>().material = material;

        GameObject title = new GameObject(label + " text");
        title.transform.position = button.transform.position - eye.forward * 0.03f;
        title.transform.rotation = eye.rotation;
        TextMesh text = title.AddComponent<TextMesh>();
        text.text = label;
        text.fontSize = 48;
        text.characterSize = 0.025f;
        text.anchor = TextAnchor.MiddleCenter;
        text.alignment = TextAlignment.Center;
        text.color = Color.black;
        return button;
    }

    void Show(int index)
    {
        if (panoramas == null || panoramas.Length == 0) return;
        current = (index + panoramas.Length) % panoramas.Length;
        RenderSettings.skybox = panoramas[current];
        DynamicGI.UpdateEnvironment();
    }
}
