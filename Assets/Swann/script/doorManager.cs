using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Button
{
    public GameObject button;
    public bool enabled;

    Button(GameObject button, bool enabled)
    {
        this.button = button.gameObject;
        this.enabled = enabled;
    }
}

public class doorManager : MonoBehaviour
{
    public List<Button> buttons;
    Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetBool("character_nearby", checkLogic());
    }

    bool checkLogic()
    {
        foreach (Button button in buttons)
        {
            if (button.button.transform.GetChild(1).gameObject.GetComponent<buttonVR>().getIsPressed() != button.enabled)
            {
                return false;
            }
        }
        return true;
    }
}
