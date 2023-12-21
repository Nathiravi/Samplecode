using System;
using System.ComponentModel.DataAnnotations;

namespace Insurance.Models;

public class Add{

    
    public int Userid{get;set;}
    public string? Name{get;set;}

    public string? Password{get;set;}
    public int Age {get;set;}
    public string? Gender{get;set;}
    public string? Role{get;set;}

    public string? PhoneNumber {get;set;}
    
    public string? city{get;set;}
    
    

}