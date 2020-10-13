// Класс, который реализует диалоговую системы (работает с DialogueBox)
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour {

    public Text nameText; // сюда будут записываться именая объектов (NPC)
    public Text dialogueText; // сюда будут записываться текст диалогов

    public Animator animator; // аниматор диалогового окна

    public ManController man; // нужно, чтобы блокировать движение игрока

    private Queue<string> sentences; // строит очередь предложений

    public ButtonCont bc; // меняет in1 и in2 в ButtonCont.cs

    public Rigidbody2D rig; // используется, чтобы зафризить игрока по X-координате

    private void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        // Debug.Log("Starting converstaion with " + dialogue.name);

        man.enabled = false; // блокирует движение игрока
        // man.rigc = RigidbodyConstraints2D.FreezePositionX;
        rig.constraints = RigidbodyConstraints2D.FreezePositionX; // блочит физику по X

        animator.SetBool("IsOpen", true); // анимация открытия диалогового окна

        nameText.text = dialogue.name; // дает имя объекту, с которым происходит диалог

        sentences.Clear(); // очищает окно от строк

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence); // делает очередь предложения
        }

        DisplayNextSentence(); // отображает следующее предложение
    }

    public void DisplayNextSentence() // метод для отображения следующего предложения
    {
        if(sentences.Count == 0) // если кончились предложения, то вызывается метод, заканчивающий диалог
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue(); // вытискаивает предложения из очереди
        //dialogueText.text = sentence;
        StopAllCoroutines(); // останавливает другие куратины, чтобы диалоги появлялись по очереди
        StartCoroutine(TypeSentence(sentence)); // вызов куратины
    }

    IEnumerator TypeSentence(string sentence) // куратина, которая печатает диалог по буквам
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue() // метод заканчивающий диалог
    {
        animator.SetBool("IsOpen", false); // анимация закрытия окна диалога
        // Debug.Log("End of conversation");

        man.enabled = true; // движение вновь разрешено

        bc.in1 = true; // для ButtonCont.cs
        bc.in2 = false;

        rig.constraints = RigidbodyConstraints2D.FreezeRotation; // замораживает движение по Z, но размораживает по X
    }
}
