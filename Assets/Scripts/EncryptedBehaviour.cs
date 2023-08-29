using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;
using System.Numerics;

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
        BigInteger p = genPrime(randomNum());
        BigInteger q = p;
        while(p == q) {
            q = genPrime(randomNum());
        }
        BigInteger N = calcN(p, q);
        int r1 = randomNum(1 , 100000);
        int r2 = r1;
        while(r1 == r2) {
            r2 = randomNum(1, 100000);
        }
        BigInteger e = calcE(p, q, r1 % r2);
        BigInteger d = calcD(e, calcA(p, q));
    
        BigInteger enc = encMsg(e, N, klar);
        BigInteger dec = decMsg(d, N, enc);
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

    public int randomNum(int start, int end) {
        System.Random random = new System.Random();
        return random.Next(start, end);
    }

    public int randomNum() {
        System.Random random = new System.Random();
        return random.Next();
    }

    public bool isPrime(BigInteger n) {
        if(n == 2 || n == 3) return true;
        if(n <= 1 || n % 2 == 0 || n % 3 == 0) return false;
        for(BigInteger i = 5; i * i <= n; i+=6) {
            if(n % i == 0 || n % (i+2) == 0) return false;
        }
        return true;
    }

    public BigInteger genPrime(BigInteger start) {
        for(BigInteger i = 0; i < 1; start++) {
            if(isPrime(start)) {
                i++;
            }
        }
        return start - 1;
    }

    public BigInteger calcN(BigInteger p, BigInteger q) {
        BigInteger n = p * q;
        return n;
    }

    public BigInteger calcE(BigInteger p, BigInteger q, BigInteger e) {
        BigInteger a = calcA(p, q);
        while(BigInteger.GreatestCommonDivisor(e, a) != 1) {
            e++;
        }
        return e;
    }

    public BigInteger calcD(BigInteger e, BigInteger a) {
        int k = 1;
        while((1+(k*a)) % e != 0) {
            k++;
        }
        BigInteger d = (1+(k*a)) / e;
        return d;
    }

    public BigInteger calcA(BigInteger p, BigInteger q) {
        BigInteger a = (p-1)*(q-1);
        return a;
    }

    public BigInteger encMsg(BigInteger e, BigInteger N, BigInteger klar) {
        return BigInteger.ModPow(klar, e, N);
    }

    public BigInteger decMsg(BigInteger d, BigInteger N, BigInteger geheim) {
        return BigInteger.ModPow(geheim, d, N);
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