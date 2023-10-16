using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Numerics;
using TMPro;
using System.Text;

public class RSA : Encryption {
    private BigInteger p, q, N, e, d;
    [SerializeField] private TMP_Text keysText;
    [SerializeField] private bool genOnStart;
    private Encoding unicode = Encoding.Unicode;

    public override BigInteger[] encryptMsg(string initMsg) {
        byte[] txt = ConvertToByteArray(initMsg);
        int length = txt.Length;
        BigInteger[] enc = new BigInteger[length];
        for(int i = 0; i < length; i++) {
            enc[i] = encMsg(e, N, txt[i]);
        }
        return enc;
    }

    public override string decryptMsg(BigInteger[] encMsg) {
        int length = encMsg.Length;
        byte[] dec = new byte[length];
        for(int i = 0; i < length; i++) {
            dec[i] = decMsg(d, N, encMsg[i]).ToByteArray()[0];
        }
        return Encoding.Unicode.GetString(dec);
    }

    public void Start() {
        if(genOnStart) {
            genKeys();
            keysText.text = ("public Key:\n  N: " + N + "\n  d: " + d + "\nprivate Key:\n  e: " + e);
        }
    }

    byte[] ConvertToByteArray(string s) {
        byte[] b = unicode.GetBytes(s);
        return b;
    }

    private static string UnicodeToChar(string hex) {
        int code = int.Parse(hex, System.Globalization.NumberStyles.HexNumber);
        string unicodeString = char.ConvertFromUtf32(code);
        return unicodeString;
    }

    public void genKeys() {
        p = genPrime(randomNum());
        q = p;
        while(p == q) {
            q = genPrime(randomNum());
        }
        N = calcN(p, q);
        int r1 = randomNum(1 , 100000);
        int r2 = r1;
        while(r1 == r2) {
            r2 = randomNum(1, 100000);
        }
        e = calcE(p, q, r1 % r2);
        d = calcD(e, calcA(p, q));
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
}