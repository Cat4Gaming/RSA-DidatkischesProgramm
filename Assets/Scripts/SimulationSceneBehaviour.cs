using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Numerics;

public class SimulationSceneBehaviour : MonoBehaviour {
    private string msgToSend;
    private bool moveLetter, eve;
    [SerializeField] private bool toBob;
    [SerializeField] private TMP_Text recieveText;
    [SerializeField] private TMP_Text textToEve;
    [SerializeField] private Transform startPos, endPos, evePos, midPos;
    [SerializeField] private GameObject startLetter, eveLetter;
    [SerializeField] private Encryption enc;
    
    public void switchEve() {
        eve = !eve;
    }

    public void sendMsg(string msg) {
        msgToSend = "" + enc.encryptMsg(msg);
        moveLetter = true;
    }

    void Update() {
        if(moveLetter) {
            startLetter.transform.position = UnityEngine.Vector3.MoveTowards(startLetter.transform.position, endPos.position, 2);
            if(toBob){
                if(eve && startLetter.transform.position.x >= eveLetter.transform.position.x) {
                    eveLetter.transform.position = UnityEngine.Vector3.MoveTowards(eveLetter.transform.position, evePos.position, 2);
                }
            } else {
                if(eve && startLetter.transform.position.x <= eveLetter.transform.position.x) {
                    eveLetter.transform.position = UnityEngine.Vector3.MoveTowards(eveLetter.transform.position, evePos.position, 2);
                }
            }
            if(startLetter.transform.position == endPos.position) {
                moveLetter = false;
                recieveText.text = enc.decryptMsg(msgToSend);
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