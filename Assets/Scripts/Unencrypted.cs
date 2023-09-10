using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Unencrypted : Encryption {
    public override string encryptMsg(string initMsg) {
        return initMsg;
    }
    public override string decryptMsg(string encMsg) {
        return encMsg;
    }
}