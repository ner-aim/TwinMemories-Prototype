using UnityEngine;

public class Interactable : MonoBehaviour
{
    // Start is called before the first frame update
    public float radius = 3f;
    public Transform _transform;
    bool isFocus = false;
    Transform player;
    bool hasInteracted = false;
    public virtual void Interact()
    {
        //to be overwritten
        //Debug.Log("Interacting with " + _transform.name);
    }

    public void onFocused(Transform playerTransform)
    {
        isFocus = true;
        player = playerTransform;
        hasInteracted = false;
    }

    public void Defocused()
    {
        isFocus = false;
        player = null;
        hasInteracted = false;
    }
    private void Update()
    {
        if (isFocus && !hasInteracted)
        {
            float distance = Vector3.Distance(player.position, _transform.position);
            if (distance <= radius)
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        if (_transform == null)
        {
            _transform = transform;
        }
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_transform.position, radius);

    }

}
