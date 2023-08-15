using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnencryptedSceneBehaviour : MonoBehaviour {
    private string msgToSend;
    [SerializeField] private bool goRight;
    [SerializeField] private TMP_Text recieveText;
    [SerializeField] private bool eve;
    [SerializeField] private TMP_Text textToEve;

    public void switchEve() {
        eve = !eve;
    }

    public void sendMsg(string msg) {
        msgToSend = msg;
        recieveText.text = msg;
        if(eve) {
            if(goRight) textToEve.text = "Alice: " + msg;
            else textToEve.text = "Bob: " + msg;
        }
    }
}