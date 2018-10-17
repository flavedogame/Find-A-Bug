using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleType : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    public float textSpeed;
    public float secsBetweenDialog;
    Coroutine currentCoroutine;
    // Start is called before the first frame update
    void Start()
    {
        Init();
        
    }

    public void Init()
    {
        if (currentCoroutine!=null) { 
        StopCoroutine(currentCoroutine);
        }
        currentCoroutine = StartCoroutine( Type());
    }

    IEnumerator Type()
    {

        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        int totalVisibleCharacters = textMeshPro.text.Length;
        int counter = 0;
        while (true)
        {
            int visibleCount = counter > totalVisibleCharacters ? totalVisibleCharacters : counter;
            textMeshPro.maxVisibleCharacters = visibleCount;
            if (visibleCount >= totalVisibleCharacters)
            {
                yield return new WaitForSeconds(secsBetweenDialog);
                //ready to next text
            }
            counter += 1;
            yield return new WaitForSeconds(textSpeed);
        }
    }

}
