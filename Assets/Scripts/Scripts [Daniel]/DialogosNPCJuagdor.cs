using System.Collections;
using UnityEngine;
using TMPro;

public class DialogosNPCJuagdor : MonoBehaviour
{

    [SerializeField] private GameObject Exclamation;
    [SerializeField] private GameObject dialogosPanel;
    [SerializeField] private TMP_Text dialogosTexto;
    [SerializeField, TextArea(4,7)] private string[] lineasDialogo;

    private float velocidadTexto = 0.05f;

    private bool elPlayerEstaEnRango;
    private bool elDialogoComenzo;
    private int lineIndex;

 
    void Update()
    {
        if (elPlayerEstaEnRango && Input.GetButtonDown("Player1_Interact")) //la letra "e" es la que es para interactuar
        {
            if (!elDialogoComenzo)
            {
                IniciarDialogo();
            } 
            else if (dialogosTexto.text == lineasDialogo[lineIndex])
            {
                siguienteLineaTexto();
            }
        }
    }

    private void IniciarDialogo()
    {
        elDialogoComenzo = true;
        dialogosPanel.SetActive(true);
        Exclamation.SetActive(false);
        lineIndex = 0;
        StartCoroutine(ShowLine());
    }

    private void siguienteLineaTexto()
    {
        lineIndex++;
        if (lineIndex < lineasDialogo.Length)
        {
            StartCoroutine(ShowLine());
        }
        else
        {
            elDialogoComenzo = false;
            dialogosPanel.SetActive(false);
            Exclamation.SetActive(true);
        }
    }
     
    private IEnumerator ShowLine()
    {
        dialogosTexto.text = string.Empty;

        foreach(char ch in lineasDialogo[lineIndex])
        {
            dialogosTexto.text += ch;
            yield return new WaitForSeconds(velocidadTexto);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            elPlayerEstaEnRango = true;
            Exclamation.SetActive(true);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            elPlayerEstaEnRango = false;
            Exclamation.SetActive(false);
        }
    }
}
