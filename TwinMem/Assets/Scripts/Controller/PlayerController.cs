using UnityEngine.EventSystems;
using UnityEngine;

[RequireComponent(typeof(PlayerMotor))]
public class PlayerController : MonoBehaviour
{
    public Interactable focus;
    public LayerMask movementMask;
    Camera cam;
    PlayerMotor motor;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        motor = GetComponent<PlayerMotor>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }
        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100, movementMask))
            {
                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);

                //Move player to what has been hit
                motor.moveToPoint(hit.point);

                //Stop focusing any objects
                RemoveFocus();
            }
        }

        if (Input.GetMouseButton(0))
        {
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 100))
            {
                //Check interactable and found
                // set it as focus
                //Debug.Log("We hit " + hit.collider.name + " " + hit.point);
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable!=null)
                {
                    SetFocus(interactable);
                }

            }
        }

    }
    void SetFocus(Interactable setFocus)
    {
        if (setFocus != focus)
        {
            if (focus != null)
            {
                focus.Defocused();
            }
            focus = setFocus;
            motor.followTarget(setFocus);

        }
        setFocus.onFocused(transform);

        //motor.faceTarget();
        
    }
    void RemoveFocus()
    {
        if (focus != null)
        {
            focus.Defocused();
        }
        focus = null;
        motor.StopFollowingTarget();
    }
}
