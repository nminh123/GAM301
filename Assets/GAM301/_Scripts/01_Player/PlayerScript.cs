using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum PlayerState
    {
        Outside,
        Driving,
        GoToTrunk
    }

    [SerializeField] public PlayerState state;
    [SerializeField] private Transform carTransform;
    [SerializeField] private CapsuleCollider collision;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private SkinnedMeshRenderer render;
    [SerializeField] private bool isDriving = false;
    // [SerializeField] private Transform spoilerTransform;
    [SerializeField] private PlayerState previousState;

    void Start()
    {
        state = PlayerState.Outside;
        previousState = state;
    }

    void Update()
    {
        SwitchStateByTappingKey();
        HandleStateChange();
    }

    void SwitchStateByTappingKey()
    {
        if (Input.GetKeyDown(KeyCode.F) && isDriving == false)
        {
            state = PlayerState.Driving;
            isDriving = true;
        }
        else if (Input.GetKeyDown(KeyCode.F) && isDriving == true)
        {
            state = PlayerState.Outside;
            isDriving = false;
        }
    }

    void HandleStateChange()
    {
        if (state != previousState)
        {
            if (state == PlayerState.Driving)
            {
                render.enabled = false;
                collision.enabled = false;

                this.transform.SetParent(carTransform);

                rb.isKinematic = true;
                this.transform.position = carTransform.position;
            }
            else if (state == PlayerState.Outside)
            {
                render.enabled = true;
                collision.enabled = true;

                this.transform.SetParent(null);

                rb.isKinematic = false;
                this.transform.position = new Vector3(carTransform.position.x + 1.5f, carTransform.position.y, carTransform.position.z);
            }

            previousState = state;
        }
    }

    public float GetCarDistance()
    {
        return Vector3.Distance(this.transform.position, carTransform.position);
    }

    public bool IsDriving()
    {
        return isDriving;
    }
}