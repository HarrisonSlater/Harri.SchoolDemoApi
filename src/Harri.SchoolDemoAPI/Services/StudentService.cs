﻿using Harri.SchoolDemoAPI.Models.Dto;
using Harri.SchoolDemoAPI.Models;
using Harri.SchoolDemoAPI.Repository;
using System.Data;
using System.Threading.Tasks;

namespace Harri.SchoolDemoAPI.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<int> AddStudent(NewStudent newStudent)
        {
            return await _studentRepository.AddStudent(newStudent);
        }

        public async Task<Student?> GetStudent(int sId)
        {
            return await _studentRepository.GetStudent(sId);
        }


        public async Task<bool> UpdateStudent(Student student)
        {
            return await _studentRepository.UpdateStudent(student);
        }

        public async Task<bool?> DeleteStudent(int sId)
        {
            return await _studentRepository.DeleteStudent(sId);
        }

        public async Task<Student?> PatchStudent(int sId, StudentPatchDto student)
        {
            var existingStudent = await _studentRepository.GetStudent(sId);
            if (existingStudent != null)
            {
                student.ApplyChangesTo(existingStudent);
                await _studentRepository.UpdateStudent(existingStudent);
                return existingStudent;
            }
            else
            {
                return null;
            }
        }
    }
}