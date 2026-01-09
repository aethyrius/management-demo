using TMPro;
using UnityEngine;

public class ServingsIndicator : MonoBehaviour
{
    private TMP_Text text;
    public Transform target;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetValue(int servings)
    {
        text.text = servings.ToString();
        gameObject.SetActive(servings >= 1);
    }

    private void Update()
    {
        transform.position = new Vector3(
            target.transform.position.x,
            target.transform.position.y + 0.5f);
    }
}