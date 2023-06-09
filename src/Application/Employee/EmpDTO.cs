﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Employee
{
    public class EmpDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        // public string City { get; set; }
        public int DepartmentId { get; set; }
        public Department Department { get; set; }
        [NotMapped]
        public string Token { get; set; }



    }
}
