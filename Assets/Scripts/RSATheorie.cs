using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Numerics;
using TMPro;

public class RSATheorie : MonoBehaviour {
    [SerializeField] private RSA rsa;
    private BigInteger p, q, N, e, d;
    [SerializeField] private TMP_Text pTxt, qTxt, pTxt1, qTxt1, pTxt2, qTxt2, pTxt3, qTxt3, nTxt, eTxt, eTxt1, dTxt;
    [SerializeField] private GameObject next1, next2, next3, next4;

    public void Start() {
        p = -3;
        q = -3;
        e = -3;
        N = -3;
        d = -3;
    }

    public void Update() {
        if(p != -3 && q != -3) next1.SetActive(true);
        if(N != -3) next2.SetActive(true);
        if(e != -3) next3.SetActive(true);
        if(d != -3) next4.SetActive(true);
    }
    
    public void setP(string tp) {
        p = rsa.genPrime(BigInteger.Parse(tp));
        if(p == q) {
            tp = p + 1 + "";
            p = rsa.genPrime(BigInteger.Parse(tp));
        }
        pTxt.text = (p + "");
        pTxt1.text = (p + "");
        pTxt2.text = (p + "");
        pTxt3.text = (p + "");
    }

    public void setQ(string tq) {
        q = rsa.genPrime(BigInteger.Parse(tq));
        if(p == q) {
            tq = q + 1 + "";
            q = rsa.genPrime(BigInteger.Parse(tq));
        }
        qTxt.text = (q + "");
        qTxt1.text = (q + "");
        qTxt2.text = (q + "");
        qTxt3.text = (q + "");
    }

    public void genN() {
        N = rsa.calcN(p, q);
        nTxt.text = (N + "");
    }

    public void genE(string te) {
        e = rsa.calcE(p, q, BigInteger.Parse(te));
        eTxt.text = (e + "");
        eTxt1.text = (e + "");
    }
    
    public void genD() {
        d = rsa.calcD(e, rsa.calcA(p, q));
        dTxt.text = (d + "");
    }
}