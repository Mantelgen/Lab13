// See https://aka.ms/new-console-template for more information
using Lab13;
using Lab13.Domain;
using System;
using System.Data;
using System.Data.Common;
using Npgsql;
    

Student student_test = new Student("Ionut", "Scoala Tehnologica Nr. 2");
student_test.ID= 1;
Console.WriteLine(student_test);



