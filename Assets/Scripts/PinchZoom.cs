using UnityEngine;

public class PinchZoom : MonoBehaviour
{
    public float perspectiveZoomSpeed = 0.5f;        // The rate of change of the field of view in perspective mode.
    public float orthoZoomSpeed = 0.5f;     // The rate of change of the orthographic size in orthographic mode. 
    public float speedMove = 2; 
    private new Camera camera;
    private float timer;
    public float timeMaxToStartMoving = 1; 
    void Awake()
    {
        this.camera = this.GetComponent<Camera>();
    }

    void Update()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // If there are two touches on the device...
        if (Input.touchCount == 2)
        {
            timer = 0; 
            // Store both touches.
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            // Find the position in the previous frame of each touch.
            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            // Find the magnitude of the vector (the distance) between the touches in each frame.
            float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

            // Find the difference in the distances between each frame.
            float deltaMagnitudeDiff = prevTouchDeltaMag - touchDeltaMag;

            // If the camera is orthographic...
            if (camera.orthographic)
            {
                // ... change the orthographic size based on the change in distance between the touches.
                camera.orthographicSize += deltaMagnitudeDiff * orthoZoomSpeed;

                // Make sure the orthographic size never drops below zero.
                camera.orthographicSize = Mathf.Max(camera.orthographicSize, 0.1f);
            }
            else
            {
                // Otherwise change the field of view based on the change in distance between the touches.
                 camera.fieldOfView += deltaMagnitudeDiff * perspectiveZoomSpeed;
                //this.transform.position += Vector3.up * deltaMagnitudeDiff * perspectiveZoomSpeed;
                // Clamp the field of view to make sure it's between 0 and 180.
                camera.fieldOfView = Mathf.Clamp(camera.fieldOfView, 0.1f, 179.9f);
            }
        }

        else if (Input.touchCount == 1)
        {
            timer += Time.deltaTime;
            if (timer > timeMaxToStartMoving)
            {
                if (Input.GetTouch(0).position.x > (Screen.width - Screen.width * 0.1f))
                {
                    this.transform.position += Vector3.right * speedMove * Time.deltaTime;
                }
                else if (Input.GetTouch(0).position.x < (0 + Screen.width * 0.1f))
                {
                    this.transform.position -= Vector3.right * speedMove * Time.deltaTime;
                }
                if (Input.GetTouch(0).position.y > (Screen.height - Screen.height * 0.2f))
                {
                    this.transform.position += Vector3.forward * speedMove * Time.deltaTime;
                }
                else if (Input.GetTouch(0).position.y < (0 + Screen.height * 0.2f))
                {
                    this.transform.position -= Vector3.forward * speedMove * Time.deltaTime;
                }
               
            }
            
        }
        else if (Input.touchCount == 0)
        {
            timer = 0; 
        }

#endif


#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
        {
            timer += Time.deltaTime;
            if (timer > timeMaxToStartMoving)
            {
                if (Input.mousePosition.x > (Screen.width - Screen.width * 0.2f))
                {
                    this.transform.position += Vector3.right * speedMove * Time.deltaTime;
                }
                else if (Input.mousePosition.x < (0 + Screen.width * 0.2f))
                {
                    this.transform.position -= Vector3.right * speedMove * Time.deltaTime;
                }
                if (Input.mousePosition.y > (Screen.height - Screen.height * 0.2f))
                {
                    this.transform.position += Vector3.forward * speedMove * Time.deltaTime;
                }
                else if (Input.mousePosition.y < (0 + Screen.height * 0.2f))
                {
                    this.transform.position -= Vector3.forward * speedMove * Time.deltaTime;
                }
                
            }
        }
        else
        {
            timer = 0; 
        }
#endif

    }
}