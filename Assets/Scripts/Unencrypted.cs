using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;
using TMPro;
using System.Text;

public class Unencrypted : Encryption {
    private Encoding unicode = Encoding.Unicode;

    public override BigInteger[] encryptMsg(string initMsg) {
        byte[] txt = ConvertToByteArray(initMsg);
        BigInteger[] enc = new BigInteger[txt.Length];
        for(int i = 0; i < txt.Length; i++) {
            enc[i] = BigInteger.Parse("" + txt[i]);
        }
        return enc;
    }

    public override string decryptMsg(BigInteger[] encMsg) {
        byte[] dec = new byte[encMsg.Length];
        for(int i = 0; i < encMsg.Length; i++) {
            dec[i] = encMsg[i].ToByteArray()[0];
        }
        return Encoding.Unicode.GetString(dec);
    }
    
    byte[] ConvertToByteArray(string s) {
        byte[] b = unicode.GetBytes(s);
        return b;
    }
}