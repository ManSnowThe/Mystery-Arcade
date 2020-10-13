using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fading : MonoBehaviour {

    public Texture2D fadeOutTexture; // текстура, которая будет перекрывать экран. Можно вставить экран загрузки
    public float fadeSpeed = 0.8f; // скорость затемнения

    private int drawDepth = -1000; // чем меньше значение, тем выше рендер в иерархии
    private float alpha = 1.0f; // используется для затемнения
    private int fadeDir = -1; // затемнение(-1) и появление(1)

    private void OnGUI()
    {
        // зайдествовать alpha значение для потухания, используя направление, скорость, и время
        alpha += fadeDir * fadeSpeed * Time.deltaTime;
        // зажать в значение между 0 и 1, т.к GUI.color использует alpha значения от 0 до 1
        alpha = Mathf.Clamp01(alpha);

        // поставить цвет на GUI (в данном случае на текстуру).
        GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, alpha); // устанавливает alpha значение
        GUI.depth = drawDepth; // ставит рендер черной текстуру на верх (прорисовывается последний)
        GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), fadeOutTexture); // текстура прорисовывается на весь экран
    }

    // Присваивается параметр для fadeDir, чтобы сцены затемнялось от значени -1 и 1
    public float BeginFade(int direction)
    {
        fadeDir = direction;
        return (fadeSpeed); // возвращает fadeSpeed, чтобы легко регулировать время SceneManager.LoadScene();
    }

    // Вызывается, когда уровень загрузится. Он может брать индекс уровня (int) как параметр, так чтобы можно было выставлять разный лимит затемнения на некоторых сценах
    private void OnLevelWasLoaded()
    {
        // alpha = 1; // использовать, если alpha не выставлен 1 по умолчанию
        BeginFade(-1); // вызывает функцию затемнения
    }
}
