using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    [SerializeField] private InputField width;
    [SerializeField] private InputField height;
    [SerializeField] private InputField colors;
    public void submit()
    {
        
    }
    public void _width()
    {
        if (width.text == "")
        {
            
            Debug.Log("������� ����� �� 10 �� 50");
        }
        else
        {
            int sizeX = int.Parse(width.text);
            
        }

    }
    public void _height()
    {
        if (height.text == "")
        {

            Debug.Log("������� ����� �� 10 �� 50");
        }
        else
        {
            int sizeY = int.Parse(height.text);
        }

    }
    public void _colors()
    {
        if (width.text == "")
        {
            Debug.Log("������� ����� �� 2 �� 5");
        }
        else
        {
            int NumColors = int.Parse(width.text);
        }
    }
}
