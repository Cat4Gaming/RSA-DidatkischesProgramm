using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Numerics;
using System.Text;

public class SimulationSceneBehaviour : MonoBehaviour {
    private BigInteger[] msgToSend;
    private bool moveLetter, eve;
    [SerializeField] private bool toBob, unenc;
    [SerializeField] private TMP_Text recieveText;
    [SerializeField] private TMP_Text textToEve;
    [SerializeField] private Transform startPos, endPos, evePos, midPos;
    [SerializeField] private GameObject startLetter, eveLetter;
    [SerializeField] private Encryption enc;
    private Encoding unicode = Encoding.Unicode;
    
    public void switchEve() {
        eve = !eve;
    }

    public void sendMsg(string msg) {
        msgToSend = enc.encryptMsg(msg);
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
                if(eve && unenc) {
                    if(toBob) textToEve.text = "Alice: " + enc.decryptMsg(msgToSend);
                    else textToEve.text = "Bob: " + enc.decryptMsg(msgToSend);
                } else if(eve && unenc == false) {
                    string tmp = String.Join("", msgToSend);
                    char[] tmpC = tmp.ToCharArray();
                    byte[] txt = new byte[tmpC.Length];
                    for(int i = 0; i < tmpC.Length-2; i = i + 2) {
                        string tpm = tmpC[i] + "" + tmpC[i+1];
                        txt[i/2] = Convert.ToByte(Int32.Parse(tpm));
                    }
                    if(toBob) textToEve.text = "Alice: " + Encoding.Unicode.GetString(txt);
                    else textToEve.text = "Bob: " + Encoding.Unicode.GetString(txt);
                }
                startLetter.transform.position = startPos.position;
                eveLetter.transform.position = midPos.position;
            }
        }
    }
}