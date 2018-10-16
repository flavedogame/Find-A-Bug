using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleType : MonoBehaviour
{
    TextMeshProUGUI textMeshPro;
    public float textSpeed;
    public float secsBetweenDialog;
    // Start is called before the first frame update
    IEnumerator Start()
    {
        textMeshPro = gameObject.GetComponent<TextMeshProUGUI>();
        int totalVisibleCharacters = textMeshPro.textInfo.characterCount;
        int counter = 0;
        while (true)
        {
            int visibleCount = counter>totalVisibleCharacters?totalVisibleCharacters:counter;
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
