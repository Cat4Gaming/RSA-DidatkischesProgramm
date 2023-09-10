using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Encryption : MonoBehaviour
{
    public abstract string encryptMsg(string initMsg);
    public abstract string decryptMsg(string encMsg);
}