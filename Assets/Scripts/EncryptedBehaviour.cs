using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class EncryptedBehaviour : MonoBehaviour {
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
        msgToSend = encryptMsg(msg);
        moveLetter = true;
    }

    void Start() {
        encryptMsg("5");
    }

    public string encryptMsg(string msg) {
        int klar = Int32.Parse(msg);
        double p = genPrime(randomNum());
        double q = p;
        while (p == q) {
            q = genPrime(randomNum());
        }
        double N = calcN(p, q);
        double e = calcE(p, q, 2);
        double d = calcD(e, calcA(p, q));
    
        double enc = encMsg(e, N, klar);
        double dec = decMsg(d, N, enc);
        //
        Debug.Log("p: " + p);
        Debug.Log("q: " + q);
        Debug.Log("N: " + N);
        Debug.Log("a: " + calcA(p, q));
        Debug.Log("e: " + e);
        Debug.Log("d: " + d);
        Debug.Log("klar: " + klar);
        Debug.Log("enc: " + enc);
        Debug.Log("dec: " + dec);
        //

        return msg;
    }

    public int randomNum() {
        System.Random random = new System.Random();
        return random.Next(1, 100);
    }

    public bool isPrime(double n) {
        if(n == 2 || n == 3) return true;
        if(n <= 1 || n % 2 == 0 || n % 3 == 0) return false;
        for(double i = 5; i * i <= n; i+=6) {
            if(n % i == 0 || n % (i+2) == 0) return false;
        }
        return true;
    }

    public double genPrime(double start) {
        for(double i = 0; i < 1; start++) {
            if(isPrime(start)) {
                i++;
            }
        }
        return start - 1;
    }

    public double calcN(double p, double q) {
        double n = p * q;
        return n;
    }

    public double ggT(double a, double b) {
        if(a == 0) {
            return b;
        }
        return ggT(b % a, a);
    }

    public double phi(double n) {
        double result = 1;
        for(double i = 2; i < n; i++) {
            if(ggT(i, n) == 1) {
                result++;
            }
        }
        return result;
    }

    public double calcE(double p, double q, double e) {
        double a = calcA(p, q);
        while(ggT(e, a) != 1) {
            e++;
        }
        return e;
    }

    public double calcD(double e, double a) {
        int k = 1;
        while((1+(k*a)) % e != 0) {
            k++;
        }
        double d = (1+(k*a)) / e;
        return d;
    }

    public double calcA(double p, double q) {
        double a = (p-1)*(q-1);
        return a;
    }

    public double encMsg(double e, double N, double klar) {
        return (Math.Pow(klar, e) % N);
    }

    public double decMsg(double d, double N, double geheim) {
        return  (Math.Pow(geheim, d) % N);
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