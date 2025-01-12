using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public enum PlayerState
    {
        Outside,
        Driving,
        GoToTrunk
    }

    [SerializeField] PlayerState state;
    [SerializeField] Transform carPosition;
    [SerializeField] CapsuleCollider collision;
    [SerializeField] SkinnedMeshRenderer render;
    [SerializeField] bool isDriving = false;

    private PlayerState previousState; // Biến lưu trạng thái trước đó

    void Start()
    {
        state = PlayerState.Outside;
        previousState = state; // Khởi tạo trạng thái trước đó
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
            }
            else if (state == PlayerState.Outside)
            {
                render.enabled = true;
                collision.enabled = true;
                this.transform.position = 
                                        new Vector3(    carPosition.position.x + 1.5f, 
                                                        carPosition.position.y, 
                                                        carPosition.position.z);
            }

            previousState = state;
        }
    }

    public float GetCarDistance()
    {
        return Vector3.Distance(this.transform.position, carPosition.position);
    }

    public bool IsDriving()
    {
        return isDriving;
    }

    public PlayerState GetState()
    {
        return state;
    }
}
