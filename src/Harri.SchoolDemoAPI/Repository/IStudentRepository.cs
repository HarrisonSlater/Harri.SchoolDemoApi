﻿using Harri.SchoolDemoAPI.Models;
using Harri.SchoolDemoAPI.Models.Dto;

namespace Harri.SchoolDemoAPI.Repository
{
    public interface IStudentRepository
    {
        int AddStudent(NewStudent newStudent);
        bool? DeleteStudent(int sId);
        Student? GetStudent(int sId);
        bool UpdateStudent(Student newStudent);
    }
}