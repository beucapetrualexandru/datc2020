using System;
public class Student
{
    public int Id {get; set;}
    public string nume { get; set;}
    public string prenume { get; set;}
    public string facultate { get; set;}
    public int anStudiu { get; set;}
    public int numarMatricol { get; set;}

    public Student (int id, string Nume, string Prenume, string Facultate, int AnStudiu, int NumarMatricol){
        Id = id;
      nume = Nume;
      prenume = Prenume;
      facultate = Facultate;
      anStudiu = AnStudiu;
      numarMatricol = NumarMatricol;
    }

}





    
