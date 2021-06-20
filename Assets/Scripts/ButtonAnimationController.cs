using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonAnimationController : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    Animator animator;

    void OnEnable()
    {
        animator = GetComponent<Animator>();
        if (animator) animator.SetBool("Selected", false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        animator.SetTrigger("Pressed");
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        animator.SetBool("Selected", true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        animator.SetBool("Selected", false);
    }
}
