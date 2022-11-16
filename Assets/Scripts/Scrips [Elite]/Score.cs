using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{
    public static Score Instance;
    private float amount;
    private TextMeshProUGUI textMesh;

    private void Awake()
    {
        if (Score.Instance == null)
        {
            Score.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
       
        textMesh.text = amount.ToString("0");
    }

    public void PlusScore(float scoreEntry)
    {
        amount += scoreEntry; 
    }
}
