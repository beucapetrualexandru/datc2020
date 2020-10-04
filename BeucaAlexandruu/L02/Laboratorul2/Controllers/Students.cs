
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Laboratorul2.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class Students : ControllerBase
    {
        StudentRepo stRepo = new StudentRepo();

        [HttpGet("{id}")]

        public Student GetStudent(int id)
        {
            foreach (Student st in stRepo.studentLista)
            {
                if (st.Id == id)
                    return st   ;
            }

            return null;
        }

        [HttpDelete("{id}")]

        public List<Student> DeleteStudent(int id)
        {
            foreach (Student st in stRepo.studentLista)
            {
                if (st.Id == id) 
                {
                    stRepo.studentLista.Remove(st);
                    return stRepo.studentLista;
                }
            }
            return null;
        }
        
        [HttpPost]
        public List<Student> InsertStudent([FromBody] Student student)
        {
            stRepo.studentLista.Add(student);
            return stRepo.studentLista;
        }

        [HttpPut] 
        public Student UpdateStudent([FromBody] Student student)
        {
            foreach (Student st in stRepo.studentLista) 
            {
                if (st.Id == student.Id) 
                {
                    st.nume = student.nume;
                    st.prenume = student.prenume;
                    st.facultate = student.facultate;
                    st.anStudiu = student.anStudiu;
                    st.numarMatricol = student.numarMatricol;
                    return st;
                }
            }
            return null;
        }

        
    }
}