using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Models;

public class SignUp{

    public string? password{get;set;}
    [Compare("password")]
    public string? confirmpassword{get;set;}
    public int userid{get;set;}
   



}