using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    [SerializeField] private ServingsIndicator indicatorPrefab;

    private void Awake()
    {
        Instance = this;
    }

    public ServingsIndicator CreateServingsIndicator(Transform target)
    {
        ServingsIndicator indicator = Instantiate(indicatorPrefab, target.transform.position, target.transform.rotation);
        indicator.gameObject.SetActive(false);
        indicator.SetTarget(target);
        indicator.gameObject.SetActive(true);
        return indicator;
    }
}
