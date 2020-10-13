// Этот класс позволяет использовать кнопки onClick с помощью клавиатуры
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonCont : MonoBehaviour
{
    public string inputName1; // имя клавиши
    // public string inputName2;

    // Определяем кнопки в Unity
    public Button buttonMe1; // кнопка: начать диалого
    public Button buttonMe2; // кнопка: продолжить диалог

    // public Image im;
    public DialogueTrigger dt; // экземпляр класса
    public Animator animMan; // аниматор для игрока

    public bool in1 = true; // нужны, чтобы отделить две кнопки при нажатии одной клавиши
    public bool in2 = false;

    private void Update()
    {
        //if (im.enabled == true)
        if (dt.ima.enabled == true) // если в зоне тригера
        {
            if (Input.GetKeyDown(inputName1) & in1 == true) 
            {
                animMan.SetFloat("Speed", -0.1f);
                animMan.SetFloat("vSpeed", -0.1f);
                animMan.SetBool("Ground", true);
                buttonMe1.onClick.Invoke(); // вызывает метод привязанный к кнопке
                StartCoroutine(TrueFalse()); // позволяет избежать бага
            }
            if (Input.GetKeyDown(inputName1) & in2 == true)
            {
                buttonMe2.onClick.Invoke();
            }
        }
    }
    IEnumerator TrueFalse() // меняет in1 и in2
    {
        in1 = false;
        yield return new WaitForSeconds(0.1f);
        in2 = true;
    }
}
