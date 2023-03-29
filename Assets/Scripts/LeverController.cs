using UnityEngine;

public class LeverController : MonoBehaviour
{
    enum Side
    {
        LEFT,
        RIGHT
    }

    [SerializeField] private float degreePerSecond = 0f;
    [SerializeField] private float kickAngle = 0f;
    [SerializeField] private GameObject pinball;
    [SerializeField] private Side side;

    enum KickState
    {
        IDLE,
        KICKING,
        RELEASING,
    }

    KickState kick;
    float idleAngle;
    Quaternion targetRotationKick;
    Quaternion targetRotationIdle;
    Quaternion targetRotation;
    Rigidbody body;
    int button;

    private void Start()
    {
        idleAngle = transform.rotation.eulerAngles.z;
        targetRotationKick = Quaternion.Euler(pinball.transform.rotation.eulerAngles.x, 0f, kickAngle);
        targetRotationIdle = Quaternion.Euler(pinball.transform.rotation.eulerAngles.x, 0f, idleAngle);
        body = GetComponent<Rigidbody>();
        kick = KickState.IDLE;

        if (side == Side.LEFT)
        {
            button = 0;
        } else
        {
            button = 1;
        }
    }

    private bool AreaTouched(Touch touch)
    {
        return (touch.position.x < Screen.width / 2 && side == Side.LEFT) || (touch.position.x > Screen.width / 2 && side == Side.RIGHT);
    }

    public void Update()
    {
        switch(kick)
        {
            case KickState.IDLE:
                {

#if UNITY_ANDROID
                    // Controll con touch de pantalla
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (AreaTouched(touch))
                        {
                            if (touch.phase == TouchPhase.Began)
                            {
                                kick = KickState.KICKING;
                                targetRotation = targetRotationKick;
                            }
                        }
                    }
#endif
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
                    // Control con click de mouse
                    if (Input.GetMouseButtonDown(button))
                    {
                        kick = KickState.KICKING;
                        targetRotation = targetRotationKick;
                    }

                    // Control con teclado
                    if ((Input.GetKeyDown(KeyCode.LeftArrow) && side == Side.LEFT) || (Input.GetKeyDown(KeyCode.RightArrow) && side == Side.RIGHT))
                    {
                        kick = KickState.KICKING;
                        targetRotation = targetRotationKick;
                    }
#endif
                }
                break;
            case KickState.KICKING:
                {
#if UNITY_ANDROID
                    // Controll con touch de pantalla
                    if (Input.touchCount > 0)
                    {
                        Touch touch = Input.GetTouch(0);

                        if (AreaTouched(touch))
                        {
                            if (touch.phase == TouchPhase.Ended)
                            {
                                kick = KickState.RELEASING;
                                targetRotation = targetRotationIdle;
                            }
                        }
                    }
#endif
#if UNITY_STANDALONE_WIN || UNITY_EDITOR
                    // Control con click de mouse
                    if (Input.GetMouseButtonUp(button))
                    {
                        kick = KickState.RELEASING;
                        targetRotation = targetRotationIdle;
                    }

                    // Control con teclado
                    if ((Input.GetKeyUp(KeyCode.LeftArrow) && side == Side.LEFT) || (Input.GetKeyUp(KeyCode.RightArrow) && side == Side.RIGHT))
                    {
                        kick = KickState.RELEASING;
                        targetRotation = targetRotationIdle;
                    }
#endif
                }
                break;
            case KickState.RELEASING:
                {
                    if (transform.rotation == targetRotationIdle)
                    {
                        kick = KickState.IDLE;
                    }
                }
                break;
        }
    }

    private void FixedUpdate()
    {
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, degreePerSecond * Time.deltaTime).normalized;

        body.MoveRotation(newRotation);
    }
}
