// Класс, который привязан к триггеру, чтобы диалог активировался
// именно в этой области
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogueTrigger : MonoBehaviour {

    public Dialogue dialogue; // экземпляр класса с массивом строк (для заполнения диалогов в Unity)
    public GameObject im; // нужен, чтобы манимупилорвать с DialogueBox
    public Image ima; // сделал, чтобы диалог начинался ТОЛЬКО в зоне триггера (использовал в ButtonCont.cs)
    public ManController man; // определяет игрока

    public DialogueSystem ds; // экземпляр класса, использовал, чтобы починить баг при уходе из триггер-зоны

    public GameObject indicator; // индикатор, чтобы видеть место диалога
    private void Start()
    {
        ima.enabled = false; // не в триггере (для ButtonCont.cs)

        im.SetActive(false); // диалогове окно вне триггера неактивно

        indicator.SetActive(false); // при запуске индикатор не активен
    }
    public void TriggerDialogue() // метод привязан к кнопке диалога - начинает диалог
    {
        FindObjectOfType<DialogueSystem>().StartDialogue(dialogue);
    }
    private void OnTriggerStay2D(Collider2D collision) // выполняет действия, если в триггере
    {
        if (collision.CompareTag("Player")) // сравнивает, стоит ли этот объект в триггере
        {
            ima.enabled = true; // в триггере (для ButtonCont.cs)

            im.SetActive(true); // диалоговое окно в триггере активно

            indicator.SetActive(true); // активирует индикатор

            /*if (Input.GetKeyDown(KeyCode.P))
            {
                TriggerDialogue();
            }*/
            /*// Удалить
            if (Input.GetKeyDown(KeyCode.C))
            {
                Cont.onClick.Invoke();
                // TriggerContDialogue();
            }*/
        }
    }
    private void OnTriggerExit2D(Collider2D collision) // выполняет действия при выходе из триггера
    {
        if (collision.CompareTag("Player")) // смотрит: тот ли это объект
        {
            ima.enabled = false; // вне триггера (для ButtonCont.cs)
            man.enabled = true; // позволяет игроку двигаться в случае выхода из триггера

            im.SetActive(false); // диалогове окно вне триггера неактивно
            ds.EndDialogue(); // заканчивает диалог при выходе из триггера

            indicator.SetActive(false); // индикатор вне триггера неактивен
        }
    }
}
