using UnityEngine;
using UnityEngine.UI;
public class CounterScript : MonoBehaviour
{
    public static CounterScript instance;
    [SerializeField] private Text counterText;
    private int strawberryCount = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        UpdateCounterUI(strawberryCount);
    }

    public void Inc()
    {
        strawberryCount += 1;
        UpdateCounterUI(strawberryCount);
    }

        public void Dec()
    {
        Debug.Log("Decreasing by 1");
        strawberryCount = strawberryCount - 1;
        UpdateCounterUI(strawberryCount);
    }

    public int getCount(){
        return strawberryCount;
    }

    public void ResetCounter()
    {
        strawberryCount = 0;
        UpdateCounterUI(strawberryCount);
    }

    private void UpdateCounterUI(int c)
    {
        counterText.text = $"Strawberries collected: {c}";
    }
}
