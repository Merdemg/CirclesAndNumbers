using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class Avatar : MonoBehaviour
{
    int value = 0;
    [SerializeField] TextMeshProUGUI textBox;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<bubble>())
        {
            bubble b = collision.GetComponent<bubble>();
            value += b.getValue();
            UpdateUI();
            b.GetDestroyed();
        }

    }

    void UpdateUI()
    {
        textBox.text = value.ToString();
    }

    public int GetPoints()
    {
        return value;
    }
}
