using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Numerics;

public abstract class Encryption : MonoBehaviour
{
    public abstract BigInteger[] encryptMsg(string initMsg);
    public abstract string decryptMsg(BigInteger[] encMsg);
}