using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GlobalUser
{
    public static UserData userData = new UserData();
}

//User Data
public interface IUserContainer
{
    string UserType { get; set; }
    string UserID { get; set; }
    string Username { get; set; }
    string Institution { get; set; }
    string dp { get; set; }
}

public class UserData : IUserContainer
{
    public string UserType { get; set; }
    public string UserID { get; set; }
    public string Username { get; set; }
    public string Institution { get; set; }
    public string dp { get; set; }
}
