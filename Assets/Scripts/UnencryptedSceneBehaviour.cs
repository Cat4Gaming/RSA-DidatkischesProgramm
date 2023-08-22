using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UnencryptedSceneBehaviour : MonoBehaviour {
    private string msgToSend;
    private bool moveLetter, eve;
    [SerializeField] private bool toBob;
    [SerializeField] private TMP_Text recieveText;
    [SerializeField] private TMP_Text textToEve;
    [SerializeField] private Transform startPos, endPos, evePos, midPos;
    [SerializeField] private GameObject startLetter, eveLetter;


    public void switchEve() {
        eve = !eve;
    }

    public void sendMsg(string msg) {
        msgToSend = msg;
        moveLetter = true;
    }

    void Update() {
        if(moveLetter) {
            startLetter.transform.position = Vector3.MoveTowards(startLetter.transform.position, endPos.position, 2);
            if(toBob){
                if(eve && startLetter.transform.position.x >= eveLetter.transform.position.x) {
                    eveLetter.transform.position = Vector3.MoveTowards(eveLetter.transform.position, evePos.position, 2);
                }
            } else {
                if(eve && startLetter.transform.position.x <= eveLetter.transform.position.x) {
                    eveLetter.transform.position = Vector3.MoveTowards(eveLetter.transform.position, evePos.position, 2);
                }
            }
            if(startLetter.transform.position == endPos.position) {
                moveLetter = false;
                recieveText.text = msgToSend;
                if(eve) {
                    if(toBob) textToEve.text = "Alice: " + msgToSend;
                    else textToEve.text = "Bob: " + msgToSend;
                }
                startLetter.transform.position = startPos.position;
                eveLetter.transform.position = midPos.position;
            }
        }
    }
}