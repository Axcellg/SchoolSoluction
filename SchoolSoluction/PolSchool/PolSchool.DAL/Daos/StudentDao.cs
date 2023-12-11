﻿using PolSchool.DAL.Context;
using PolSchool.DAL.Entities.Base;
using PolSchool.DAL.Exceptions;
using PolSchool.DAL.Interfaces;
using PolSchool.DAL.Models;

namespace PolSchool.DAL.Daos
{
    public class StudentDao: InterStudentDao
    {
        private readonly  SchoolDbContext schoolDb;
        
        public StudentDao(SchoolDbContext schoolDb)
        {
            this.schoolDb = schoolDb;
        }

        public ModelStudent GetStudentById(int studentId)
        {
            ModelStudent model = new ModelStudent();
            try
            {
                Student? student = schoolDb.Students.Find(studentId);

                if (student is null)
                    throw new StudentExceptionsDao("No se encuentra registrado.");


                model.FirstName = student.FirstName;
                model.LastName = student.LastName;
                model.CreationDate = student.CreationDate;
                model.EnrollmentDate = student.EnrollmentDate.Value;
                model.Id = student.Id;



            }
            catch (Exception ex)
            {
                throw new StudentExceptionsDao(ex.Message);
            }
            return model;
        }

        public List<ModelStudent> GetStudents()
        {
            List<ModelStudent> students = new List<ModelStudent>();
            try
            {
                ///LINQ QUERY
                var query = from st in this.schoolDb.Students
                            where st.Deleted == false
                            orderby st.CreationDate descending
                            select new ModelStudent()
                            {
                                CreationDate = st.CreationDate,
                                EnrollmentDate = st.EnrollmentDate.Value,
                                Id = st.Id,
                                FirstName = st.FirstName,
                                LastName = st.LastName
                            };

                students = query.ToList();

            }
            catch (Exception ex)
            {
                throw new StudentExceptionsDao(ex.Message);
            }
            return students;
        }


        public void saveStudent(Student student)
        {
            try
            {
                if (student is null)
                    throw new StudentExceptionsDao("la clase debe de ser instaciada.");


                this.schoolDb.Students.Add(student);
                this.schoolDb.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new StudentExceptionsDao(ex.Message);
            }
        }

        public void RemoveStudent(Student student)
        {
            try
            {
                Student? studentToRemove = this.schoolDb.Students.Find(student.Id);

                if (studentToRemove is null)
                    throw new StudentExceptionsDao("No se encuentra registrado.");


                studentToRemove.Deleted = student.Deleted;
                studentToRemove.DeletedDate = student.DeletedDate;
                studentToRemove.UserDeleted = student.UserDeleted;

                this.schoolDb.Students.Update(studentToRemove);
                this.schoolDb.SaveChanges();

            }
            catch (Exception ex)
            {

                throw new StudentExceptionsDao(ex.Message);
            }
        }

        public void UpdateStudent(Student student)
        {
            try
            {
                Student? studentToUpdate = this.schoolDb.Students.Find(student.Id);

                if (studentToUpdate is null)
                    throw new StudentExceptionsDao("No se encuentra registrado.");


                studentToUpdate.ModifyDate = student.ModifyDate;
                studentToUpdate.UserMod = student.UserMod;
                studentToUpdate.Id = student.Id;
                studentToUpdate.LastName = student.LastName;
                studentToUpdate.FirstName = student.FirstName;
                studentToUpdate.EnrollmentDate = student.EnrollmentDate.Value;


                this.schoolDb.Students.Update(studentToUpdate);
                this.schoolDb.SaveChanges();
            }
            catch (Exception ex)
            {

                throw new StudentExceptionsDao(ex.Message);
            }
        }
    }
}
   
