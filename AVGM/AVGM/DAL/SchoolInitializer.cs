using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using AVGM.Models;

namespace AVGM.DAL
{
    public class SchoolInitializer : System.Data.Entity.DropCreateDatabaseAlways<SchoolContext>
    {
        protected override void Seed(SchoolContext context)
        {
            //TODO remove after debugging/testing
            //adds "fake" addresses to the Address table of the AVGM database
            List<Address> testAddresses = new List<Address>();
            testAddresses.Add(new Address
            {
                AddressID = Guid.Parse("0132C0C3-CDC4-41F7-5555-AA6C1452B58F"),
                StreetAddress = "1234 Main St",
                StreetAddress2 = "",
                City = "Libertyville",
                State = "IL",
                Zip = "60048",
                Guardian = new List<Guardian>()
            });

            testAddresses.Add(new Address
            {
                AddressID = Guid.Parse("0132C0C3-CDC4-41F7-6666-AA6C1452B58F"),
                StreetAddress = "54321 Union Ave",
                StreetAddress2 = "",
                City = "Libertyville",
                State = "IL",
                Zip = "60048",
                Guardian = new List<Guardian>()
            });

            testAddresses.Add(new Address
            {
                AddressID = Guid.Parse("0132C0C3-CDC4-41F7-7777-AA6C1452B58F"),
                StreetAddress = "4619 N Ravenswood Ave",
                StreetAddress2 = "Suite 302C",
                City = "Chicago",
                State = "IL",
                Zip = "60640",
                Guardian = new List<Guardian>()
            });

            testAddresses.ForEach(s => context.Addresses.Add(s));
            context.SaveChanges();

            //adds "fake" addresses to the Address table of the AVGM database
            List<Job> testJob = new List<Job>();
            testJob.Add(new Job
            {
                JobID = Guid.Parse("0132C0C3-CDC4-41F7-8888-AA6C1452B58F"),
                Title = "Software Developer",
                PhoneNumber = "312-123-5555",
                Address = testAddresses.First(m => m.AddressID == Guid.Parse("0132C0C3-CDC4-41F7-7777-AA6C1452B58F"))
            });

            //adds "fake" users to the Guardian table of the AVGM database
            List<Guardian> testGuardians = new List<Guardian>();

                testGuardians.Add(new Guardian
                {
                    GuardianID = Guid.Parse("0132C0C3-CDC4-41F7-1111-AA6C1452B58F"),
                    FName = "John",
                    DisplayName = "J",
                    MName = "James",
                    LName = "Loiacano",
                    Email = "example2@example.com",
                    Gender = 'M',
                    Relationship = "Father",
                    PhoneNumber = "847-596-5555",
                    LivesWithStudent = true,
                    SharingContactInfo = true,
                    Address = testAddresses.First(m => m.AddressID == Guid.Parse("0132C0C3-CDC4-41F7-5555-AA6C1452B58F")),
                    Job = testJob.First(m => m.JobID == Guid.Parse("0132C0C3-CDC4-41F7-8888-AA6C1452B58F"))
                    
                });

                testGuardians.Add(new Guardian
                {
                    GuardianID = Guid.Parse("0132C0C3-CDC4-41F7-2222-AA6C1452B58F"),
                    FName = "Megan",
                    MName = "Kristine",
                    LName = "Loiacano",
                    Email = "example@example.com",
                    Gender = 'F',
                    Relationship = "Mother",
                    PhoneNumber = "773-931-5555",
                    LivesWithStudent = true,
                    SharingContactInfo = false,
                    Address = testAddresses.First(m => m.AddressID == Guid.Parse("0132C0C3-CDC4-41F7-5555-AA6C1452B58F"))
                });

            testGuardians.ForEach(s => context.Guardians.Add(s));
            context.SaveChanges();

            //adds "fake" students to the Students table of the AVGM database

            List<Student> testStudents = new List<Student>();
                testStudents.Add(new Student
                {
                    StudentID = Guid.Parse("0132C0C3-CDC4-41F7-3333-AA6C1452B58F"),
                    FName = "Zoe",
                    MName = "Jastine",
                    LName = "Loiacano",
                    Gender = 'F',
                    DOB = DateTime.Parse("12/13/2012"),
                    SSNumber = "111-11-1111"
                });

                testStudents.Add(new Student
                {
                    StudentID = Guid.Parse("0132C0C3-CDC4-41F7-4444-AA6C1452B58F"),
                    FName = "Xander",
                    MName = "Mikah",
                    LName = "Loiacano",
                    Gender = 'M',
                    DOB = DateTime.Parse("01/08/2015"),
                    SSNumber = "222-22-2222"
                });

            testStudents.ForEach(s => context.Students.Add(s));
            context.SaveChanges();

            //adds "fake" connections to the StudentGuardian table of the AVGM database

            List<StudentGuardian> testStudentGuardianConnections = new List<StudentGuardian>();
                testStudentGuardianConnections.Add(new StudentGuardian
                {
                    StudentID = Guid.Parse("0132C0C3-CDC4-41F7-4444-AA6C1452B58F"),
                    GuardianID = Guid.Parse("0132C0C3-CDC4-41F7-1111-AA6C1452B58F")
                });

                testStudentGuardianConnections.Add(new StudentGuardian
                {
                    StudentID = Guid.Parse("0132C0C3-CDC4-41F7-4444-AA6C1452B58F"),
                    GuardianID = Guid.Parse("0132C0C3-CDC4-41F7-2222-AA6C1452B58F")
                });

                testStudentGuardianConnections.Add(new StudentGuardian
                {
                    StudentID = Guid.Parse("0132C0C3-CDC4-41F7-3333-AA6C1452B58F"),
                    GuardianID = Guid.Parse("0132C0C3-CDC4-41F7-1111-AA6C1452B58F")
                });

                testStudentGuardianConnections.Add(new StudentGuardian
                {
                    StudentID = Guid.Parse("0132C0C3-CDC4-41F7-3333-AA6C1452B58F"),
                    GuardianID = Guid.Parse("0132C0C3-CDC4-41F7-2222-AA6C1452B58F")
                });

            testStudentGuardianConnections.ForEach(s => context.StudentGuardians.Add(s));
            context.SaveChanges();

            //adds "fake" connections to the GuardianAddress table of the AVGM database

            //List<GuardianAddress> testGuardianAddressConnections = new List<GuardianAddress>();
            //    testGuardianAddressConnections.Add(new GuardianAddress
            //    {
            //        GuardianID = Guid.Parse("0132C0C3-CDC4-41F7-1111-AA6C1452B58F"),
            //        AddressID = Guid.Parse("0132C0C3-CDC4-41F7-5555-AA6C1452B58F")
            //    });

            //    testGuardianAddressConnections.Add(new GuardianAddress
            //    {
            //        GuardianID = Guid.Parse("0132C0C3-CDC4-41F7-2222-AA6C1452B58F"),
            //        AddressID = Guid.Parse("0132C0C3-CDC4-41F7-6666-AA6C1452B58F")
            //    });

            //testGuardianAddressConnections.ForEach(s => context.GuardianAddresses.Add(s));
            //context.SaveChanges();
            
        }
    }
}